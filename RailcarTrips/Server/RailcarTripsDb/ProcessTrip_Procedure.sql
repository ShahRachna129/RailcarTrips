-- ============================
-- Create Trips table
-- ============================
IF OBJECT_ID('dbo.Trips', 'U') IS NOT NULL DROP TABLE dbo.Trips;
CREATE TABLE dbo.Trips (
    TripID INT IDENTITY PRIMARY KEY,
    Equipment_Id Nvarchar(50),
    OriginCityId INT,
    DestinationCityId INT,
    StartUtc DATETIME,
    EndUtc DATETIME,
    TotalTripHours FLOAT
);

-- ============================
-- Create TripEvents table
-- ============================
IF OBJECT_ID('dbo.TripEvents', 'U') IS NOT NULL DROP TABLE dbo.TripEvents;
CREATE TABLE dbo.TripEvents (
    TripEventID INT IDENTITY PRIMARY KEY,
    TripID INT,
    Equipment_Id Nvarchar(50),
    City_Id INT,
    Event_Order INT,
    EventTime_UTC DATETIME
);

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[ProcessingTrips]
As
BEGIN
 SET NOCOUNT ON;

    -- ============================
    -- Step 1: compute trips

    -- OrderEvents creates a Temperory ResultSet (CTE)
    -- Everytime SQL sees 'w' it increments TripNumber
    -- All events after that belong to that trip
    -- TripsCTE now we group by  Equipment_Id, TripNumber So one row per Equipment + TripNumber
    -- Find trip start gets timestamp of W ,City where W occurred
    -- Find trip end Timestamp of Z, Find destination city
    -- HAVING clause Prevents broken trips like Z without W,W without Z
    -- Store Trips into temp table ,Creates a temporary table So we can reuse this data multiple times dropping in end of proc
    -- Insert Trips Creates permanent trip records and calculates TotalTripHours
    -- Added TripID for further mapping
    -- Insert TripEvents rebuild OrderedEvents again (same logic).Because CTEs only exist for one statement and do join So every event gets the correct TripID.
    -- Now every event is permanently tied to a trip.
    -- Return Trip for UI
    -- ============================
    WITH OrderedEvents AS (
        SELECT 
            e.Equipment_Id,
            e.City_Id,
            e.Event_Code,
            e.Event_Order,
            e.EventTime_UTC,
            SUM(CASE WHEN e.Event_Code = 'W' THEN 1 ELSE 0 END)
                OVER (PARTITION BY e.Equipment_Id ORDER BY e.EventTime_UTC ROWS UNBOUNDED PRECEDING) AS TripNumber
        FROM dbo.equipment_events e
    ),
    TripsCTE AS (
        SELECT 
            o.Equipment_Id,
            o.TripNumber,
            MIN(CASE WHEN o.Event_Code = 'W' THEN o.EventTime_UTC END) AS StartUtc,
            MIN(CASE WHEN o.Event_Code = 'W' THEN o.City_Id END) AS OriginCityId,
            MAX(CASE WHEN o.Event_Code = 'Z' THEN o.EventTime_UTC END) AS EndUtc,
            MAX(CASE WHEN o.Event_Code = 'Z' THEN o.City_Id END) AS DestinationCityId
        FROM OrderedEvents o
        GROUP BY o.Equipment_Id, o.TripNumber
        HAVING MIN(CASE WHEN o.Event_Code = 'W' THEN o.EventTime_UTC END) IS NOT NULL
           AND MAX(CASE WHEN o.Event_Code = 'Z' THEN o.EventTime_UTC END) IS NOT NULL
    )
    -- Store trips in temp table
    SELECT * INTO #TripsTemp FROM TripsCTE;

    -- ============================
    -- Step 2: Insert trips
    -- ============================
    INSERT INTO dbo.Trips (Equipment_Id, OriginCityId, DestinationCityId, StartUtc, EndUtc, TotalTripHours)
    SELECT 
        Equipment_Id,
        OriginCityId,
        DestinationCityId,
        StartUtc,
        EndUtc,
        DATEDIFF(MINUTE, StartUtc, EndUtc)/60.0
    FROM #TripsTemp;

    -- Capture TripID mapping
    SELECT t.TripID, tmp.Equipment_Id, tmp.TripNumber, tmp.StartUtc, tmp.EndUtc
    INTO #TripMapping
    FROM dbo.Trips t
    JOIN #TripsTemp tmp
        ON t.Equipment_Id = tmp.Equipment_Id
       AND t.StartUtc = tmp.StartUtc
       AND t.EndUtc = tmp.EndUtc;

    -- ============================
    -- Step 3: Insert TripEvents
    -- ============================
    WITH OrderedEvents AS (
        SELECT 
            e.Equipment_Id,
            e.City_Id,
            e.Event_Code,
            e.Event_Order,
            e.EventTime_UTC,
            SUM(CASE WHEN e.Event_Code = 'W' THEN 1 ELSE 0 END)
                OVER (PARTITION BY e.Equipment_Id ORDER BY e.EventTime_UTC ROWS UNBOUNDED PRECEDING) AS TripNumber
        FROM dbo.equipment_events e
    )
    INSERT INTO dbo.TripEvents (TripID, Equipment_Id, City_Id, Event_Order, EventTime_UTC)
    SELECT 
        tm.TripID,
        oe.Equipment_Id,
        oe.City_Id,
        oe.Event_Order,
        oe.EventTime_UTC
    FROM OrderedEvents oe
    JOIN #TripMapping tm
        ON oe.Equipment_Id = tm.Equipment_Id
       AND oe.TripNumber = tm.TripNumber;

    -- ============================
    -- Step 4: Return trips with city names
    -- ============================
    SELECT 
        t.TripID,
        t.Equipment_Id,
        origin.City_Name AS OriginCity,
        destination.City_Name AS DestinationCity,
        t.StartUtc,
        t.EndUtc,
        t.TotalTripHours
    FROM dbo.Trips t
    JOIN dbo.canadian_cities origin ON origin.City_Id = t.OriginCityId
    JOIN dbo.canadian_cities destination ON destination.City_Id = t.DestinationCityId
    ORDER BY t.StartUtc;

    -- Clean up temp tables
    DROP TABLE #TripsTemp;
    DROP TABLE #TripMapping;
END
GO

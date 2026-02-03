CREATE TABLE [dbo].[Event_Code_Definations] (
    [Id]               INT            NOT NULL PRIMARY KEY CLUSTERED ([Id] ASC),
    [EventCode]        CHAR (1)       NULL,
    [EventDescription] NVARCHAR (100) NULL,
    [LongDescription]  NVARCHAR (MAX) NULL
);

Insert into [dbo].[Event_Code_Definations] ([Id] , [EventCode],[EventDescription],[LongDescription])
VALUES (1,'W','Released','Railcar equipment is released from origin')
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[equipment_events](
	[Equipment_Id] [nvarchar](50) NOT NULL,
	[Event_Code] [nvarchar](50) NOT NULL,
	[Event_Time] [datetime2](7) NOT NULL,
	[City_Id] [tinyint] NOT NULL,
	[Event_Order] [int] NULL,
	[EventTime_UTC] [datetime2](7) NULL
) ON [PRIMARY]
GO

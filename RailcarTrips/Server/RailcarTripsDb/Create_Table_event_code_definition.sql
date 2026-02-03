SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[event_code_definitions](
	[Event_Code] [nvarchar](50) NOT NULL,
	[Event_Description] [nvarchar](50) NOT NULL,
	[Long_Description] [nvarchar](50) NOT NULL,
	[Sort_Order] [int] NULL
) ON [PRIMARY]
GO

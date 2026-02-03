SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[canadian_cities](
	[City_Id] [tinyint] NOT NULL,
	[City_Name] [nvarchar](50) NOT NULL,
	[Time_Zone] [nvarchar](50) NOT NULL,
	[TimeZoneHrs] [int] NULL,
	[TimeZoneMinutes] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[canadian_cities] ADD  CONSTRAINT [PK_canadian_cities] PRIMARY KEY CLUSTERED 
(
	[City_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

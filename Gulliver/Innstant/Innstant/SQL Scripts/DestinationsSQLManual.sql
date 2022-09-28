CREATE TABLE [INNSTANT_GULLIVER_DESTINATION_CONVERSIONS] (
	GulliverDestinationId int,
	GulliverDestinationName varchar(255),
	InnstantDestinationId int,
	InnstantDestinationName varchar(255),
	"Type" varchar(50) 
);

drop table [INNSTANT_GULLIVER_DESTINATION_CONVERSIONS]

select * FROM [INNSTANT_GULLIVER_DESTINATION_CONVERSIONS]

-- Moze i preko SP

CREATE Procedure [INSERT_DATA_INTO_DB] (
	@GulliverDestinationId int,
	@InnstantDestinationId int,
	@GulliverDestinationName varchar(255),
	@InnstantDestinationName varchar(255),
	@Type varchar(50))
AS
BEGIN
	INSERT INTO [INNSTANT_GULLIVER_DESTINATION_CONVERSIONS] VALUES (@GulliverDestinationId, @InnstantDestinationId, @GulliverDestinationName, @InnstantDestinationName, @Type)
END

exec [INSERT_DATA_INTO_DB] 0, 2995, AJSD, Jerusalem, City


___________________________
CREATE TABLE [dbo].[Destinations](
	[id] [int] NULL,
	[name] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[lat] [varchar](50) null,
	[lon] [varchar](50) null,
	[countryid] [varchar](50) NULL,
	[searchable] int null,
	[seoname] [varchar](50) NULL,
	[state] [varchar](50) NULL,
	[contains] [varchar](max) NULL,
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Hotel_Destinations](
	hotel_id [int] NULL,
	destination_id [int] NULL,
	surroundings [int] NULL,
) ON [PRIMARY]
GO


drop table [dbo].[Destinations]

select * from  [dbo].[Destinations]

ALTER TABLE [dbo].[Destinations]
ALTER COLUMN [contains] [varchar](max);
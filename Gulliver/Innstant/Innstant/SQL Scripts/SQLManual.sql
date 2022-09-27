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
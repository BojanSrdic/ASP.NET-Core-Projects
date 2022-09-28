CREATE TABLE [Innstant_Hotels] (
	HotelId int ,
	HotelName varchar(255),
	HotelAddress varchar(255),
	"Status" int,
	Zip int,
	Phone varchar(255),
	Fax varchar(255),
	Lat float,
	Lon float,
	Stars int ,
	Seoname varchar(255),
);

CREATE TABLE [Innstant_Hotel_Destination] (
	HotelId int,
	DestinationId int,
	Surroundings int,

);

CREATE TABLE [Innstant_Destinations] (
	DestinationId int,
	DestinationName varchar(255),
	DestinationType varchar(255),
	Lat float,
	Lon float,
	CountryId varchar(255),
	Searchable int ,
	Seoname varchar(255),
	"State" varchar(255),
	"Contains" varchar(255) 
);


--
INSERT INTO [Innstant_Hotels] (HotelId, HotelName, HotelAddress, Lat, Lon) 
VALUES (1, 'Cardinal', 'Tom B. Erichsen', 44.067100524902, 12.582200050354);

--
drop table [Innstant_Hotels]

select * FROM [Innstant_Hotels]

-- check data type of table
SELECT COLUMN_NAME, DATA_TYPE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = '[Innstant_Hotels]'






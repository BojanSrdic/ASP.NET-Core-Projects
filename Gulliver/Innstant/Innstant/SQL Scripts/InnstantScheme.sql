DROP TABLE [Innstant_Destinations]
DROP TABLE [Innstant_Hotel_Destination]
DROP TABLE [Innstant_Hotels]

CREATE TABLE [Innstant_Destinations] (
	DestinationId int,
	DestinationName varchar(255),
	DestinationType varchar(255),
	Latitude varchar(255),
	Longitude varchar(255),
	CountryId varchar(255),
	Searchable int ,
	Seoname varchar(255),
	"State" varchar(255),
	"Contains" varchar(max) 
);

CREATE TABLE [Innstant_Hotel_Destination] (
	HotelId int,
	DestinationId int,
	Surroundings int,
);

CREATE TABLE [Innstant_Hotels] (
	HotelId int ,
	HotelName varchar(255),
	HotelAddress varchar(255),
	"Status" int,
	Zip int,
	Phone varchar(255),
	Fax varchar(255),
	Latitude varchar(255),
	Longitude varchar(255),
	Stars int ,
	Seoname varchar(255),
);

select * from [Innstant_Destinations]
select * from [Innstant_Hotel_Destination]
select * from [Innstant_Hotels]
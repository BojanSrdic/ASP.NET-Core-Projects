
CREATE TABLE [Innstant_Hotels] (
	HotelId int,
	HotelName varchar(255),
	Address varchar
	status int
	zip int
	phone varchar
	fax varchar
	lat decimal
	lon decimal
	stars int 
	seoname varchar
);

CREATE TABLE [Innstant_Hotel_Destination] (
	HotelId int,
	DestinationId int,
	Surroundings int,

);

CREATE TABLE [Innstant_Destinations] (
	DestinationId int,
	DestinationName int,
	CountryId varchar(max),
	Type varchar(max),
	Contains varchar(max), //ovo nevalja

);



drop table [INNSTANT_GULLIVER_DESTINATION_CONVERSIONS]

select * FROM [INNSTANT_GULLIVER_DESTINATION_CONVERSIONS]

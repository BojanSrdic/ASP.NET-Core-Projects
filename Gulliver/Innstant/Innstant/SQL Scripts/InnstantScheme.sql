DROP TABLE [Innstant_Destinations]
DROP TABLE [Innstant_Hotel_Destination]
DROP TABLE [Innstant_Hotels]
DROP TABLE [Innstant_Rooms]

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

CREATE TABLE [Innstant_Rooms] (
	HotelId int ,
	RoomName varchar(255),
	RoomCategory varchar(255),
	Bedding varchar(255)
);

select * from [Innstant_Destinations]
select * from [Innstant_Hotel_Destination]
select * from [Innstant_Hotels]
select * from [Innstant_Rooms]

select * from [Innstant_Hotels] where HotelId = 695
select * from [Innstant_Hotels] order by HotelName

---------------------------------------------------
			-- ## Innstant Boards ## --

-- Create [Innstant_Boards] table: BoardId, BoardCode, Board name
-- Create [Innstant_Possible_Boards_For_Hotels] table: HotelId, BoardId
-- Create [Innstant_Gulliver_Boards_Conversions] table: InnstantBoardId, GulliverBoardId

CREATE TABLE INNSTANT_GULLIVER_BOARDS(
	BoardId int,
	InnstantBoardCode varchar(30),
	InnstantDescription varchar(255),
	GulliverId int
);

Insert into INNSTANT_GULLIVER_BOARDS values ('1', 'RO', 'Room Only', '15')
Insert into INNSTANT_GULLIVER_BOARDS values ('2', 'BB', 'Breakfast', '6')
Insert into INNSTANT_GULLIVER_BOARDS values ('3', 'HB', 'Half Board', '13')
Insert into INNSTANT_GULLIVER_BOARDS values ('4', 'FB', 'Full Board', '11')
Insert into INNSTANT_GULLIVER_BOARDS values ('5', 'AI', 'All Inclusive', '1')

select * from INNSTANT_GULLIVER_BOARDS



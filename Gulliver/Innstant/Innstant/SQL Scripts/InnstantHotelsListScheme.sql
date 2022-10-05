


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






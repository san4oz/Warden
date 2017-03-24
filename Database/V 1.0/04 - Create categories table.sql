create table Categories(
	Id uniqueidentifier primary key not null,
	Title nvarchar(150) not null,
	Keywords nvarchar(max)
);
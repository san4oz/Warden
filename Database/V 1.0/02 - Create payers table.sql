create table Payers(
	Id uniqueidentifier not null primary key,
	PayerId nvarchar(50) not null,
	Name nvarchar(255) not null
);
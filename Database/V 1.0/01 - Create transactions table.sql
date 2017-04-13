create table Transactions(
	Id uniqueidentifier not null primary key,
	PayerId nvarchar(50) not null,
	ReceiverId nvarchar(50) not null,
	Price money not null,
	[Date] datetime not null,
	Keywords nvarchar(max) not null,
	ExternalId nvarchar(100) not null,
);

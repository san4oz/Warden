create table CategoryKeyword
(
	Id uniqueidentifier not null primary key,
	Keyword nvarchar(150) not null,
	CategoryId uniqueidentifier not null foreign key references Categories(Id),
	SuccessVotes bigint not null,
	TotalVotes bigint not null,
);
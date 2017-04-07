create table TransactionImportTaskConfigurations(
	Id uniqueidentifier not null primary key,
	StartDate datetime2 not null,
	EndDate datetime2 not null,
	PayerId nvarchar(50) not null
);
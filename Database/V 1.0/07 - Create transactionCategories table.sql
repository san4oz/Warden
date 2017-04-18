create table TransactionCategories(
	Id uniqueidentifier,
	TransactionId uniqueidentifier foreign key references Transactions(Id),
	CategoryId uniqueidentifier foreign key references Categories(Id),
	Voted bit default(0)
	);

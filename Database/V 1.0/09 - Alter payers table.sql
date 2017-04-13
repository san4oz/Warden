alter table Payers
add RegionId nvarchar(5) foreign key references [Regions](Id)
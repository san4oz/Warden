﻿create table Regions(
	[Id] nvarchar(5) primary key not null,
	[Region] nvarchar(50) not null
)

insert into Regions
values('UA-18', N'Житомирська область'),
	('UA-40', N'Севастополь'),
	('UA-43', N'Крим'),
	('UA-71', N'Черкаська область'),
	('UA-74', N'Чернігівська область'),
	('UA-77', N'Чернівецька область'),
	('UA-12', N'Дніпропетровська область'),
	('UA-14', N'Донецька область'),
	('UA-26', N'Івано-Франківська область'),
	('UA-63', N'Харківська область'),
	('UA-65', N'Херсонська область'),
	('UA-68', N'Хмельницька область'),
	('UA-30', N'Київ'),
	('UA-32', N'Київська область'),
	('UA-35', N'Кіровоградська область'),
	('UA-09', N'Луганська область'),
	('UA-46', N'Львівська область'),
	('UA-48', N'Миколаївська область'),
	('UA-51', N'Одеська область'),
	('UA-53', N'Полтавська область'),
	('UA-56', N'Рівненська область'),
	('UA-59', N'Сумська область'),
	('UA-61', N'Тернопільська область'),
	('UA-21', N'Закарпатська область'),
	('UA-05', N'Вінницька область'),
	('UA-07', N'Волинська область'),
	('UA-23', N'Запорізька область')
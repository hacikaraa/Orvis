--OrvisDataBase
--Base
	--ID int primary key identity(1,1),
	--Name varchar(200),
	--DateCreated datetime,
	--DateUpdated datetime,
	--UserCreated int,
	--UserUpdated int,

--Category
CREATE TABLE Framework_Category
(
	ID int primary key identity(1,1),
	Name varchar(200),
	DateCreated datetime,
	DateUpdated datetime,
	UserCreated int,
	UserUpdated int,
	BaseCategoryID int references Framework_Category(ID)
)
--Property
CREATE TABLE Framework_Property
(
	ID int primary key identity(1,1),
	Name varchar(200),
	DateCreated datetime,
	DateUpdated datetime,
	UserCreated int,
	UserUpdated int
)
--PropertyValue
CREATE TABLE Framework_PropertyValue
(
	ID int primary key identity(1,1),
	Value varchar(max),
	DateCreated datetime,
	DateUpdated datetime,
	UserCreated int,
	UserUpdated int,
	PropertyID int references Framework_Property(ID)
)
--Item

--ItemProperty
CREATE TABLE Catalog_ItemProperty
(
	ID int primary key identity(1,1),
	ItemID int references Catalog_Item(ID), --Yazýlmadý daha
	PropertyID int references Framework_Property(ID)
)
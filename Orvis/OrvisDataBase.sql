--OrvisDataBase
--Base
	--ID int primary key identity(1,1),
	--Name varchar(200),
	--Description varchar(1000),
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
--Image
CREATE TABLE Framework_Image
(
	ID int primary key identity(1,1),
	DateCreated datetime,
	DateUpdated datetime,
	UserCreated int,
	UserUpdated int,
	Url varchar(500),
	ImageBase64 varchar(max)
)
--Item
CREATE TABLE Catalog_Item
(
	ID int primary key identity(1,1),
	Name varchar(200),
	Description varchar(1000),
	DateCreated datetime,
	DateUpdated datetime,
	UserCreated int,
	UserUpdated int,
	Code varchar(200),
	SecondName varchar(200),
	WarningText varchar(200),
	ManufacturerID int references Catalog_Manufacturer(ID)
)
--ItemProperty
CREATE TABLE Catalog_ItemProperty
(
	ID int primary key identity(1,1),
	ItemID int references Catalog_Item(ID),
	PropertyID int references Framework_Property(ID)
)
--ItemPropertyValue
CREATE TABLE Catalog_ItemPropertyValue
(
	ID int primary key identity(1,1),
	ItemID int references Catalog_Item(ID), 
	PropertyValueID int references Framework_PropertyValue(ID)
)
--ItemImage
CREATE TABLE Catalog_ItemImage
(
	ID int primary key identity(1,1),
	ItemID int references Catalog_Item(ID), 
	ImageID int references Framework_Image(ID)
)
--ItemCategories
CREATE TABLE Catalog_ItemCategory
(
	ID int primary key identity(1,1),
	ItemID int references Catalog_Item(ID), 
	CategoryID int references Framework_Category(ID)
)
--Manufacturer
CREATE TABLE Catalog_Manufacturer
(
	ID int primary key identity(1,1),
	Name varchar(200),
	DateCreated datetime,
	DateUpdated datetime,
	UserCreated int,
	UserUpdated int,
	ImageID int references Framework_Image(ID) 
)
--Tax
CREATE TABLE Finance_Tax
(
	ID int primary key identity(1,1),
	Name varchar(200),
	DateCreated datetime,
	DateUpdated datetime,
	UserCreated int,
	UserUpdated int,
	Rate float
)
--Catalog_ItemTax
CREATE TABLE Catalog_ItemCategory
(
	ID int primary key identity(1,1),
	ItemID int references Catalog_Item(ID), 
	TaxID int references Framework_Category(ID)
)
--language
CREATE TABLE Framework_Language
(
	ID int primary key identity(1,1),
	Name varchar(200),
	Code varchar(200),
	DateCreated datetime,
	DateUpdated datetime,
	UserCreated int,
	UserUpdated int
)
--ParameterValue
CREATE TABLE Framework_ParemeterValue
(
	ID int primary key identity(1,1),
	Value varchar(500),
	DateCreated datetime,
	DateUpdated datetime,
	UserCreated int,
	UserUpdated int,
	LanguageID int references Framework_Language(ID)
)
--parameter
CREATE TABLE Framework_Parameter
(
	ID int primary key identity(1,1),
	Alias varchar(256),
	DateCreated datetime,
	DateUpdated datetime,
	UserCreated int,
	UserUpdated int
)
--ParameterParameterValue
CREATE TABLE Framework_ParameterParameterValue
(
	ID int primary key identity(1,1),
	ParameterID int references Framework_Parameter(ID),
	ParemeterValueID int references Framework_ParemeterValue(ID)
)



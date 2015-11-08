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

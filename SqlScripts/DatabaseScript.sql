USE Master;
GO

if exists (select * from sys.databases where name='CarDealership')
		drop database CarDealership
go

CREATE DATABASE CarDealership;
GO 

Use CarDealership;
GO

if exists (select * from sys.tables where name = 'BodyColors')
    drop table BodyColors;
go 

Create Table BodyColors (
	BodyColorId int identity(1,1) Primary Key, 
	ColorName varChar(20) not null, 
)

if exists (select * from sys.tables where name = 'InteriorColors')
    drop table InteriorColors;
go 

Create Table InteriorColors (
	InteriorColorID int identity(1,1) Primary Key, 
	InteriorColorName varChar(20) not null, 
)

if exists (select * from sys.tables where name = 'BodyTypes')
    drop table BodyTypes;
go 

Create Table BodyTypes (
	BodyTypeId int identity(1,1) Primary Key, 
	BodyTypeName varChar(20) not null, 
)

if exists (select * from sys.tables where name = 'Transmissions')
    drop table Transmissions;
go

Create Table Transmissions (
	TransmissionId int identity(1,1) Primary Key, 
	TransmissionName varChar(20) not null, 
)


if exists (select * from sys.tables where name = 'PurchaseTypes')
    drop table PurchaseTypes;
go

Create Table PurchaseTypes (
	PurchaseTypeId int identity(1,1) Primary Key, 
	PurchaseTypeName varChar(20) not null, 
)

if exists (select * from sys.tables where name = 'Roles')
    drop table Roles;
go

Create Table Roles (
	RoleId int identity(1,1) Primary Key, 
	RoleName varChar(20) not null, 
)

if exists (select * from sys.tables where name = 'Users')
    drop table Users;
go

Create Table Users (
	UserId int identity(1,1) Primary Key, 
	FirstName varChar(20) not null,
	LastName varchar(20) not null, 
	EMail varchar(30) not null, 
	UserName varchar(30) not null, 
	Password varchar(40) not null,
	RoleId int foreign key references Roles(RoleId) not null 

)

if exists (select * from sys.tables where name = 'Make')
    drop table Make;
go
Create Table Make (
	MakeId int identity(1,1) Primary Key, 
	MakeName varChar(20) not null, 
	UserId int foreign key references Users(UserId) not null,
	DateAdded datetime2 not null, 
)

if exists (select * from sys.tables where name = 'Models')
    drop table Models;
go

Create Table Models (
	ModelId int identity(1,1) Primary Key, 
	ModelName varChar(20) not null, 
	UserId int foreign key references Users(UserId) not null,
	DateAdded datetime2 not null, 
	MakeId int foreign key references Make(MakeId) not null,
)

if exists (select * from sys.tables where name = 'States')
    drop table States;
go

Create Table States (
	StateId varchar(2) Primary Key, 
	StateName varChar(20) not null, 
)

if exists (select * from sys.tables where name = 'Dealerships')
    drop table Dealerships;
go

Create Table Dealerships (
	DealershipId int identity(1,1) Primary Key, 
	DealershipName varChar(20) not null, 
	StreetAddress varchar(40) not null, 
	City varchar(30) not null, 
	StateId varchar(2) foreign key references States(StateId) not null, 
	PhoneNumber nvarchar(15) not null, 
)

if exists (select * from sys.tables where name = 'Specials')
    drop table Specials;
go

Create Table Specials (
	SpecialId int identity(1,1) Primary Key, 
	Title varChar(50) not null, 
	SpecialDescription nvarchar(150) not null, 
	StartDate datetime2 not null, 
	ExpirationDate datetime2 not null
)

if exists (select * from sys.tables where name = 'Vehicles')
    drop table Vehicles;
go

Create Table Vehicles (
	VehicleId int identity(1,1) Primary Key,
	VinNumber varchar(17) not null,
	ModelId int foreign key references Models(modelId),
	VehicleYear datetime2 not null, 
	BodyTypeId int foreign key references BodyTypes(BodyTypeId) not null,
	TransmisisonId int foreign key references Transmissions(TransmissionId) not null,
	BodyColorId int foreign key references BodyColors(BodyColorId) not null, 
	InteriorColorId int foreign key references InteriorColors(InteriorColorId) not null, 
	Mileage int not null, 
	SalePrice decimal(8,2) not null, 
	MSRP decimal(8,2) not null, 
	VehicleDescription nvarchar(100),
	IsNewType bit not null, 
	IsPurchased bit not null, 
	DealershipId int foreign key references Dealerships(DealershipId) not null, 
	SpecialId int foreign key references Specials(specialId),
	ImageFileLink varchar(50), 
)

if exists (select * from sys.tables where name = 'Purchase')
    drop table Purchase;
go

Create Table Purchase (
	PurchaseId int identity(1,1) Primary Key, 
	VehicleId int Foreign key references Vehicles(VehicleId) not null, 
	CustomerFirstName varchar(15) not null,
	CustomerLastName varchar(15) not null,
	Email nvarchar(30) not null, 
	Phone nvarchar(15) not null, 
	Street1 varchar(30) not null, 
	Street2 varchar(30), 
	City varchar(30) not null, 
	StateId varchar(2) foreign key references States(StateId) not null, 
	Zipcode varchar(5) not null, 
	PurchasePrice decimal(8, 2) not null, 
	PurchaseTypeId int foreign key references PurchaseTypes(PurchaseTypeId) not null, 
	UserId int foreign key references Users(UserId) not null, 
)


if exists (select * from sys.tables where name = 'ContactUs')
    drop table ContactUs;
go

Create Table ContactUs (
	ContactUsId int identity(1,1) Primary Key, 
	ContactName varChar(50) not null, 
	EMail varchar(30) not null, 
	Phone nvarchar(15), 
	Message nvarchar(150) not null, 
	DealershipId int foreign key references Dealerships(DealershipId) not null
)
















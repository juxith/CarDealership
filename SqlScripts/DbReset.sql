USE CarDealership
GO

if exists(Select * From INFORMATION_SCHEMA.Routines
where routine_name = 'DbReset')
drop procedure DbReset
go 

Create Procedure DbReset As
Begin

	Delete from Purchases;
	Delete from PurchaseTypes;
	Delete from ContactUs;
	Delete from Vehicles;
	Delete from BodyTypes;
	Delete from InteriorColors;
	Delete from BodyColors;
	Delete from Specials;
	Delete from Models;
	Delete from Makes;

	DBCC CHECKIDENT ('BodyColors', reseed, 1)
	Set Identity_Insert BodyColors on;
		insert into BodyColors(BodyColorId, ColorName)
		Values(1, 'Black'),
		(2, 'White')
	Set Identity_Insert BodyColors off;

	DBCC CHECKIDENT ('InteriorColors', reseed, 1)
	Set Identity_Insert InteriorColors on;
		insert into InteriorColors(InteriorColorId, InteriorColorName)
		Values(1, 'Black Leather'),
		(2, 'Gunmetal'),
		(3, 'White'),
		(4, 'Red Trim')
	Set Identity_Insert InteriorColors off;

	DBCC CHECKIDENT ('BodyTypes', reseed, 1)
	Set Identity_Insert BodyTypes on;
		insert into BodyTypes(BodyTypeId, BodyTypeName)
		Values(1, 'Truck'),
		(2, 'Sport'),
		(3, 'Sedan'),
		(4, 'Mid-Size'),
		(5, 'MiniVan'),
		(6, 'Convertable')
	Set Identity_Insert BodyTypes off;

	DBCC CHECKIDENT ('Makes', reseed, 1)
	Set Identity_Insert Makes on;
		insert into Makes(MakeId, MakeName, DateAdded, UserId)
		Values(1, 'Chevrolet', 2009/10/31, '2f097e01-4dc0-4b18-9a5c-affaef723a98'),
		(2, 'Subaru',2009/11/10, '2f097e01-4dc0-4b18-9a5c-affaef723a98')
	Set Identity_Insert Makes off;

	DBCC CHECKIDENT ('Models', reseed, 1)
	Set Identity_Insert Models on;
		insert into Models(ModelId, ModelName, MakeId, DateAdded, UserId)
		Values(1,'Silverado', 1, 2009/10/31, '2f097e01-4dc0-4b18-9a5c-affaef723a98'),
		(2,'Impreza WRX',2, 2010/03/16, '2f097e01-4dc0-4b18-9a5c-affaef723a98')
	Set Identity_Insert Models off;

	DBCC CHECKIDENT ('PurchaseTypes', reseed, 1)
	Set Identity_Insert PurchaseTypes on;
		insert into PurchaseTypes(PurchaseTypeId, PurchaseTypeName)
		Values(1,'Bank Finance'),
		(2,'Dealer Finance'),
		(3,'Cash')
	Set Identity_Insert PurchaseTypes off;

	DBCC CHECKIDENT ('Specials', reseed, 1)
	Set Identity_Insert Specials on;
		insert into Specials(SpecialId, [Title], SpecialDescription)
		Values(1,'All NEW trucks on sale','All NEW premium trucks on sale for the Christmas season. Answer Dad''s letters to Santa'),
		(2,'Spring Used Car Sale','Start off spring with an upgrade. Check out all our used cars today for title prices covered.')
	Set Identity_Insert Specials off;

	DBCC CHECKIDENT ('Vehicles', reseed, 1)
	Set Identity_Insert Vehicles on;
		insert into Vehicles(VehicleId,VinNumber, ModelId, VehicleYear, BodyTypeId, IsAutomatic, BodyColorId,InteriorColorId, Mileage, SalePrice,MSRP, VehicleDescription,IsNewType, IsPurchased, SpecialId, ImageFileLink)
		Values(1,'123456789101112123',1, '2010',1,1,1,1,150000,20000,25000,'A beautiful black car',0,1,null,'car.jpg'),
		(2,'123456789101112124',2, '2018',2,0,2,2,0,29000,33000,'A beautiful white car',1,0,null,'car.jpg')
	Set Identity_Insert Vehicles off;

	DBCC CHECKIDENT ('Purchases', reseed, 1)
	Set Identity_Insert Purchases on;
		insert into Purchases(PurchaseId, VehicleId, CustomerFirstName, CustomerLastName, Email, Phone, Street1, City, StateAbrv, Zipcode, PurchasePrice, PurchaseTypeId, UserId, DateOfPurchase)
		Values(1,1,'Robert','Reynolds','rr@gmail.com','111 111 1111','12345 Guild Drive','Arden Hills','MN','55378',19500,1,'ed4f2dc0-b14a-4c3a-9f3a-9eed9a4262df', 2017/08/10)
	Set Identity_Insert Purchases off;

	DBCC CHECKIDENT ('ContactUs', reseed, 1)
	Set Identity_Insert ContactUs on;
		insert into ContactUs(ContactUsId, ContactName, VehicleId, Email, Phone, Message)
		Values(1,'Judy Thao', null,'tsg@tsg.com','555 555 5555','I''m interested in '),
		(2,'Aj Rohde',1,'tsg@tsg.com','555 555 5555','I like that truck. Could you tell me more about it.')
	Set Identity_Insert ContactUs off;

End
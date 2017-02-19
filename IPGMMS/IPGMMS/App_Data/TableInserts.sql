-- ############# MemberLevels #############
INSERT INTO [dbo].[MemberLevels] (MLevel) VALUES ('Student Member');
INSERT INTO [dbo].[MemberLevels] (MLevel) VALUES ('IPG Member');
INSERT INTO [dbo].[MemberLevels] (MLevel) VALUES ('Certified Professional Groomer');
INSERT INTO [dbo].[MemberLevels] (MLevel) VALUES ('Certified Advanced Professional Groomer');
INSERT INTO [dbo].[MemberLevels] (MLevel) VALUES ('International Certified Master Groomer');
INSERT INTO [dbo].[MemberLevels] (MLevel) VALUES ('Approved Salon');
INSERT INTO [dbo].[MemberLevels] (MLevel) VALUES ('Approved School');
INSERT INTO [dbo].[MemberLevels] (MLevel) VALUES ('Member School');
INSERT INTO [dbo].[MemberLevels] (MLevel) VALUES ('Uncategorized');

-- ############# ContactTypes #############
INSERT INTO [dbo].[ContactTypes] (ContactType) Values ('Mailing');
INSERT INTO [dbo].[ContactTypes] (ContactType) Values ('Listing');


-- ############# Certificates #############
INSERT INTO [dbo].[Certificates](Certification) VALUES ('Certified Salon Professional');
INSERT INTO [dbo].[Certificates](Certification) VALUES ('Certified Professional Groomer');
INSERT INTO [dbo].[Certificates](Certification) VALUES ('Certified Advanced Professional Groomer');
INSERT INTO [dbo].[Certificates](Certification) VALUES ('International Certified Master Groomer');


-- ############# Members #############
INSERT INTO [dbo].[Members](UserName,MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website) VALUES('AdminAdmin','000','2000-1-1','2025-12-31',9,'Admin','','Admin','','');
INSERT INTO [dbo].[Members](UserName,MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website) VALUES ('Annaunk','','2016-1-1','2016-10-31',4,'Anna','','unknown','Blue''s Doggy Daycare','');
INSERT INTO [dbo].[Members](UserName,MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website) VALUES('MelanieAbb','','2015-1-1','2016-3-31',1,'Melanie','','Costello','','');
INSERT INTO [dbo].[Members](UserName,MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website) VALUES('MaokiAbe','','2016-1-1','2017-1-31',3,'Maoki','','Abe','Puff n Stuff Pets','');
INSERT INTO [dbo].[Members](UserName,MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website) VALUES('HirasAbr','','2016-1-1','2018-1-31',3,'Hiras','','Abraham','Hiras Shop','');
INSERT INTO [dbo].[Members](UserName,MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website) VALUES('MyleneAda','','2016-1-1','2020-1-31',5,'Mylene','','Adams','','');
INSERT INTO [dbo].[Members](UserName,MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website) VALUES('DuskAda','','2015-1-1','2015-3-31',2,'Dusk','','Adams','Pawprints','');
INSERT INTO [dbo].[Members](UserName,MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website) VALUES('GeorgeinaAda','1648!A1','2015-1-1','2015-1-31',2,'Georgina','','Adams','','');
INSERT INTO [dbo].[Members](UserName,MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website) VALUES('ElizabethAda','01-0841','2009-1-1','2015-6-30',5,'Elizabeth','','Adams','The Paw Spaw Mobile','www.thepawspayakima.com');
INSERT INTO [dbo].[Members](UserName,MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website) VALUES('SamAda','1536!A1','2015-3-8','2020-3-31',1,'Sam','','Adams','','');

-- ############# ContactInfo #############
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (2,'32 Carter st','Blue Ridge','GA','','','','Anna@pmail.com');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (3,'55 Harrisburg Run','Bradford','PA','','16701','','Mel@pmail.com');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (4,'3243 Murdock Ln','Duluth','GA','','30096','4044081446','Maoki@pmail.com');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (4,'1150 Jones Bridge Rd','Johns Creek','GA','','30022','','Maoki@pmail.com');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (5,'2535 Birchtree Dr','Murrieta','CA','','92563','','animalspaws@pmail.com');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (5,'2535 Birchtree Dr','Murrieta','CA','','92563','','animalspaws@pmail.com');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (6,'332 Norman Rd','Belmot, Lower Hutt','Wellington','NZ','5010','','');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (7,'932 Powderkeg Ln','Baltimore','MD','','21234','','Dusk@pmail.com');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (7,'932 PowerKeg Ln','Baltimore','MD','','21234','4443235555','Dusk@pmail.com');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (8,'22 Ash Rd','Newbold Coleorton','Leicestershire','UK','','','George@pmail.com');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (8,'22 Ash Rd','Newbold Coleorton','Leicestershire','UK','','07940926752','George@pmail.com');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (9,'2200 68th Ave','Yakima','WA','','98903','','thepaws@pmail.com');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (9,'2200 68th Ave','Yakima','WA','','98903','5555734000','thepaws@pmail.com');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (10,'106 Sheffy Rd,Birdwell','Barnsley','Sheffield','UK','S70 5UX','','sam@pmail.com');
INSERT INTO [dbo].[ContactInfo] (Member_ID,StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES (10,'106 Sheffy Rd,Birdwell','Barnsley','Sheffield','UK','S70 5UX','00781247444','sam@pmail.com');


-- ############# Contacts #############
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (2,1,1);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (3,2,1);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (4,3,1);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (4,4,2);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (5,5,1);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (5,6,2);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (6,7,1);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (7,8,1);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (7,9,2);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (8,10,1);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (8,11,2);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (9,12,1);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (9,13,2);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (10,14,1);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (10,15,2);


-- ############# MemberCertifications #############
INSERT INTO [dbo].[MemberCertifications] (Member_ID,Certificate_ID) VALUES (2,3);
INSERT INTO [dbo].[MemberCertifications] (Member_ID,Certificate_ID) VALUES (4,2);
INSERT INTO [dbo].[MemberCertifications] (Member_ID,Certificate_ID) VALUES (5,2);
INSERT INTO [dbo].[MemberCertifications] (Member_ID,Certificate_ID) VALUES (6,4);
INSERT INTO [dbo].[MemberCertifications] (Member_ID,Certificate_ID) VALUES (8,1);
INSERT INTO [dbo].[MemberCertifications] (Member_ID,Certificate_ID) VALUES (9,4);

-- #############  Username Bridge #################
--INSERT INTO [dbo].[UserNameBridges] (Member_UserName,AspNet_UserName) VALUES (2,2)

-- TO BE DETERMINED Need to be able to seed AspNetUsers
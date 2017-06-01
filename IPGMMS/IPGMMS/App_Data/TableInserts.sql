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
INSERT INTO [dbo].[Members](MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website,Identity_ID) VALUES('0010000','2000-1-1','2025-12-31',9,'Lucy','','Danger','','','');
INSERT INTO [dbo].[Members](MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website,Identity_ID) VALUES ('0132111','2016-1-1','2016-10-31',4,'Anna','','Halfway','Blue''s Doggy Daycare','','');
INSERT INTO [dbo].[Members](MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website,Identity_ID) VALUES('0132222','2015-1-1','2016-3-31',1,'Melanie','','Costello','','','');
INSERT INTO [dbo].[Members](MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website,Identity_ID) VALUES('0132333','2016-1-1','2017-1-31',3,'Maoki','','Abe','Puff n Stuff Pets','','');
INSERT INTO [dbo].[Members](MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website,Identity_ID) VALUES('0132444','2016-1-1','2018-1-31',3,'Hiras','','Abraham','Hiras Shop','','');
INSERT INTO [dbo].[Members](MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website,Identity_ID) VALUES('0443212','2016-1-1','2020-1-31',5,'Mylene','','Adams','','','');
INSERT INTO [dbo].[Members](MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website,Identity_ID) VALUES('0132555','2015-1-1','2015-3-31',2,'Dusk','','Adams','Pawprints','','');
INSERT INTO [dbo].[Members](MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website,Identity_ID) VALUES('0354313','2015-1-1','2015-1-31',2,'Georgina','','Adams','','','');
INSERT INTO [dbo].[Members](MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website,Identity_ID) VALUES('0132677','2009-1-1','2015-6-30',5,'Elizabeth','','Adams','The Paw Spaw Mobile','http://www.thepawspayakima.com','');
INSERT INTO [dbo].[Members](MemberShip_Number,Membership_SignupDate,Membership_ExpirationDate,MemberLevel,FirstName,MiddleName,LastName,BusinessName,Website,Identity_ID) VALUES('0354414','2015-3-8','2020-3-31',1,'Sam','','Adams','','','');

-- ############# ContactInfo #############
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('32 Carter st','Blue Ridge','GA','US','30513','5555555554','Anna@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('55 Harrisburg Run','Bradford','PA','US','16701','5555555678','Mel@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('3243 Murdock Ln','Duluth','GA','US','30096','4044081446','Maoki@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('1150 Jones Bridge Rd','Johns Creek','GA','US','30022','','Maoki@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('2535 Birchtree Dr','Murrieta','CA','US','92563','5555550987','animalspaws@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('2535 Birchtree Dr','Murrieta','CA','US','92563','5555550987','animalspaws@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('332 Norman Rd','Belmot, Lower Hutt','Wellington','NZ','5010','5555554321','adams@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('932 Powderkeg Ln','Baltimore','MD','US','21234','4443235555','Dusk@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('932 PowerKeg Ln','Baltimore','MD','US','21234','4443235555','Dusk@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('22 Ash Rd','Newbold Coleorton','Leicestershire','UK','LE67 8PH','07940926752','George@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('22 Ash Rd','Newbold Coleorton','Leicestershire','UK','LE67 8PH','07940926752','George@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('2200 68th Ave','Yakima','WA','US','98903','5555734000','thepaws@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('2200 68th Ave','Yakima','WA','US','98903','5555734000','thepaws@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('106 Sheffy Rd,Birdwell','Barnsley','Sheffield','UK','S70 5UX','00781247444','sam@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('106 Sheffy Rd,Birdwell','Barnsley','Sheffield','UK','S70 5UX','00781247444','sam@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('32 Carter st','Blue Ridge','GA','US','30513','5555555554','Anna@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('55 Harrisburg Run','Bradford','PA','US','16701','5555555678','Mel@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('332 Norman Rd','Belmot, Lower Hutt','Wellington','NZ','5010','5555554321','adams@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('345 Monmouth Ave N','Monmouth','OR','US','97361','5555551234','danger@pmail.com');
INSERT INTO [dbo].[ContactInfo] (StreetAddress,City,StateName,Country,PostalCode,PhoneNumber,Email) VALUES ('345 Monmouth Ave N','Monmouth','OR','US','97361','5555551234','danger@pmail.com');

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
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (2,16,2);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (3,17,2);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (6,18,2);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (1,19,1);
INSERT INTO [dbo].[Contacts] (Member_ID,ContactInfo_ID,ContactType_ID) VALUES (1,20,2);

-- ############# MemberCertifications #############
INSERT INTO [dbo].[MemberCertifications] (Member_ID,Certificate_ID) VALUES (2,3);
INSERT INTO [dbo].[MemberCertifications] (Member_ID,Certificate_ID) VALUES (4,2);
INSERT INTO [dbo].[MemberCertifications] (Member_ID,Certificate_ID) VALUES (5,2);
INSERT INTO [dbo].[MemberCertifications] (Member_ID,Certificate_ID) VALUES (6,4);
INSERT INTO [dbo].[MemberCertifications] (Member_ID,Certificate_ID) VALUES (8,1);
INSERT INTO [dbo].[MemberCertifications] (Member_ID,Certificate_ID) VALUES (9,4);
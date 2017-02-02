-- ########### User ###########
INSERT INTO [dbo].[User](FName,LName,UserName)VALUES('Mr','Anon','Anonymous');

-- ########### Story ###########

INSERT INTO [dbo].[Story](UserID,Title,Body,Summary,PostDate)VALUES(1,'First','This is my first post ever!','First Post','2016-11-21');

-- ########### UserKey ###########

INSERT INTO [dbo].[UserKey](UserID,UKey)VALUES(1,00000000);

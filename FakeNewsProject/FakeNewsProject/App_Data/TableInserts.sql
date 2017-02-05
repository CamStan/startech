-- ########### User ###########
INSERT INTO [dbo].[User](FName,LName,UserName)VALUES('Mr','Anon','Anonymous');

-- ########### Story ###########

INSERT INTO [dbo].[Story](UserID,Title,Body,Summary,PostDate)VALUES(1,'First','This is my first post ever!','First Post','2016-11-21');
INSERT INTO [dbo].[Story](UserID,Title,Body,Summary,PostDate)VALUES(1,'Funny Animals','A bunch of animals of a certain type did a funny thing','Animal Post','2017-1-21');
INSERT INTO [dbo].[Story](UserID,Title,Body,Summary,PostDate)VALUES(1,'Fitness or Bust','Tried the treadmill, fell on my face.','Oops','2017-1-11');
INSERT INTO [dbo].[Story](UserID,Title,Body,Summary,PostDate)VALUES(1,'Tech Heartbreaks','A robot ran off with my girlfriend','Tech Woes','2016-11-21');
INSERT INTO [dbo].[Story](UserID,Title,Body,Summary,PostDate)VALUES(1,'Can This Really be Called Sports','New study finds that falling down drunk at the sports bar does not count as sports related injury','Sports Injury','2016-12-21');
INSERT INTO [dbo].[Story](UserID,Title,Body,Summary,PostDate)VALUES(1,'Politic Rage','They will cause massive destruction and have cats and dogs living together!','Us vs Them','2017-1-25');

-- ########### UserKey ###########

INSERT INTO [dbo].[UserKey](UserID,UKey)VALUES(1,00000000);

-- ########### Tag ###########

INSERT INTO [dbo].[Tag](Name)VALUES('Politics');
INSERT INTO [dbo].[Tag](Name)VALUES('Sports');
INSERT INTO [dbo].[Tag](Name)VALUES('Technology');
INSERT INTO [dbo].[Tag](Name)VALUES('Fitness');
INSERT INTO [dbo].[Tag](Name)VALUES('Animals');

-- ########### StoryTag ###########

INSERT INTO [dbo].[StoryTag](StoryID,TagID)VALUES(1,1);
INSERT INTO [dbo].[StoryTag](StoryID,TagID)VALUES(2,5);
INSERT INTO [dbo].[StoryTag](StoryID,TagID)VALUES(3,4);
INSERT INTO [dbo].[StoryTag](StoryID,TagID)VALUES(4,3);
INSERT INTO [dbo].[StoryTag](StoryID,TagID)VALUES(5,2);
INSERT INTO [dbo].[StoryTag](StoryID,TagID)VALUES(6,1);
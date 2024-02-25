CREATE TABLE Catalog (
	Id INT IDENTITY(1,1) not null, 
	[Name] varchar(max) null, 
	[Path] varchar(max) null, 
	[Type] varchar(max) null, 
	[Parent_Id] INT null);

INSERT INTO [Catalog] VALUES('CommonFiles', '/RootFolder/CommonFiles', 'Folder', 1)
INSERT INTO [Catalog] VALUES('SystemFiles', '/RootFolder/SystemFiles', 'Folder', 1)
INSERT INTO [Catalog] VALUES('Images', '/RootFolder/CommonFiles/Images', 'Folder', 2)
INSERT INTO [Catalog] VALUES('Download', '/RootFolder/Download', 'Folder', 2)
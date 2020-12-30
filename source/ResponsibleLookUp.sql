
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'ResponsibleLookUp')) 
 BEGIN

 CREATE TABLE ResponsibleLookUp (
  [Id] int check ([Id] > 0) IDENTITY(1,1) NOT NULL,
  [Title] nvarchar(512) NOT NULL,
  [OrderNumber] int check ([OrderNumber] > 0) DEFAULT NULL,
  [CreatedById] int check ([CreatedById] > 0) DEFAULT NULL,
  [CreatedDate] datetime2(0) DEFAULT NULL,
  [ModifiedById] int check ([ModifiedById] > 0) DEFAULT NULL,
  [ModifiedDate] datetime2(0) DEFAULT NULL,
  [Active] bit NOT NULL DEFAULT 1,
  PRIMARY KEY ([Id])
 );

END;
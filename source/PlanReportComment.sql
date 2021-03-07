  
  IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'PlanReportComment')) 
 BEGIN

 CREATE TABLE PlanReportComment (
  [Id] int check ([Id] > 0) IDENTITY(1,1) NOT NULL,
  [LevelId] int check ([LevelId] > 0) NOT NULL,
  [Strengths] nvarchar(max) NULL,
  [OFI] nvarchar(max) NULL,
  [CreatedById] int check ([CreatedById] > 0) DEFAULT NULL,
  [CreatedDate] datetime2(0) DEFAULT NULL,
  [ModifiedById] int check ([ModifiedById] > 0) DEFAULT NULL,
  [ModifiedDate] datetime2(0) DEFAULT NULL,
  [Active] bit NOT NULL DEFAULT 1,
  PRIMARY KEY ([Id]),
  CONSTRAINT [FK_PlanReportComment_Level_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Level] ([LevelId])
);

END;

IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'StakeholderLevel')) 
 BEGIN

 CREATE TABLE StakeholderLevel (
  [StakeholderLevelId] int check ([StakeholderLevelId] > 0) IDENTITY(1,1) NOT NULL,
  [UserId] int check ([UserId] > 0)  NOT NULL,
  [LevelId] int check ([LevelId] > 0)  NOT NULL,
  [CreatedById] int check ([CreatedById] > 0) DEFAULT NULL,
  [CreatedDate] datetime2(0) DEFAULT NULL,
  [ModifiedById] int check ([ModifiedById] > 0) DEFAULT NULL,
  [ModifiedDate] datetime2(0) DEFAULT NULL,
  [Active] bit NOT NULL DEFAULT 1,
  PRIMARY KEY ([StakeholderLevelId]),
  CONSTRAINT [FK_StakeholderLevel_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES ApplicationUser ([UserId]),
  CONSTRAINT [FK_StakeholderLevel_Level_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Level] ([LevelId])
);

END;
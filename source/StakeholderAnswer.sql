
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'StakeholderAnswer')) 
 BEGIN

 CREATE TABLE StakeholderAnswer (
  [Id] int check ([Id] > 0) IDENTITY(1,1) NOT NULL,
  [QuestionId] int check ([QuestionId] > 0) NOT NULL,
  [AnswerValue] nvarchar(max) NOT NULL,
  [LevelType] int check ([LevelType] > 0) NOT NULL,
  [StakeholderId] int check ([StakeholderId] > 0) NOT NULL,
  [LevelId] int check ([LevelId] > 0) NOT NULL,
  [QuestionaireTemplateId] int check ([QuestionaireTemplateId] > 0) NOT NULL,
  [CreatedById] int check ([CreatedById] > 0) DEFAULT NULL,
  [CreatedDate] datetime2(0) DEFAULT NULL,
  [ModifiedById] int check ([ModifiedById] > 0) DEFAULT NULL,
  [ModifiedDate] datetime2(0) DEFAULT NULL,
  [Active] bit NOT NULL DEFAULT 1,
  PRIMARY KEY ([Id]),
  CONSTRAINT [FK_StakeholderAnswer_Question_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES Question ([Id]),
  CONSTRAINT [FK_StakeholderAnswer_LevelTypeLookUp_LevelType] FOREIGN KEY ([LevelType]) REFERENCES LevelTypeLookUp ([Id]),
   CONSTRAINT [FK_StakeholderAnswer_QuestionaireTemplate_QuestionaireTemplateId] FOREIGN KEY ([QuestionaireTemplateId]) REFERENCES QuestionnaireTemplate ([Id]),
  CONSTRAINT [FK_StakeholderAnswer_ApplicationUser_UserId] FOREIGN KEY ([StakeholderId]) REFERENCES ApplicationUser ([UserId]),
  CONSTRAINT [FK_StakeholderAnswer_Level_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Level] ([LevelId])

 );

END;


IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'StakeholderQuestionnaireStatus')) 
 BEGIN

 CREATE TABLE StakeholderQuestionnaireStatus (
  [Id] int check ([Id] > 0) IDENTITY(1,1) NOT NULL,
  [QuestionaireTemplateId] int check ([QuestionaireTemplateId] > 0) NOT NULL,
  [IsSubmit] bit NOT NULL DEFAULT 1,
  [StakeholderId] int check ([StakeholderId] > 0) NOT NULL,
  [LevelId] int check ([LevelId] > 0) NOT NULL,
  [CreatedById] int check ([CreatedById] > 0) DEFAULT NULL,
  [CreatedDate] datetime2(0) DEFAULT NULL,
  [ModifiedById] int check ([ModifiedById] > 0) DEFAULT NULL,
  [ModifiedDate] datetime2(0) DEFAULT NULL,
  [Active] bit NOT NULL DEFAULT 1,
  PRIMARY KEY ([Id]),
  CONSTRAINT [FK_StakeholderQuestionnaireStatus_QuestionaireTemplate_QuestionaireTemplateId] FOREIGN KEY ([QuestionaireTemplateId]) REFERENCES QuestionnaireTemplate ([Id]),
  CONSTRAINT [FK_StakeholderQuestionnaireStatus_ApplicationUser_UserId] FOREIGN KEY ([StakeholderId]) REFERENCES ApplicationUser ([UserId]),
  CONSTRAINT [FK_StakeholderQuestionnaireStatus_Level_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [Level] ([LevelId])
 );

END;

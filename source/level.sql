
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'Level')) 
 BEGIN

 CREATE TABLE Level (
  [LevelId] int check ([LevelId] > 0) IDENTITY(1,1) NOT NULL,
  [LevelName] varchar(256) DEFAULT NULL,
  [ParentId] int check ([ParentId] > 0) DEFAULT NULL,
  [QuestionaireTemplateId] int check ([QuestionaireTemplateId] > 0) DEFAULT NULL,
  [DeadlineDate] datetime2(0) DEFAULT NULL,
  [RenewalDate] datetime2(0) DEFAULT NULL,
  [CreatedById] int check ([CreatedById] > 0) DEFAULT NULL,
  [CreatedDate] datetime2(0) DEFAULT NULL,
  [ModifiedById] int check ([ModifiedById] > 0) DEFAULT NULL,
  [ModifiedDate] datetime2(0) DEFAULT NULL,
  [Active] bit NOT NULL DEFAULT 1,
  PRIMARY KEY ([LevelId]),
  CONSTRAINT [FK_level_level_ParentId] FOREIGN KEY ([ParentId]) REFERENCES level ([LevelId]),
  CONSTRAINT [FK_level_QuestionaireTemplate_QuestionaireTemplateId] FOREIGN KEY ([QuestionaireTemplateId]) REFERENCES QuestionnaireTemplate ([Id])
);

END;
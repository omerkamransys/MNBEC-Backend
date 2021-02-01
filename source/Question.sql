
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'Question')) 
 BEGIN

 CREATE TABLE Question (
  [Id] int check ([Id] > 0) IDENTITY(1,1) NOT NULL,
  [QuestionaireTemplateId] int check ([QuestionaireTemplateId] > 0) NOT NULL,
  [Area] int check ([Area] > 0) DEFAULT NULL,
  [FourP] int check ([FourP] > 0) DEFAULT NULL,
  [Responsible] int check ([Responsible] > 0) DEFAULT NULL,
  [Level] int check ([Level] > 0) DEFAULT NULL,
  [OrderNumber] int check ([OrderNumber] > 0) DEFAULT NULL,
  [Level0] nvarchar(max) NOT NULL,
  [Level1] nvarchar(max) NOT NULL,
  [Level2] nvarchar(max) NOT NULL,
  [Level3] nvarchar(max) NOT NULL,
  [Level4] nvarchar(max) NOT NULL,
  [Element] nvarchar(max) NOT NULL,
  [CreatedById] int check ([CreatedById] > 0) DEFAULT NULL,
  [CreatedDate] datetime2(0) DEFAULT NULL,
  [ModifiedById] int check ([ModifiedById] > 0) DEFAULT NULL,
  [ModifiedDate] datetime2(0) DEFAULT NULL,
  [Active] bit NOT NULL DEFAULT 1,
  PRIMARY KEY ([Id]),
  CONSTRAINT [FK_Question_QuestionaireTemplate_QuestionaireTemplateId] FOREIGN KEY ([QuestionaireTemplateId]) REFERENCES QuestionnaireTemplate ([Id]),
  CONSTRAINT [FK_Question_AreaLookUp_Area] FOREIGN KEY ([Area]) REFERENCES AreaLookUp ([Id]),
  CONSTRAINT [FK_Question_FourPLookUp_FourP] FOREIGN KEY ([FourP]) REFERENCES FourPLookUp ([Id]),
  CONSTRAINT [FK_Question_Applicationrole_Responsible] FOREIGN KEY ([Responsible]) REFERENCES  [Applicationrole] ([RoleId]),
  CONSTRAINT [FK_Question_LevelLookUp_Level] FOREIGN KEY ([Level]) REFERENCES LevelLookUp ([Id])
 );

END;


ALTER TABLE Question
ADD Level4  nvarchar(max) NOT NULL Default '';

ALTER TABLE Question
ADD Element nvarchar(max) NOT NULL Default '';

ALTER TABLE [Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Applicationrole_Responsible] FOREIGN KEY([Responsible])
REFERENCES [Applicationrole] ([RoleId])
GO
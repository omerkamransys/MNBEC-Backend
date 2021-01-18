DROP procedure if exists [StakeholderAnswerAdd];
GO


-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE StakeholderAnswerAdd(	
 @PCurrentUserId int,
 @PId	int OUT,
 @PQuestionId int,
 @PAnswerValue	nvarchar(max),
 @PLevelType int,
 @PStakeholderId int,
 @PLevelId int,
 @PQuestionaireTemplateId int
)
As
Begin
Set nocount on;
	Insert Into StakeholderAnswer
				(QuestionId, AnswerValue, LevelType, StakeholderId, LevelId, QuestionaireTemplateId, CreatedById,  CreatedDate,  Active)
		Values	(@PQuestionId, @PAnswerValue, @PLevelType, @PStakeholderId,@PLevelId, @PQuestionaireTemplateId, @PCurrentUserId, GETUTCDATE(), 1);

	Set @PId = SCOPE_IDENTITY();
End;

GO
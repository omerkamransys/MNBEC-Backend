DROP procedure if exists [StakeholderQuestionnaireStatusAdd];
GO


-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE StakeholderQuestionnaireStatusAdd(	
 @PCurrentUserId int,
 @PId	int OUT,
 @PQuestionaireTemplateId int,
 @PStakeholderId int,
 @PLevelId int
)
As
Begin
Set nocount on;
	Insert Into StakeholderQuestionnaireStatus
				(QuestionaireTemplateId, StakeholderId, LevelId, CreatedById,  CreatedDate,  Active)
		Values	(@PQuestionaireTemplateId, @PStakeholderId, @PLevelId, @PCurrentUserId, GETUTCDATE(), 1);

	Set @PId = SCOPE_IDENTITY();
End;

GO
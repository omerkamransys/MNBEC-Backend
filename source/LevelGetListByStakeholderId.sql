DROP procedure if exists [LevelGetListByStakeholderId];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE LevelGetListByStakeholderId
(
	@PStakeholderId int
)
As
Begin
Set nocount on;

	Select l.LevelId,l.LevelName,l.ParentId,qt.Id as QuestionaireTemplateId, 
	qt.Title as QuestionaireTemplateName , shqs.IsSubmit  from StakeholderLevel sl with (nolock)
	inner join level l with (nolock) on sl.LevelId = l.LevelId and sl.Active = 1 and l.Active = 1
	inner join QuestionnaireTemplate qt with (nolock) on l.QuestionaireTemplateId = qt.Id
	left join StakeholderQuestionnaireStatus shqs with (nolock) on  shqs.QuestionaireTemplateId = qt.Id
	and shqs.LevelId = l.LevelId and shqs.StakeholderId = @PStakeholderId
	where sl.UserId = @PStakeholderId and sl.Active = 1 and l.Active = 1;

	
End;

GO
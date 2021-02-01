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

	Select Distinct l.LevelId,l.LevelName,l.ParentId,qt.Id as QuestionaireTemplateId, 
	qt.Title as QuestionaireTemplateName , shqs.IsSubmit  
	from level l with (nolock)
	inner join QuestionnaireTemplate qt with (nolock) on l.QuestionaireTemplateId = qt.Id
	and l.Active = 1 and qt.Active = 1
	inner join Question q with (nolock) on qt.Id = q.QuestionaireTemplateId and q.Active = 1
	left join StakeholderLevel sl with (nolock) on sl.LevelId = l.LevelId and sl.Active = 1
	left join StakeholderQuestionnaireStatus shqs with (nolock) on  shqs.QuestionaireTemplateId = qt.Id
	and shqs.LevelId = l.LevelId and shqs.StakeholderId = @PStakeholderId
	where (sl.UserId = @PStakeholderId and sl.Active = 1 and q.Responsible is null) or 
	(q.Responsible in (Select RoleId from applicationuserrole where UserId = @PStakeholderId
		and Active = 1))  ;

End;

GO
DROP procedure if exists [StakeholderAnswerGetListByStakeholderId];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE StakeholderAnswerGetListByStakeholderId
(
	@PStakeholderId int,
	@PLevelId int,
	@PQuestionaireTemplateId int
)
As
Begin
Set nocount on;

	
Select sha.id, q.id as QuestionId, sha.AnswerValue,sha.LevelType
	,sha.StakeholderId,l.LevelId, qt.id as QuestionaireTemplateId 
	,q.Level0,q.Level1,q.Level2,q.Level3,q.Level4 , q.Element, alu.Title as AreaLookUpTitle
	from Question q with (nolock)
	inner join QuestionnaireTemplate qt  with (nolock) on q.QuestionaireTemplateId = qt.Id
	and qt.Id = @PQuestionaireTemplateId
	inner join Level l  with (nolock) on qt.Id = l.QuestionaireTemplateId
	and l.LevelId = @PLevelId
	left join StakeholderAnswer sha  with (nolock) on q.Id = sha.QuestionId
	and qt.Id = sha.QuestionaireTemplateId and l.LevelId = sha.LevelId
	and sha.StakeholderId = @PStakeholderId
	left join AreaLookUp alu  with (nolock) on q.Area = alu.Id
	where q.Responsible in ((Select RoleId from applicationuserrole where UserId = @PStakeholderId
		and Active = 1)) or (q.Responsible is null AND @PStakeholderId = (Select top 1 UserId from StakeholderLevel where LevelId = @PLevelId and UserId = @PStakeholderId and Active = 1))


End;

GO

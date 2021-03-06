DROP procedure if exists [SP_GetReportList];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE SP_GetReportList
(
	@PLevelId int,
	@PQuestionaireTemplateId int
)
As
Begin
Set nocount on;

select q.FourP,
4 As [Max], (Sum(q.DesiredLevel)/COUNT(q.ID)) as Desired, (SUM(lt.score) / COUNT(q.ID)) As [Current]
 from StakeholderAnswer ans with (nolock)
	inner join Question q on q.Id = ans.QuestionId
		inner join LevelTypeLookUp lt on ans.LevelType = lt.Id
	where ans.QuestionaireTemplateId = @PQuestionaireTemplateId and ans.LevelId = @PLevelId
	group by q.FourP 

	select CONCAT(fp.Title,' - ',q.Element) as Title,
Max(q.DesiredLevel) as Desired, (SUM(lt.score) / COUNT(q.ID)) As [Current]
 from StakeholderAnswer ans with (nolock)
	inner join Question q on q.Id = ans.QuestionId
		inner join LevelTypeLookUp lt on ans.LevelType = lt.Id
		inner join FourPLookUp fp on q.FourP = fp.Id
	where ans.QuestionaireTemplateId = @PQuestionaireTemplateId and ans.LevelId = @PLevelId
	group by CONCAT(fp.Title,' - ',q.Element) 
 


End;

GO

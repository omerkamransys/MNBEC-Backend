﻿DROP procedure if exists [SP_GetReportList];

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

	
select q.FourP,SUM(lt.score) as [sum],COUNT(q.ID) as [count] from StakeholderAnswer ans with (nolock)
	inner join Question q on q.Id = ans.QuestionId
		inner join LevelTypeLookUp lt on ans.LevelType = lt.Id
	where ans.QuestionaireTemplateId = @PQuestionaireTemplateId and ans.LevelId = @PLevelId
	group by q.FourP 
 


End;

GO
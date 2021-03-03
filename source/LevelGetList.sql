DROP procedure if exists [LevelGetList];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE LevelGetList
As
Begin
Set nocount on;

	Select 
		tbl.LevelId, tbl.LevelName, tbl.ParentId, tbl.QuestionaireTemplateId,
		tbl.DeadlineDate, tbl.RenewalDate, tbl.Active,tbl.WF
	From level tbl 
	Where tbl.Active = 1;

	select ReviewerLevelId, UserId, LevelId
	from ReviewerLevel
	where Active = 1;

	select StakeholderLevelId, UserId, LevelId
	from StakeholderLevel
	where Active = 1;

End;

GO
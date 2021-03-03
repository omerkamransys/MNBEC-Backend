DROP procedure if exists [LevelGet];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE LevelGet(	
 @PLevelId int
)
As
Begin
Set nocount on;
	Select 
		tbl.LevelId, tbl.LevelName, tbl.ParentId, tbl.QuestionaireTemplateId,
		tbl.DeadlineDate, tbl.RenewalDate,  tbl.Active, tbl.WF
	From level tbl 
	Where tbl.LevelId = @PLevelId;

	select ReviewerLevelId, UserId, LevelId
	from ReviewerLevel
	where LevelId = @PLevelId and Active = 1;

	select StakeholderLevelId, UserId, LevelId
	from StakeholderLevel
	where LevelId = @PLevelId and Active = 1;

End;

GO
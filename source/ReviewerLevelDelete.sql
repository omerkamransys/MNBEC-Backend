DROP procedure if exists [ReviewerLevelDelete];
GO


-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE ReviewerLevelDelete(
 @PCurrentUserId int,
 @PLevelId	int
)
As
Begin
Set nocount on;

	Update ReviewerLevel
		Set 
			Active = 0,
			ModifiedById = @PCurrentUserId,
			ModifiedDate = GETUTCDATE()
	Where  LevelId = @PLevelId;
End;

GO

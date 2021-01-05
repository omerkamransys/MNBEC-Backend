DROP procedure if exists [StakeholderLevelDelete];
GO


-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE StakeholderLevelDelete(
 @PCurrentUserId int,
 @PLevelId	int
)
As
Begin
Set nocount on;

	Update StakeholderLevel
		Set 
			Active = 0,
			ModifiedById = @PCurrentUserId,
			ModifiedDate = GETUTCDATE()
	Where  LevelId = @PLevelId;
End;

GO

DROP procedure if exists [LevelActivate];
GO


-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE LevelActivate(
 @PCurrentUserId int,
 @PLevelId	int,
 @PActive bit
)
As
Begin
Set nocount on;
	Update level 
		Set 
			ModifiedById = @PCurrentUserId,
			ModifiedDate = GETUTCDATE(),
			Active = @PActive
	Where LevelId = @PLevelId;
End;

GO

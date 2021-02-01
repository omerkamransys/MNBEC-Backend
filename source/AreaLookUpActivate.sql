DROP procedure if exists [AreaLookUpActivate];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE AreaLookUpActivate(	
 @PCurrentUserId int,
 @PId	int,
 @PActive bit
)
As
Begin
Set nocount on;
	Update AreaLookUp 
		Set 
			ModifiedById = @PCurrentUserId,
			ModifiedDate = GETUTCDATE(),
			Active = @PActive
			where id = @PId;
End;

GO

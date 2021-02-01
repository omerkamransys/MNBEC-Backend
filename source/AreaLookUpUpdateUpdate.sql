DROP procedure if exists [AreaLookUpUpdateUpdate];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE AreaLookUpUpdateUpdate(	
 @PCurrentUserId int,
 @PId	int,
 @PTitle	nvarchar(512),
 @PActive bit
)
As
Begin
Set nocount on;
	Update AreaLookUp 
		Set 
			Title = @PTitle,
			ModifiedById = @PCurrentUserId,
			ModifiedDate = GETUTCDATE(),
			Active = @PActive
			where id = @PId;
End;

GO

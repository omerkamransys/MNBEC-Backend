
DROP procedure if exists [AreaLookUpAdd];
GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE AreaLookUpAdd(	
 @PCurrentUserId int,
 @PId	int OUT,
 @PTitle	nvarchar(512)
)
As
Begin
Set nocount on;
	Insert Into AreaLookUp
				(Title, CreatedById,  CreatedDate,  Active)
		Values	(@PTitle, @PCurrentUserId, GETUTCDATE(), 1);

	Set @PId = SCOPE_IDENTITY();
End;

GO


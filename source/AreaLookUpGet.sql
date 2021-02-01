

DROP procedure if exists [AreaLookUpGet];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE AreaLookUpGet(	
 @PId int
)
As
Begin
Set nocount on;
	Select 
		Id, Title, Active
	From AreaLookUp with(nolock)
	Where Id = @PId;

End;

GO
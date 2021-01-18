DROP procedure if exists [LevelTypeLookUpGetList];
GO

-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE  PROCEDURE LevelTypeLookUpGetList
As
Begin
Set nocount on;

    Select 
        tbl.id, tbl.Title
	From LevelTypeLookUp tbl
	Where tbl.Active = 1
    Order By tbl.OrderNumber Asc;

End;

GO
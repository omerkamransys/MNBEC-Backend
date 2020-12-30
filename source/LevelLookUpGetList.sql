DROP procedure if exists [LevelLookUpGetList];
GO

-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE  PROCEDURE LevelLookUpGetList
As
Begin
Set nocount on;

    Select 
        tbl.id, tbl.Title
	From LevelLookUp tbl
	Where tbl.Active = 1
    Order By tbl.OrderNumber Asc;

End;

GO
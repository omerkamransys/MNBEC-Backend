DROP procedure if exists [AreaLookUpGetList];
GO

-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE  PROCEDURE AreaLookUpGetList
As
Begin
Set nocount on;

    Select 
        tbl.id, tbl.Title
	From AreaLookUp tbl
	Where tbl.Active = 1
    Order By tbl.OrderNumber Asc;

End;

GO
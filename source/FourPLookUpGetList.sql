DROP procedure if exists [FourPLookUpGetList];
GO

-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE  PROCEDURE FourPLookUpGetList
As
Begin
Set nocount on;

    Select 
        tbl.id, tbl.Title
	From FourPLookUp tbl
	Where tbl.Active = 1
    Order By tbl.OrderNumber Asc;

End;

GO
DROP procedure if exists [ResponsibleLookUpGetList];
GO

-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE  PROCEDURE ResponsibleLookUpGetList
As
Begin
Set nocount on;

    Select 
        tbl.id, tbl.Title
	From ResponsibleLookUp tbl
	Where tbl.Active = 1
    Order By tbl.OrderNumber Asc;

End;

GO
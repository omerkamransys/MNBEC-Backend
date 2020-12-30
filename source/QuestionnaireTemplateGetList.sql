DROP procedure if exists [QuestionnaireTemplateGetList];
GO

-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE  PROCEDURE QuestionnaireTemplateGetList(	
 @PCurrentUserId int 
	)
As
Begin
Set nocount on;

    Select 
        tbl.id, tbl.Title
	From QuestionnaireTemplate tbl
	Where tbl.Active = 1
    Order By tbl.id Asc;

End;

GO
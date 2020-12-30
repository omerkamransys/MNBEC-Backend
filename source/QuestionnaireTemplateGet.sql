DROP procedure if exists [QuestionnaireTemplateGet];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE QuestionnaireTemplateGet(	
 @PId int
)
As
Begin
Set nocount on;
	Select 
		tbl.Id, tbl.Title
	From QuestionnaireTemplate tbl with(nolock)
	Where tbl.Id = @PId;

End;

GO
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

	Select 
		Id, QuestionaireTemplateId, Area, FourP, Responsible, Level, Level0, Level1, Level2, Level3,Level4,Element, OrderNumber, Active, DesiredLevel
	From Question with(nolock)
	Where QuestionaireTemplateId = @PId and Active = 1;

End;

GO
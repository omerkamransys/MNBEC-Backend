DROP procedure if exists [QuestionGet];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE QuestionGet(	
 @PId int
)
As
Begin
Set nocount on;
	Select 
		Id, QuestionaireTemplateId, Area, FourP, Responsible, Level, Level0, Level1, Level2, Level3, Level4, OrderNumber, Active
	From Question with(nolock)
	Where Id = @PId;

End;

GO
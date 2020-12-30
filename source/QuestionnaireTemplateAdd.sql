DROP procedure if exists [QuestionnaireTemplateAdd];
GO


-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE QuestionnaireTemplateAdd(	
 @PCurrentUserId int,
 @PId	int OUT,
 @PTitle	nvarchar(256)
)
As
Begin
Set nocount on;
	Insert Into QuestionnaireTemplate
				(Title, CreatedById,  CreatedDate,  Active)
		Values	(@PTitle, @PCurrentUserId, GETUTCDATE(), 1);

	Set @PId = SCOPE_IDENTITY();
End;

GO
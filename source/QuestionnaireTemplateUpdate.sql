DROP procedure if exists [QuestionnaireTemplateUpdate];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE QuestionnaireTemplateUpdate(	
 @PCurrentUserId int,
 @PId	int,
 @PTitle	nvarchar(256)
)
As
Begin
Set nocount on;
	Update QuestionnaireTemplate 
		Set 
			Title = @PTitle,
			ModifiedById = @PCurrentUserId,
			ModifiedDate = GETUTCDATE()
			where id = @PId;
End;

GO
DROP procedure if exists [QuestionnaireTemplateActivate];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE QuestionnaireTemplateActivate(	
 @PCurrentUserId int,
 @PId	int,
 @PActive bit
	)
As
Begin
Set nocount on;
	Update QuestionnaireTemplate
		Set 
			ModifiedById=@PCurrentUserId,
			ModifiedDate=GETUTCDATE(),
			Active=@PActive
			where id = @PId;

End;

GO

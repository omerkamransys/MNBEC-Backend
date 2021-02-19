DROP procedure if exists [QuestionDesiredLevelChange];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE QuestionDesiredLevelChange(	
 @PCurrentUserId int,
 @PId	int,
 @PDesiredLevel bit
	)
As
Begin
Set nocount on;
	Update Question
		Set 
			ModifiedById = @PCurrentUserId,
			ModifiedDate=GETUTCDATE(),
			DesiredLevel = @PDesiredLevel
			where id = @PId;

End;

GO
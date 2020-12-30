DROP procedure if exists [QuestionActivate];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE QuestionActivate(	
 @PCurrentUserId int,
 @PId	int,
 @PActive bit
	)
As
Begin
Set nocount on;
	Update Question
		Set 
			ModifiedById=@PCurrentUserId,
			ModifiedDate=GETUTCDATE(),
			Active=@PActive
			where id = @PId;

End;

GO
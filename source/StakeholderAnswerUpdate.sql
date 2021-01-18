DROP procedure if exists [StakeholderAnswerUpdate];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE StakeholderAnswerUpdate(	
 @PCurrentUserId int,
 @PId	int,
 @PAnswerValue	nvarchar(max),
 @PLevelType int
)
As
Begin
Set nocount on;
	Update StakeholderAnswer 
		Set 
			AnswerValue = @PAnswerValue,
			LevelType = @PLevelType,
			ModifiedById = @PCurrentUserId,
			ModifiedDate = GETUTCDATE()
			where id = @PId;
End;

GO
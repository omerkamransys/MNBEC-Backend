DROP procedure if exists [LevelUpdate];
GO


-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE LevelUpdate(
 @PCurrentUserId int,
 @PLevelId	int,
 @PLevelName nvarchar(256),
 @PParentId	int,
 @PQuestionaireTemplateId int,
 @PDeadlineDate datetime2(0),
 @PRenewalDate datetime2(0),
 @PWF decimal(18,4)
)
As
Begin
Set nocount on;

	Update level
		Set 
			ParentId = @PParentId,
			LevelName = @PLevelName,
			QuestionaireTemplateId = @PQuestionaireTemplateId,
			DeadlineDate = @PDeadlineDate,
			RenewalDate = @PRenewalDate,
			WF = @PWF,
			ModifiedById = @PCurrentUserId,
			ModifiedDate = GETUTCDATE()
	Where  LevelId = @PLevelId;
End;

GO
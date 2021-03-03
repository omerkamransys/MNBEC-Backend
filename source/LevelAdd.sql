

DROP procedure if exists [levelAdd];
GO


-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE levelAdd(
 @PCurrentUserId int,
 @PLevelId	int OUT,
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
	Insert Into level
				(LevelName, ParentId, QuestionaireTemplateId, DeadlineDate, RenewalDate, CreatedById,  CreatedDate,  Active, WF)
		Values	(@PLevelName, @PParentId, @PQuestionaireTemplateId, @PDeadlineDate, @PRenewalDate, @PCurrentUserId, GETUTCDATE(), 1, @PWF);

	Set @PLevelId = SCOPE_IDENTITY();
End;

GO
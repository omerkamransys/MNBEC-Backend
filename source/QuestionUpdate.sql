DROP procedure if exists [QuestionUpdate];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE QuestionUpdate(	
 @PCurrentUserId int,
 @PId	int,
 @PQuestionaireTemplateId int,
 @PArea int,
 @PFourP int,
 @PResponsible int,
 @PLevel int,
 @PLevel0	nvarchar(max),
 @PLevel1	nvarchar(max),
 @PLevel2	nvarchar(max),
 @PLevel3	nvarchar(max),
 @PActive bit
)
As
Begin
Set nocount on;
	Update Question 
		Set 
			QuestionaireTemplateId = @PQuestionaireTemplateId,
			Area = @PArea,
			FourP = @PFourP,
			Responsible = @PResponsible,
			Level = @PLevel,
			Level0 = @PLevel0,
			Level1 = @PLevel1,
			Level2 = @PLevel2,
			Level3 = @PLevel3,
			ModifiedById = @PCurrentUserId,
			ModifiedDate = GETUTCDATE(),
			Active = @PActive
			where id = @PId;
End;

GO
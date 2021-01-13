DROP procedure if exists [QuestionAdd];
GO


-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE QuestionAdd(	
 @PCurrentUserId int,
 @PId	int OUT,
 @PQuestionaireTemplateId int,
 @PArea int,
 @PFourP int,
 @PResponsible int,
 @PLevel int,
 @PLevel0	nvarchar(max),
 @PLevel1	nvarchar(max),
 @PLevel2	nvarchar(max),
 @PLevel3	nvarchar(max),
 @PLevel4	nvarchar(max)
)
As
Begin
Set nocount on;
	Insert Into Question
				(QuestionaireTemplateId, Area, FourP, Responsible, Level, Level0, Level1, Level2, Level3, Level4, CreatedById,  CreatedDate,  Active, OrderNumber)
		Values	(@PQuestionaireTemplateId, @PArea, @PFourP, @PResponsible, @PLevel, @PLevel0, @PLevel1, @PLevel2, @PLevel3, @PLevel4, @PCurrentUserId, GETUTCDATE(), 1, SCOPE_IDENTITY());

	Set @PId = SCOPE_IDENTITY();
End;

GO

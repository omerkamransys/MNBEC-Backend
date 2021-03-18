DROP procedure if exists [AddPlanReportComment];
GO


-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE AddPlanReportComment(
 @PCurrentUserId int,
 @PId	int OUT,
 @PLevelId	int,
 @PStrengths	nvarchar(max),
 @POFI	nvarchar(max),
 @POpportunities	nvarchar(max),
 @PRecommendations	nvarchar(max),
 @PConclusion	nvarchar(max)
)
As
Begin
Set nocount on;
	Insert Into PlanReportComment
				(LevelId, Strengths, OFI,Opportunities,Recommendations,Conclusion, CreatedById,  CreatedDate,  Active)
		Values	(@PLevelId, @PStrengths, @POFI,@POpportunities,@PRecommendations,@PConclusion, @PCurrentUserId, GETUTCDATE(), 1);

	Set @PId = SCOPE_IDENTITY();
End;

GO
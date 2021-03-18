DROP procedure if exists [UpdatePlanReportComment];
GO


-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE UpdatePlanReportComment(
 @PCurrentUserId int,
 @PId	int,
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
	
	
	Update PlanReportComment 
		Set 
			Strengths=@PStrengths,
			OFI = @POFI,
			Opportunities = @POpportunities,
			Recommendations = @PRecommendations,
			Conclusion = @PConclusion,
			ModifiedById = @PCurrentUserId,
			ModifiedDate = GETUTCDATE()
	Where LevelId = @PLevelId

End;

GO
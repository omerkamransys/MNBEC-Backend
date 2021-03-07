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
 @POFI	nvarchar(max)
)
As
Begin
Set nocount on;
	
	
	Update PlanReportComment 
		Set 
			Strengths=@PStrengths,
			OFI = @POFI,
			ModifiedById = @PCurrentUserId,
			ModifiedDate = GETUTCDATE()
	Where LevelId = @PLevelId

End;

GO
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
 @POFI	nvarchar(max)
)
As
Begin
Set nocount on;
	Insert Into PlanReportComment
				(LevelId, Strengths, OFI, CreatedById,  CreatedDate,  Active)
		Values	(@PLevelId, @PStrengths, @POFI, @PCurrentUserId, GETUTCDATE(), 1);

	Set @PId = SCOPE_IDENTITY();
End;

GO
﻿DROP procedure if exists [GetPlanReportComment];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE GetPlanReportComment(	
 @PLevelId int
)
As
Begin
Set nocount on;
	Select 
		 tbl.Strengths, tbl.OFI
	From PlanReportComment tbl 
	Where tbl.LevelId = @PLevelId;


End;

GO
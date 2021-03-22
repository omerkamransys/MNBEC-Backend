DROP procedure if exists [SP_GetParentReportList];

GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================

CREATE PROCEDURE SP_GetParentReportList
(
	@PLevelId int
)
As
Begin
Set nocount on;

DECLARE @LOCAL_TABLEVARIABLE Table
(LevelId int, 
ParentId int,
 LevelName varchar(250),
 wf decimal(18,0)
);

WITH cte AS 
 (
  SELECT a.LevelId, a.parentId, a.LevelName, a.wf
  FROM [level] a
  WHERE LevelId = @PLevelId
  UNION ALL
  SELECT a.LevelId, a.parentId, a.LevelName, a.wf
  FROM [Level] a JOIN cte c ON a.parentId = c.LevelId
  )
  insert into @LOCAL_TABLEVARIABLE
  SELECT  LevelId,parentId, LevelName , wf
  FROM cte 
  
  --insert into @LOCAL_TABLEVARIABLE select * from cte;
select lc.LevelId,q.FourP,lc.ParentId,lc.LevelName,lc.wf,
4 As [Max], (Sum(q.DesiredLevel)/COUNT(q.ID)) as Desired, (SUM(lt.score) / COUNT(q.ID)) As [Current]
 from @LOCAL_TABLEVARIABLE lc 
		left join StakeholderAnswer ans on lc.LevelId = ans.LevelId
		left join Question q on q.Id = ans.QuestionId
		left join LevelTypeLookUp lt on ans.LevelType = lt.Id
	--where  ans.LevelId = @PLevelId
	group by lc.LevelId, q.FourP ,lc.ParentId,lc.LevelName,lc.wf


End;

GO

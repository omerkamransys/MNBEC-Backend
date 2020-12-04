##################################LikeDislike################################################

DROP procedure IF EXISTS `LikeDislikeAdd`;

DELIMITER $$

CREATE  PROCEDURE `LikeDislikeAdd`(	
	IN PCurrentUserId int unsigned,
    IN PUserId int unsigned,
	INOUT PLikeDislikeId	int unsigned,    
	IN PValue	boolean,    
    IN PMenuItemName	varchar(256)
	)
Begin
	Insert Into LikeDislike
				(UserId,MenuItemName,Value,  CreatedById,  CreatedDate,  Active)
		Values	(PUserId,PMenuItemName,PValue, PCurrentUserId, utc_timestamp(), 1);

	Set PLikeDislikeId=last_insert_id();   
End$$

DELIMITER ;

##################################Favourite################################################

   
DROP procedure IF EXISTS `favouriteadd`;

DELIMITER $$

CREATE PROCEDURE `favouriteadd`(	
	IN PFavouriteId int unsigned,
	IN PSelector varchar(256),
	IN PCurrentUserId int unsigned,
    IN PUserId int unsigned,
    IN PMenuId int unsigned,
	IN PActive	boolean,    
    IN PMenuItemName	varchar(256),
    IN PDisplayName	varchar(256)
	)
Begin

	SET PFavouriteId = ifnull(PFavouriteId,0);

IF PFavouriteId = 0 THEN
		IF NOT EXISTS (select FavouriteId from Favourite where UserId = PUserId and Active = 1 and MenuItemName = PMenuItemName  ) THEN		
			Insert Into Favourite
						(UserId,MenuId,MenuItemName, DisplayName, Selector,CreatedById, CreatedDate, Active)
				Values	(PCurrentUserId,PMenuId,PMenuItemName,PDisplayName,PSelector, PCurrentUserId, utc_timestamp(), PActive); 
		END IF;
	ELSE
    
		UPDATE Favourite
        SET	Active = 0
        WHERE FavouriteId = PFavouriteId;
        
    END IF;

End$$

DELIMITER ;



DROP procedure IF EXISTS `favouriteGetlist`;

DELIMITER $$

CREATE  PROCEDURE `favouriteGetlist`(	
	
    IN PUserId int unsigned
	
	)
Begin	
select fvr.FavouriteId,fvr.UserId,fvr.MenuItemName,ifnull(fvr.DisplayName,fvr.MenuItemName) as DisplayName,fvr.Selector,
			scr.IsPowerBIReport, scr.ReportId,scr.ReportName,scr.EmbedUrl,scr.DatasetId,scr.GroupId
    from Favourite fvr 
	inner join menu men on men.MenuId = fvr.MenuId
	inner join screen scr on scr.ScreenId = men.ScreenId
	
	where fvr.active = 1 AND fvr.UserId = PUserId;	

End$$

DELIMITER ;


DROP procedure IF EXISTS `favouriteUpdate`;

DELIMITER $$

CREATE  PROCEDURE `favouriteUpdate`(	
	IN PFavouriteId int unsigned,
	IN PCurrentUserId int unsigned,
    IN PUserId int unsigned,
	IN PActive	boolean,    
    IN PMenuItemName	varchar(256),
    IN PDisplayName	varchar(256),
    IN PSelector	varchar(256)
	)
Begin  

			Update Favourite
            SET DisplayName = PDisplayName,            
				MenuItemName = PMenuItemName,
				Selector = PSelector,
                ModifiedById = PCurrentUserId,
				ModifiedDate = utc_timestamp()
			WHERE FavouriteId = PFavouriteId;

End$$

DELIMITER ;



DROP procedure IF EXISTS `favouriteActivate`;

DELIMITER $$

CREATE  PROCEDURE `favouriteActivate`(	
	IN PFavouriteId int unsigned,
	IN PCurrentUserId int unsigned,
	IN PActive	boolean 
	)
Begin  

			Update Favourite
            SET Active = 0,            				
                ModifiedById = PCurrentUserId,
				ModifiedDate = utc_timestamp()
			WHERE FavouriteId = PFavouriteId;

End$$

DELIMITER ;


##################################Menu################################################



Drop Procedure If Exists `menuAdd`;
DELIMITER $$
# =============================================
# Author:		
# Create date: June, 03, 2020
# Description:	menuAdd adds new record in menu
# =============================================
Create Procedure `menuAdd`
(	IN PCurrentUserId int unsigned,
	INOUT PMenuId	int unsigned,
	IN PParentId	int unsigned,
	IN PScreenId	int unsigned,
	IN PClientId	int unsigned,
	IN PName	varchar(100),
	IN PGermanName	varchar(100),
	IN PChineseName	varchar(100),
	IN PDescription	varchar(100),
	IN PIcon	varchar(40),
	IN PContent	text,
	IN PSelector	varchar(40),
	IN POrderNumber	int unsigned,
	IN PActive	bit
	)
Begin
	SET POrderNumber = (select MAX(OrderNumber) + 1 from menu);
	Insert Into menu
				(ParentId,  ScreenId,ClientConfigurationId,  ClientId,  Name,GermanName,ChineseName,  Icon,  Content,  Selector,Description,  OrderNumber,  CreatedById,  CreatedDate,  Active)
		Values	(PParentId, PScreenId, PClientId,1, PName,PGermanName,PChineseName, PIcon, PContent, PSelector,PDescription, POrderNumber, PCurrentUserId, utc_timestamp(), 1);

	Set PMenuId=last_insert_id();
End$$
DELIMITER ;

Drop Procedure If Exists `menuGet`;
DELIMITER $$
# =============================================
# Author:		
# Create date: June, 03, 2020
# Description:	menuGet gets related record from menu
# =============================================
Create Procedure `menuGet`
(	
	IN PMenuId	int unsigned
	)
Begin
	Select 
		tbl.MenuId, tbl.ParentId, tbl.ScreenId, tbl.ClientId, tbl.Name,tbl.GermanName,tbl.ChineseName, scr.Name as 'OrignalName',
		tbl.Icon, tbl.Content, tbl.Description, scr.Selector, tbl.OrderNumber, tbl.Active,
		scr.IsPowerBIReport, scr.ReportId,scr.ReportName,scr.EmbedUrl,scr.DatasetId,scr.GroupId
	From menu tbl
	INNER JOIN screen scr on scr.screenId = tbl.screenId
	Where  tbl.MenuId=PMenuId;

End$$
DELIMITER ;

Drop Procedure If Exists `menuGetList`;
DELIMITER $$
# =============================================
# Author:		
# Create date: June, 03, 2020
# Description:	menuGetList gets all records from menu
# =============================================
Create Procedure `menuGetList`
(	IN PCurrentUserId int unsigned	,
	IN PClientId int unsigned	
)
Begin
	Select 
		tbl.MenuId, tbl.ParentId, tbl.ScreenId, tbl.ClientId, tbl.Name,tbl.GermanName,tbl.ChineseName, scr.Name as 'OrignalName',
		tbl.Icon, tbl.Content, tbl.Description, scr.Selector, tbl.OrderNumber, tbl.Active,
		scr.IsPowerBIReport, scr.ReportId,scr.ReportName,scr.EmbedUrl,scr.DatasetId,scr.GroupId
	From menu tbl
	INNER JOIN screen scr on scr.screenId = tbl.screenId
	Where tbl.Active = 1 AND tbl.ClientConfigurationId = PClientId;

End$$
DELIMITER ;

Drop Procedure If Exists `menuUpdate`;
DELIMITER $$
# =============================================
# Author:		
# Create date: June, 03, 2020
# Description:	menuUpdate updates record in menu
# =============================================
Create Procedure `menuUpdate`
(	IN PCurrentUserId int unsigned,
	IN PMenuId	int unsigned,
	IN PParentId	int unsigned,
	IN PScreenId	int unsigned,
	IN PClientId	int unsigned,
	IN PName	varchar(100),
	IN PGermanName	varchar(100),
	IN PChineseName	varchar(100),
	IN PDescription	varchar(100),
	IN PIcon	varchar(40),
	IN PContent	text,
	IN PSelector	varchar(40),
	IN POrderNumber	int unsigned,
	IN PActive	bit
	)
Begin
	Update menu tbl
		Set 
			tbl.ParentId=PParentId,
			tbl.ScreenId=PScreenId,
			tbl.ClientId=1,
			tbl.ClientConfigurationId=PClientId,
			tbl.Name=PName,
			tbl.GermanName=PGermanName,
			tbl.ChineseName=PChineseName,
			tbl.Description=PDescription,
			tbl.Icon=PIcon,
			tbl.Content=PContent,
			tbl.Selector=PSelector,
			#tbl.OrderNumber=POrderNumber,
			tbl.ModifiedById=PCurrentUserId,
			tbl.ModifiedDate=utc_timestamp()
	Where tbl.MenuId=PMenuId;
End$$
DELIMITER ;

Drop Procedure If Exists `menuActivate`;
DELIMITER $$
# =============================================
# Author:		
# Create date: June, 03, 2020
# Description:	menuActivate deletes record from menu
# =============================================
Create Procedure `menuActivate`
(	IN PCurrentUserId int unsigned,
	IN PActive bit,
	IN PMenuId	int unsigned
	)
Begin
	Update menu tbl
		Set 
			tbl.ModifiedById=PCurrentUserId,
			tbl.ModifiedDate=utc_timestamp(),
			tbl.Active=PActive
	Where tbl.MenuId=PMenuId;
End$$
DELIMITER ;


##################################Screen################################################


Drop Procedure If Exists `ScreenGetList`;
DELIMITER $$
# =============================================
# Author:		
# Create date: June, 03, 2020
# Description:	ScreenGetList gets all records from screen
# =============================================
Create Procedure `ScreenGetList`
(	IN PCurrentUserId int unsigned
	)
Begin
	Select sql_calc_found_rows
		tbl.ScreenId, tbl.ParentId, tbl.ClientId, tbl.Name, tbl.Icon, tbl.Selector, tbl.Content, tbl.Description, tbl.OrderNumber, tbl.Active
	From screen tbl order by tbl.Name;

End$$

DELIMITER ;

DROP procedure IF EXISTS `SynchronizeWithPowerBI`;

DELIMITER $$

CREATE  PROCEDURE `SynchronizeWithPowerBI`(
INOUT PScreenId	int unsigned,
      PReportId varchar(100),
      PReportName varchar(100),
      PEmbedUrl varchar(100),
      PWebUrl varchar(100),
      PDatasetId varchar(100),
	  PGroupId varchar(100)
)
BEGIN 

	IF NOT EXISTS (select ScreenId from screen where Name = PName) THEN

		INSERT INTO Screen(IsPowerBiReport,Selector,ReportId,ReportName,EmbedUrl,DatasetId,Name,ClientId,Active,GroupId)
					VALUES (1,'app-dynamic-report', PReportId,PReportName,PEmbedUrl,PDatasetId,PName,1,1,PGroupId);
		
	END if;   

END$$

DELIMITER ;





##################################UserActivity################################################

DROP procedure IF EXISTS `UserActivityAdd`;

DELIMITER $$

CREATE  PROCEDURE `UserActivityAdd`(	
	
    IN PUserId int unsigned,
	IN PUserActivityName	varchar(256),    	
	IN PUserActivityDescription	varchar(256)
	)
Begin
	Insert Into UserActivity
				(UserId,UserActivityName,UserActivityDescription, CreatedDate, Active)
		Values	(PUserId,PUserActivityName,PUserActivityDescription,  utc_timestamp(), 1);	
End$$

DELIMITER ;

DROP procedure IF EXISTS `UserActivityGetList`;

DELIMITER $$

CREATE  PROCEDURE `UserActivityGetList`(	
	
    IN PUserId int unsigned,
	IN PTotalRecords	int unsigned
	)
Begin
		
		select ua.UserId,ua.UserActivityName,ua.UserActivityDescription,ua.CreatedDate,au.UserName
		from UserActivity ua
		inner join applicationuser au on au.UserId = ua.UserId
		where ua.UserId = CASE WHEN  PUserId = 0 THEN ua.UserId ELSE PUserId END
		Order by CreatedDate desc LIMIT 20;
			
End$$

DELIMITER ;


##################################Announcement################################################

DROP procedure if exists `DealerAnnouncementAdd`;

DELIMITER $$

# =============================================
# Author:		MM
# Create date:23/4/2020
# =============================================

Create Procedure `DealerAnnouncementAdd`
	(	
	IN PCurrentUserId int unsigned,
	INOUT PAnnouncementId	int unsigned,
	IN PContent text,
	IN PIsPublish bit
  
	)
Begin

     INSERT INTO announcement (Content,IsPublish,CreatedById,CreatedDate,Active)
		VALUES (PContent,PIsPublish,PCurrentUserId,utc_timestamp(),1);
    
	Set PAnnouncementId=last_insert_id();
 
End$$

DELIMITER ;


DROP procedure if exists `DealerAnnouncementUpdate`;


DELIMITER $$

# =============================================
# Author:		MM
# Create date:23/4/2020
# =============================================
Create Procedure `DealerAnnouncementUpdate`
(	IN PCurrentUserId int unsigned,
	IN PAnnouncementId	int unsigned,
	IN PContent text,
	IN PIsPublish bit
	)
Begin
	
	
	update announcement set 
    Content = PContent, IsPublish = PIsPublish,
    ModifiedById = PCurrentUserId, ModifiedDate = utc_timestamp()
    where DealerAnnouncementId = PAnnouncementId;
    
End$$

DELIMITER ;


DROP procedure if exists `DealerAnnouncementDelete`;


DELIMITER $$

# =============================================
# Author:		MM
# Create date:23/4/2020
# =============================================
Create Procedure `DealerAnnouncementDelete`
(	IN PCurrentUserId int unsigned,
	IN PAnnouncementId	int unsigned
	)
Begin

	update announcement set 
    active = 0,
    ModifiedById = PCurrentUserId, ModifiedDate = utc_timestamp()
    where DealerAnnouncementId = PAnnouncementId;
End$$

DELIMITER ;


Drop Procedure If Exists `DealerAnnouncementGetAll`;
DELIMITER $$
# =============================================
# Author:		Zulqarnain Saleem 
# Create date: April, 20, 2020
# Description:	Dealer Announcement GetAll from DealerAnnouncement
# =============================================
Create Procedure `DealerAnnouncementGetAll`(
	INOUT PTotalRecord int unsigned,
    IN PIsPublish bool,
    IN POffset int unsigned,
	IN PPageSize int unsigned
	)

Begin
	Select sql_calc_found_rows
		tbl.DealerAnnouncementId, 
        tbl.Content,
        tbl.IsPublish,
        tbl.OrderNumber
	From announcement tbl
	Where tbl.Active = 1
    and CASE WHEN PIsPublish = 1
			THEN tbl.IsPublish = 1 ELSE 1 = 1 END
            
	order by tbl.OrderNumber,tbl.DealerAnnouncementId desc
	limit PPageSize offset POffset
    ;
    set PTotalRecord = found_rows();


End$$
DELIMITER ;


###################################################################################################



########################UserInterfaceActivity#########################


Drop Procedure If Exists `UserInterfaceActivityAdd`;
DELIMITER $$
# =============================================
# Author:		Omer
# Create date: May, 23, 2020
# Description:	UserInterfaceActivityAdd deletes record from itemmaster
# =============================================
Create Procedure `UserInterfaceActivityAdd`
(	INOUT PUserInterfaceActivityId int unsigned,
	IN PCurrentUserId int unsigned,
	IN PUserId int unsigned,
	IN PMenuId int unsigned
	)
Begin
	INSERT INTO UserInterfaceActivity (UserId,MenuId,CreatedById,Active)
								VALUES (PUserId, PMenuId,PCurrentUserId,1 );
			
		SET PUserInterfaceActivityId = last_insert_id();   
End$$
DELIMITER ;


Drop Procedure If Exists `UserInterfaceActivityGet`;
DELIMITER $$
# =============================================
# Author:		Omer
# Create date: May, 23, 2020
# Description:	UserInterfaceActivityGet deletes record from itemmaster
# =============================================
Create Procedure `UserInterfaceActivityGet`
(	
	IN PUserId int unsigned,
	IN PMenuId int unsigned
	)
Begin
	select tbl.UserId, tbl.MenuId, m.Name as MenuName, COUNT(tbl.UserInterfaceActivityId) as Counter
	from UserInterfaceActivity tbl
	INNER JOIN Menu m on m.MenuId = tbl.MenuId
	WHERE tbl.UserId = PUserId AND tbl.MenuId = PMenuId
	
	Group By tbl.MenuId;
								
End$$
DELIMITER ;




Drop Procedure If Exists `UserInterfaceActivityGetAll`;
DELIMITER $$
# =============================================
# Author:		Omer
# Create date: May, 23, 2020
# Description:	UserInterfaceActivityGetAll deletes record from itemmaster
# =============================================
Create Procedure `UserInterfaceActivityGetAll`
(	
	IN PUserId int unsigned
)
Begin
	select tbl.UserId, tbl.MenuId, m.Name as MenuName, COUNT(tbl.UserInterfaceActivityId) as Counter
	from UserInterfaceActivity tbl
	INNER JOIN Menu m on m.MenuId = tbl.MenuId
	WHERE tbl.UserId = PUserId 
	Group By tbl.MenuId;
											  
End$$
DELIMITER ;


#################################################Note####################################################

Drop Procedure If Exists `noteAdd`;
DELIMITER $$
# =============================================
# Author:		
# Create date: June, 11, 2020
# Description:	noteAdd adds new record in note
# =============================================
Create Procedure `noteAdd`
(	IN PCurrentUserId int unsigned,
	INOUT PNoteId	int unsigned,
	IN PUserId	int unsigned,
	IN PNoteName	varchar(100),
	IN PContent	text,	
	IN PActive	bit
	)
Begin
	Insert Into note
				(UserId,  NoteName,  Content,  CreatedById,  CreatedDate,  Active)
		Values	(PUserId, PNoteName, PContent, PCurrentUserId, utc_timestamp(), 1);

	Set PNoteId=last_insert_id();
End$$
DELIMITER ;

Drop Procedure If Exists `noteGet`;
DELIMITER $$
# =============================================
# Author:		
# Create date: June, 11, 2020
# Description:	noteGet gets related record from note
# =============================================
Create Procedure `noteGet`
(	IN PUserId int unsigned,
	IN PNoteId	int unsigned
	)
Begin
	Select 
		tbl.NoteId, tbl.UserId, tbl.NoteName, tbl.Content
	From note tbl
	Where tbl.NoteId=PNoteId;

End$$
DELIMITER ;

Drop Procedure If Exists `noteGetList`;
DELIMITER $$
# =============================================
# Author:		
# Create date: June, 11, 2020
# Description:	noteGetAll gets all records from note
# =============================================
Create Procedure `noteGetList`
(	IN PUserId int unsigned
	
	)
Begin
	Select 
		tbl.NoteId, tbl.UserId, tbl.NoteName, tbl.Content
	From note tbl
	Where tbl.Active = 1 AND tbl.UserId = PUserId;

End$$
DELIMITER ;

Drop Procedure If Exists `noteUpdate`;
DELIMITER $$
# =============================================
# Author:		
# Create date: June, 11, 2020
# Description:	noteUpdate updates record in note
# =============================================
Create Procedure `noteUpdate`
(	IN PCurrentUserId int unsigned,
	IN PNoteId	int unsigned,
	IN PUserId	int unsigned,
	IN PNoteName	varchar(100),
	IN PContent	text,
	IN PActive	bit
	)
Begin
	Update note tbl
		Set 
			
			tbl.NoteName=PNoteName,
			tbl.Content=PContent,			
			tbl.ModifiedById=PCurrentUserId,
			tbl.ModifiedDate=utc_timestamp()
	Where tbl.NoteId=PNoteId;
End$$
DELIMITER ;

Drop Procedure If Exists `noteActivate`;
DELIMITER $$
# =============================================
# Author:		
# Create date: June, 11, 2020
# Description:	noteActivate deletes record from note
# =============================================
Create Procedure `noteActivate`
(	IN PCurrentUserId int unsigned,
	IN PActive bit,
	IN PNoteId	int unsigned
	)
Begin
	Update note tbl
		Set 
			tbl.ModifiedById=PCurrentUserId,
			tbl.ModifiedDate=utc_timestamp(),
			tbl.Active=PActive
	Where tbl.NoteId=PNoteId;
End$$
DELIMITER ;



#################################################applicationinsights####################################################

Drop Procedure If Exists `applicationinsightsAdd`;
DELIMITER $$
# =============================================
# Author:		Omer Kamran
# Create date: October, 07, 2020
# Description:	applicationinsightsAdd adds new record in applicationinsights
# =============================================
Create Procedure `applicationinsightsAdd`
(	IN PCurrentUserId int unsigned,
	INOUT PApplicationInsightsId	int unsigned,
	IN PDateGenerated	datetime,
	IN PExpiryDate	datetime,
	IN PExpiryCondition	varchar(256),
	IN PAppInsightsCountryId	int unsigned,
	IN PAppInsightsRetailerId	int unsigned,
	IN PAppInsightsCategoryId	int unsigned,
	IN PAppInsightsDivisionId	int unsigned,
	IN PLeftImage	varchar(256),
	IN PRightImage	varchar(256),
	IN PValue	decimal(18,4),
	IN PDescription	varchar(500),
	IN PSuggestedAction	varchar(500),
	IN PActive	bit,
    IN PIsExpired	bit,
	IN PIsPublish	bit
	)
Begin
	Insert Into applicationinsights
				(DateGenerated,  ExpiryDate,  ExpiryCondition,  AppInsightsCountryId,  AppInsightsRetailerId,  AppInsightsCategoryId,  AppInsightsDivisionId,  LeftImage,  RightImage,  Value,  Description,  SuggestedAction,IsExpired,IsPublish,  CreatedById,  CreatedDate,  Active)
		Values	(PDateGenerated, PExpiryDate, PExpiryCondition, PAppInsightsCountryId, PAppInsightsRetailerId, PAppInsightsCategoryId, PAppInsightsDivisionId, PLeftImage, PRightImage, PValue, PDescription, PSuggestedAction,PIsExpired,PIsPublish, PCurrentUserId, utc_timestamp(), PActive);

	Set PApplicationInsightsId=last_insert_id();
End$$
DELIMITER ;

Drop Procedure If Exists `applicationinsightsGet`;
DELIMITER $$
# =============================================
# Author:		Omer Kamran
# Create date: October, 07, 2020
# Description:	applicationinsightsGet gets related record from applicationinsights
# =============================================
Create Procedure `applicationinsightsGet`
(	
	IN PApplicationInsightsId	int unsigned
	)
Begin
	Select 
		tbl.ApplicationInsightsId, tbl.DateGenerated, tbl.ExpiryDate, tbl.ExpiryCondition, tbl.AppInsightsCountryId, tbl.AppInsightsRetailerId, tbl.AppInsightsCategoryId, tbl.AppInsightsDivisionId, tbl.LeftImage, tbl.RightImage, tbl.Value, tbl.Description, tbl.SuggestedAction,tbl.IsExpired,tbl.IsPublish, tbl.CreatedById, tbl.CreatedDate, tbl.ModifiedById, tbl.ModifiedDate, tbl.Active
	From applicationinsights tbl
	Where tbl.ApplicationInsightsId=PApplicationInsightsId;
    
    Select 
		tbl1.MetricApplicationInsightsId, tbl1.ApplicationInsightsId, tbl1.Value, tbl1.MetricApplicationDescription
        from MetricApplicationInsights tbl1
	Where  tbl1.ApplicationInsightsId=PApplicationInsightsId;

End$$
DELIMITER ;


Drop Procedure If Exists `applicationinsightsGetList`;
DELIMITER $$
# =============================================
# Author:		Omer Kamran
# Create date: October, 07, 2020
# Description:	applicationinsightsGet gets related record from applicationinsights
# =============================================
Create Procedure `applicationinsightsGetList`
(	
	IN PCurrentUserId	int unsigned
	)
Begin
	Select 
		tbl.ApplicationInsightsId, tbl.DateGenerated, tbl.ExpiryDate, tbl.ExpiryCondition, tbl.AppInsightsCountryId, tbl.AppInsightsRetailerId, tbl.AppInsightsCategoryId, tbl.AppInsightsDivisionId, tbl.LeftImage, tbl.RightImage, tbl.Value, tbl.Description, tbl.SuggestedAction,tbl.IsExpired,tbl.IsPublish, tbl.CreatedById, tbl.CreatedDate, tbl.ModifiedById, tbl.ModifiedDate, tbl.Active
	From applicationinsights tbl
	Where tbl.Active=1;
    
    Select 
		tbl.MetricApplicationInsightsId, tbl.ApplicationInsightsId, tbl.Value, tbl.MetricApplicationDescription
        from MetricApplicationInsights tbl;

End$$
DELIMITER ;


Drop Procedure If Exists `applicationinsightsUpdate`;
DELIMITER $$
# =============================================
# Author:		Omer Kamran
# Create date: October, 07, 2020
# Description:	applicationinsightsUpdate updates record in applicationinsights
# =============================================
Create Procedure `applicationinsightsUpdate`
(	IN PCurrentUserId int unsigned,
	IN PApplicationInsightsId	int unsigned,
	IN PDateGenerated	datetime,
	IN PExpiryDate	datetime,
	IN PExpiryCondition	varchar(256),
	IN PAppInsightsCountryId	int unsigned,
	IN PAppInsightsRetailerId	int unsigned,
	IN PAppInsightsCategoryId	int unsigned,
	IN PAppInsightsDivisionId	int unsigned,
	IN PLeftImage	varchar(256),
	IN PRightImage	varchar(256),
	IN PValue	decimal(18,4),
	IN PDescription	varchar(500),
	IN PSuggestedAction	varchar(500),
	IN PActive	bit,
	IN PIsExpired	bit,
	IN PIsPublish	bit
	)
Begin
	Update applicationinsights tbl
		Set 
			tbl.DateGenerated=PDateGenerated,
			tbl.ExpiryDate=PExpiryDate,
			tbl.ExpiryCondition=PExpiryCondition,
			tbl.AppInsightsCountryId=PAppInsightsCountryId,
			tbl.AppInsightsRetailerId=PAppInsightsRetailerId,
			tbl.AppInsightsCategoryId=PAppInsightsCategoryId,
			tbl.AppInsightsDivisionId=PAppInsightsDivisionId,
			tbl.LeftImage=PLeftImage,
			tbl.RightImage=PRightImage,
			tbl.Value=PValue,
			tbl.Description=PDescription,
			tbl.SuggestedAction=PSuggestedAction,
			tbl.ModifiedById=PCurrentUserId,
			tbl.ModifiedDate=utc_timestamp(),
			tbl.Active=PActive,
            tbl.IsExpired=PIsExpired,
            tbl.IsPublish=PIsPublish
	Where tbl.ApplicationInsightsId=PApplicationInsightsId;
End$$
DELIMITER ;


Drop Procedure If Exists `applicationinsightsPublish`;
DELIMITER $$
# =============================================
# Author:		Omer Kamran
# Create date: October, 07, 2020
# Description:	applicationinsightsActivate deletes record from applicationinsights
# =============================================
Create Procedure `applicationinsightsPublish`
(	IN PCurrentUserId int unsigned,	
	IN PIsPublish bit,
	IN PApplicationInsightsId	int unsigned
	)
Begin
	Update applicationinsights tbl
		Set 
			tbl.ModifiedById=PCurrentUserId,
			tbl.ModifiedDate=utc_timestamp(),
			tbl.IsPublish=PIsPublish
	Where tbl.ApplicationInsightsId=PApplicationInsightsId;
End$$
DELIMITER ;

Drop Procedure If Exists `applicationinsightsActivate`;
DELIMITER $$
# =============================================
# Author:		Omer Kamran
# Create date: October, 07, 2020
# Description:	applicationinsightsActivate deletes record from applicationinsights
# =============================================
Create Procedure `applicationinsightsActivate`
(	IN PCurrentUserId int unsigned,	
	IN PActive bit,
	IN PApplicationInsightsId	int unsigned
	)
Begin
	Update applicationinsights tbl
		Set 
			tbl.ModifiedById=PCurrentUserId,
			tbl.ModifiedDate=utc_timestamp(),
			tbl.Active=PActive
	Where tbl.ApplicationInsightsId=PApplicationInsightsId;
End$$
DELIMITER ;


Drop Procedure If Exists `MetricDelete`;
DELIMITER $$
# =============================================
# Author:		Omer Kamran
# Create date: October, 07, 2020
# Description:	metricapplicationinsightsActivate deletes record from metricapplicationinsights
# =============================================
Create Procedure `MetricDelete`
(	
	IN PApplicationInsightsId	int unsigned
)
Begin
	DELETE FROM metricapplicationinsights 
    Where ApplicationInsightsId = PApplicationInsightsId AND MetricApplicationInsightsId > 0;
			
End$$
DELIMITER ;


#######################################################COMMON#################################################################



Drop Procedure If Exists `CommonCodeGetList`;
DELIMITER $$
# =============================================
# Author:		Omer Kamran
# Create date: October, 07, 2020
# Description:	applicationinsightsGet gets related record from applicationinsights
# =============================================
Create Procedure `CommonCodeGetList`
(	
	IN PCode VARCHAR(120)
	)
Begin
	Select 
		tbl.CommonCodeValueId, tbl.value
	from CommonCodeValue tbl where tbl.Code = PCode;
    
   
End$$
DELIMITER ;



########################################################################################################################
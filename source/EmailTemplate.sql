delete from emailtemplate where emailtemplateId > 0;


INSERT INTO `emailtemplate` (`EmailTemplateId`,`EmailTemplateCode`,`Subject`,`SubjectTranslation`,`Message`,`MessageTranslation`,`TOEmail`,`CCEmail`,`BCCEmail`,`FromEmail`,`FromName`,`CreatedById`,`CreatedDate`,`ModifiedById`,`ModifiedDate`,`Active`) VALUES
(1,'EmpCreate','Your employee account has been created',null,
'

 <!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" href="{{CdnFontUrl}}fonts.css">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="format-detection" content="telephone=no">
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=no;">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE" />
    <title>AlgoPlus - Account Creation</title>
    <style>
 	@import url("{{CdnFontUrl}}fonts.css");
    </style>

    <!-- <link rel="stylesheet" href="{{CdnFontUrl}}fonts.css"> -->
</head>
<body style="padding:0; margin: 0; text-align: center; background: #f8f8f8;">

    <table border="0" align="center" width="600" cellspacing="0" cellpadding="0" style="border-collapse: collapse;">
        <link rel="stylesheet" href="{{CdnFontUrl}}fonts.css">

        <tr>
            <td valign="top" style="padding: 10px; text-align: center;">


                <table border="0" align="center" width="100" cellspacing="0" cellpadding="0" style="border-collapse: collapse;">
                    <tr>
                        <td style="padding: 10px; text-align: center;">
                            <img src="https://algo.com/oneview/customers/Algo_LightBackground.png" height="50" alt="AlgoPlus" title="AlgoPlus" />
                        </td>
                    </tr>
                </table>
                <table border="0" align="center" width="100%" cellspacing="0" cellpadding="0" bgcolor="#fff" style="margin-bottom: 16px; max-width: 600px; border-collapse: collapse;  background-color: #FFFFFF; box-shadow: 0 2px 4px 0 rgba(0,0,0,0.28);">
                    <tr style="width:100%; background-color: #fff;">
                        <td colspan="3" height="50">&nbsp;</td>
                    </tr>
                    <tr style="width:100%; background-color: #fff;">
                        <td width="30"></td>
                        <td style="padding:0;">
                            <table border="0" align="center" width="100%" cellspacing="0" cellpadding="0" bgcolor="#fff">
                                <tbody>
                                    <tr>
                                        <td style="font-family: NHaasGroteskDSPro-65Md, sans-serif; text-align: center; margin-top: 0; font-weight: normal; color: #3B3B3B; font-size: 36px; line-height: 38px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="color: #3B3B3B; font-family: NHaasGroteskDSPro-65Md, sans-serif; font-size: 20px; line-height: 24px; padding-bottom: 12px; text-align: left;">                                        
										<p style="margin-bottom 30px; font-size: 14px; font-family:   sans-serif; font-weight: bold; color:#444;">Congratulations!</p>
          
          <p style="margin-bottom 30px; font-size: 14px; font-family:   sans-serif; color:#444;">Your account has been successfully created.</br>
 		 Please reset your password by <a href="{{ResetPasswordURL}}"> Clicking Here </a></br>
 
          </p>
          
          <p style="margin-top: 0; margin-bottom 30px; font-size: 14px; font-family:  sans-serif; color:#444;">AlgoPlus</p>
                                        </td>
                                    </tr>

                                    <tr><td height="10" style="font-size: 0">&nbsp;</td></tr>
                                </tbody>
                            </table>

                        </td>
                        <td width="30"></td>
                    </tr>
                    <tr style="width:100%; background-color: #fff;">
                        <td colspan="3" height="20">&nbsp;</td>
                    </tr>
                </table>
               
            </td>
        </tr>
    </table>
</body>
</html>',
null,'','','','','',1,utc_timestamp(),NULL,NULL,1);

INSERT INTO `emailtemplate` (`EmailTemplateId`,`EmailTemplateCode`,`Subject`,`SubjectTranslation`,`Message`,`MessageTranslation`,`TOEmail`,`CCEmail`,`BCCEmail`,`FromEmail`,`FromName`,`CreatedById`,`CreatedDate`,`ModifiedById`,`ModifiedDate`,`Active`) VALUES 
(2,'PwdRes','Password reset request',null,
'<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" href="{{CdnFontUrl}}fonts.css">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="format-detection" content="telephone=no">
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=no;">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE" />
    <title>AlgoPlus - Reset Password</title>
    <style>
 	@import url("{{CdnFontUrl}}fonts.css");
    </style>

    <!-- <link rel="stylesheet" href="{{CdnFontUrl}}fonts.css"> -->
</head>
<body style="padding:0; margin: 0; text-align: center; background: #f8f8f8;">

    <table border="0" align="center" width="600" cellspacing="0" cellpadding="0" style="border-collapse: collapse;">
        <link rel="stylesheet" href="{{CdnFontUrl}}fonts.css">

        <tr>
            <td valign="top" style="padding: 10px; text-align: center;">


                <table border="0" align="center" width="100" cellspacing="0" cellpadding="0" style="border-collapse: collapse;">
                    <tr>
                        <td style="padding: 10px; text-align: center;">
                            <img src="https://algo.com/oneview/customers/Algo_LightBackground.png" height="50" alt="AlgoPlus" title="AlgoPlus" />
                        </td>
                    </tr>
                </table>
                <table border="0" align="center" width="100%" cellspacing="0" cellpadding="0" bgcolor="#fff" style="margin-bottom: 16px; max-width: 600px; border-collapse: collapse;  background-color: #FFFFFF; box-shadow: 0 2px 4px 0 rgba(0,0,0,0.28);">
                    <tr style="width:100%; background-color: #fff;">
                        <td colspan="3" height="50">&nbsp;</td>
                    </tr>
                    <tr style="width:100%; background-color: #fff;">
                        <td width="30"></td>
                        <td style="padding:0;">
                            <table border="0" align="center" width="100%" cellspacing="0" cellpadding="0" bgcolor="#fff">
                                <tbody>
                                    <tr>
                                        <td style="font-family: NHaasGroteskDSPro-65Md, sans-serif; text-align: center; margin-top: 0; font-weight: normal; color: #3B3B3B; font-size: 36px; line-height: 38px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="color: #3B3B3B; font-family: NHaasGroteskDSPro-65Md, sans-serif; font-size: 20px; line-height: 24px; padding-bottom: 12px; text-align: left;">    
 <p style="margin-bottom 30px; font-size: 14px; font-family:   sans-serif; color:#444;">Algo was notified that you forgot your password. <a href="{{ResetPasswordURL}}"> Click here </a> to reset your password.

</p>			
</br>
  <p style="margin-bottom: 10px; font-size: 14px; font-family:   sans-serif; color:#444;">Thank you,</p>	
  <p style="margin-bottom: 30px; font-size: 14px; font-family:   sans-serif; color:#444;">Algo Team</p>	</br>
										 
                                        </td>
                                    </tr>

                                    <tr><td height="10" style="font-size: 0">&nbsp;</td></tr>
                                </tbody>
                            </table>

                        </td>
                        <td width="30"></td>
                    </tr>
                    <tr style="width:100%; background-color: #fff;">
                        <td colspan="3" height="20">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>',null,'','','','','',1,utc_timestamp(),NULL,NULL,1);
  
INSERT INTO `emailtemplate` (`EmailTemplateId`,`EmailTemplateCode`,`Subject`,`SubjectTranslation`,`Message`,`MessageTranslation`,`TOEmail`,`CCEmail`,`BCCEmail`,`FromEmail`,`FromName`,`CreatedById`,`CreatedDate`,`ModifiedById`,`ModifiedDate`,`Active`) VALUES 
(3,'APPRV','Request for approval',null,
'',null,'','','','','',1,utc_timestamp(),NULL,NULL,1);
  
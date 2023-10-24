using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using DataAccess.CustomModels;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DataAccess.Repositories
{
   public class RegisteredDevicesRepository
    {
        SugarLabEntities Db;
        EmailConfigurationRepository emailConfigRepo;
        public RegisteredDevicesRepository()
        {
            Db = new SugarLabEntities();
            emailConfigRepo = new EmailConfigurationRepository();
        }

        /// <summary>
        /// Register a new smart phone / IoT device.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(RegisteredDevice model)
        {
            
            if(model == null)
            {
                return false;
            }
            try
            {
                Db.RegisteredDevices.Add(model);
                Db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }
        

        /// <summary>
        /// Get the list of all smart phone/IoT registered on BSLIS.
        /// </summary>
        /// <returns>async List</returns>
        public List<RegisteredDevice> GetRegisteredDevicesList()
        {
            List<RegisteredDevice> registeredDevices = new List<RegisteredDevice>();
            try
            {
                registeredDevices = Db.RegisteredDevices.ToList();
                
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return registeredDevices;
        }

        public RegisteredDevice FindRegisteredDeviceByImei(string userCode)
        {
            RegisteredDevice registeredDevice = new RegisteredDevice();
            if(userCode == null)
            {
                return registeredDevice;
            }
            try
            {
                registeredDevice = Db.RegisteredDevices.Where(r => r.UserCode == userCode).FirstOrDefault();
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return registeredDevice;
        }

        public RegisteredDevice FindRegisteredDeviceByCode(string Code)
        {
            RegisteredDevice registeredDevice = new RegisteredDevice();
            if (Code == null)
            {
                return registeredDevice;
            }
            try
            {
                registeredDevice = Db.RegisteredDevices.Where(r => r.Code == Code).FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return registeredDevice;
        }

        /// <summary>
        /// update existing registered device
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(RegisteredDevice model)
        {
            try
            {
                RegisteredDevice device = new RegisteredDevice();
                device = FindRegisteredDeviceByCode(model.Code);
                device.Gender = model.Gender;
                device.FirstName = model.FirstName;
                device.LastName = model.LastName;
                device.ImageUrl = model.ImageUrl;
                device.DeviceTypeCode = model.DeviceTypeCode;
                device.UnitRights = model.UnitRights;
                device.MenuRights = model.MenuRights;
                device.ValidFrom = model.ValidFrom;
                device.ValidTill = model.ValidTill;
                device.DepartmentCode = model.DepartmentCode;
                device.Email = model.Email;
                device.IsActive = model.IsActive;
            
                Db.SaveChanges();
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// Deactivate a registered device.
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public bool Delete(string userCode)
        {
            RegisteredDevice registeredDevice = new RegisteredDevice();
            registeredDevice =  FindRegisteredDeviceByImei(userCode);

            registeredDevice.IsActive = false;
            try
            {
                Db.SaveChanges();
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            return true;
        }

        public bool ResetPasswordRequestForDevice(PasswordResetModel p)
        {
            bool status = false;
            if(p.UserCode == null)
            {
                status = false;
            }

            // check either email address exists in RegisteredDevice table or not

            RegisteredDevice devices = new RegisteredDevice();
            devices = Db.RegisteredDevices.Where(x => x.UserCode == p.UserCode && x.IsActive == true).FirstOrDefault();

            if(devices != null)
            {
                // get validity duration in minutes from 'FlexSubMaster' table

                FlexRepository fRepo = new FlexRepository();
                List<FlexSubMaster> f = new List<FlexSubMaster>();
                f = fRepo.GetActiveFlexMasters();
                int duration = 10; // default duration is 10 minutes
                string baseUrl = "";
                if (f != null)
                {
                    duration = Convert.ToInt32(f.Where(x => x.Code == 10).Select(x => x.Value).FirstOrDefault());
                    baseUrl = f.Where(x => x.Code == 11).Select(x => x.Value).FirstOrDefault();
                }


                // generate a random string (for token)
                string token = Guid.NewGuid().ToString();


                // get Client IP
                string IPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (IPAddress == null || IPAddress == string.Empty)
                {
                    IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                // create an object of 'PasswordResets' generate values accordingly and insert into table

                PasswordReset obj = new PasswordReset
                {
                    UserCode = p.UserCode,
                    Url = baseUrl,
                    Token = token,
                    CreatedAt = DateTime.Now,
                    ValidFrom = DateTime.Now,
                    ValidTill = DateTime.Now.AddMinutes(duration),
                    IsActive = true,
                    RequestIP = IPAddress
                };

                Db.PasswordResets.Add(obj);
                Db.SaveChanges();
                status = true;
                string userFullName = string.Concat(devices.FirstName, " ", devices.LastName);
               Task<string> response = SendVerificationMail(p.UserCode, userFullName, baseUrl, token);
            }


            // once values successfully inserted int 'PasswordReset' table send an email with activation link
            else
            {
                status = false;
            }
            return status;
        }


        /// <summary>
        /// send email to user. (Later move this function to Email Repository)
        /// </summary>
        /// <param name="EmailTo"></param>
        /// <param name="EmailToName"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public async Task<string> SendVerificationMail(string EmailTo, string EmailToName, string url, string token, string subject = "Birla Sugar Lab Information System Account Verification!")
        {
            string verificationLink = url;
            if (token != null || token != string.Empty)
            {
                verificationLink = string.Concat(verificationLink, token);
            }
            var emailConfig = emailConfigRepo.GetEmailConfigrationByPrioritySequence(1);
            var apiKey = emailConfig.ApiKey; // @"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";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                //From = new EmailAddress("ugsil.ravibhushan@birla-sugar.com", "BSLIS Admin"),
                From = new EmailAddress(emailConfig.EmailAddress, "BSLIS Admin"),
                Subject = "BSLIS | Reset Password",
                PlainTextContent = "",
                HtmlContent = PasswordResetEmailTemplate(verificationLink, EmailToName)
            };
            msg.AddTo(new EmailAddress(EmailTo, EmailToName));

            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            return response.Body.ToString();

        }

        private string PasswordResetEmailTemplate(string verificationUrl, string emailToName)
        {
            string htmlContent = @"<html> " +
                        " <head> " +
                         " <style> " +
                        "    /* Base ------------------------------ */ " +
                        "    *:not(br):not(tr):not(html) { " +
                        "      font-family: Arial, 'Helvetica Neue', Helvetica, sans-serif; " +
                        "      -webkit-box-sizing: border-box; " +
                        "      box-sizing: border-box; " +
                        "    } " +
                        "    body { " +
                        "      width: 100% !important; " +
                        "      height: 100%; " +
                        "      margin: 0; " +
                        "      line-height: 1.4; " +
                        "      background-color: #F5F7F9; " +
                        "      color: #839197; " +
                        "      -webkit-text-size-adjust: none; " +
                        "    } " +
                        "    a { " +
                        "      color: #414EF9; " +
                        "    } " +
                        "    /* Layout ------------------------------ */ " +
                        "    .email-wrapper { " +
                        "      width: 100%; " +
                        "      margin: 0; " +
                        "      padding: 0; " +
                        "      background-color: #F5F7F9; " +
                        "    } " +
                        "    .email-content { " +
                        "      width: 100%; " +
                        "      margin: 0; " +
                        "      padding: 0; " +
                        "    } " +
                        "    /* Masthead ----------------------- */ " +
                        "    .email-masthead { " +
                        "      padding: 25px 0; " +
                        "      text-align: center; " +
                        "    } " +
                        "    .email-masthead_logo { " +
                        "      max-width: 400px; " +
                        "      border: 0; " +
                        "    } " +
                        "    .email-masthead_name { " +
                        "      font-size: 16px; " +
                        "      font-weight: bold; " +
                        "      color: #839197; " +
                        "      text-decoration: none; " +
                        "      text-shadow: 0 1px 0 white; " +
                        "    } " +
                        "    /* Body ------------------------------ */ " +
                        "    .email-body { " +
                        "      width: 100%; " +
                        "      margin: 0; " +
                        "      padding: 0; " +
                        "      border-top: 1px solid #E7EAEC; " +
                        "      border-bottom: 1px solid #E7EAEC; " +
                        "      background-color: #FFFFFF; " +
                        "    } " +
                        "    .email-body_inner { " +
                        "      width: 570px; " +
                        "      margin: 0 auto; " +
                        "      padding: 0; " +
                        "    } " +
                        "    .email-footer { " +
                        "      width: 570px; " +
                        "      margin: 0 auto; " +
                        "      padding: 0; " +
                        "      text-align: center; " +
                        "    } " +
                        "    .email-footer p { " +
                        "      color: #839197; " +
                        "    } " +
                        "    .body-action { " +
                        "      width: 100%; " +
                        "      margin: 30px auto; " +
                        "      padding: 0; " +
                        "      text-align: center; " +
                        "    } " +
                        "    .body-sub { " +
                        "      margin-top: 25px; " +
                        "      padding-top: 25px; " +
                        "      border-top: 1px solid #E7EAEC; " +
                        "    } " +
                        "    .content-cell { " +
                        "      padding: 35px; " +
                        "    } " +
                        "    .align-right { " +
                        "      text-align: right; " +
                        "    } " +
                        "    /* Type ------------------------------ */ " +
                        "    h1 { " +
                        "      margin-top: 0; " +
                        "      color: #292E31; " +
                        "      font-size: 19px; " +
                        "      font-weight: bold; " +
                        "      text-align: left; " +
                        "    } " +
                        "    h2 { " +
                        "      margin-top: 0; " +
                        "      color: #292E31; " +
                        "      font-size: 16px; " +
                        "      font-weight: bold; " +
                        "      text-align: left; " +
                        "    } " +
                        "    h3 { " +
                        "      margin-top: 0; " +
                        "      color: #292E31; " +
                        "      font-size: 14px; " +
                        "      font-weight: bold; " +
                        "      text-align: left; " +
                        "    } " +
                        "    p { " +
                        "      margin-top: 0; " +
                        "      color: #839197; " +
                        "      font-size: 16px; " +
                        "      line-height: 1.5em; " +
                        "      text-align: left; " +
                        "    } " +
                        "    p.sub { " +
                        "      font-size: 12px; " +
                        "    } " +
                        "    p.center { " +
                        "      text-align: center; " +
                        "    } " +
                        "    /* Buttons ------------------------------ */ " +
                        "    .button { " +
                        "      display: inline-block; " +
                        "      width: 200px; " +
                        "      background-color: #414EF9; " +
                        "      border-radius: 3px; " +
                        "      color: #ffffff; " +
                        "      font-size: 15px; " +
                        "      line-height: 45px; " +
                        "      text-align: center; " +
                        "      text-decoration: none; " +
                        "      -webkit-text-size-adjust: none; " +
                        "      mso-hide: all; " +
                        "    } " +
                        "    .button--green { " +
                        "      background-color: #28DB67; " +
                        "    } " +
                        "    .button--red { " +
                        "      background-color: #FF3665; " +
                        "    } " +
                        "    .button--blue { " +
                        "      background-color: #414EF9; " +
                        "    } " +
                        "    /*Media Queries ------------------------------ */ " +
                        "    @media only screen and (max-width: 600px) { " +
                        "      .email-body_inner, " +
                        "      .email-footer { " +
                        "        width: 100% !important; " +
                        "      } " +
                        "    } " +
                        "    @media only screen and (max-width: 500px) { " +
                        "      .button { " +
                        "        width: 100% !important; " +
                        "      } " +
                        "    } " +
                        "  </style> " +
                        "</head> " +
                        "<body> " +
                        "  <table class=\"email-wrapper\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"> " +
                        "    <tr> " +
                        "      <td align=\"center\"> " +
                        "        <table class=\"email-content\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"> " +
                        "          <!-- Logo --> " +
                        "          <tr> " +
                        "            <td class=\"email-masthead\"> " +
                        "              <a class=\"email-masthead_name\">Birla Sugar Lab Information System!</a> " +
                        "            </td> " +
                        "          </tr> " +
                        "          <!-- Email Body --> " +
                        "          <tr> " +
                        "            <td class=\"email-body\" width=\"100%\"> " +
                        "              <table class=\"email-body_inner\" align=\"center\" width=\"570\" cellpadding=\"0\" cellspacing=\"0\"> " +
                        "                <!-- Body content --> " +
                        "                <tr> " +
                        "                  <td class=\"content-cell\"> " +
                        "                    <h1>Password reset for your account</h1> " +
                        "                    <p>Greetings "+emailToName+" Ji,, <br/></p> " +
                        "                    <!-- Action --> " +
                        "                    <table class=\"body-action\" align=\"center\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"> " +
                        "                      <tr> " +
                        "                        <td align=\"center\"> " +
                        "                          <div> " +
                        "                            <!--[if mso]><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"{{action_url}}\" style=\"height:45px;v-text-anchor:middle;width:200px;\" arcsize=\"7%\" stroke=\"f\" fill=\"t\"> " +
                        "                            <v:fill type=\"tile\" color=\"#414EF9\" /> " +
                        "                            <w:anchorlock/> " +
                        "                            <center style=\"color:#ffffff;font-family:sans-serif;font-size:15px;\">Click on button to reset your password</center> " +
                        "                          </v:roundrect><![endif]--> " +
                        "                            <a href=" + verificationUrl + " class=\"button button--green\">Reset Password</a> " +
                        "                          </div> " +
                        "                        </td> " +
                        "                      </tr> " +
                        "                    </table> " +
                        "                    <p>Thanks,<br>The BSLIS Team</p> " +
                        "                    <!-- Sub copy --> " +
                        "                    <table class=\"body-sub\"> " +
                        "                      <tr> " +
                        "                        <td> " +
                        "                          <p class=\"sub\">If you’re having trouble clicking the button, copy and paste the URL below into your web browser. " +
                        "                          </p> " +
                        "                          <p class=\"sub\"><a href=" + verificationUrl + ">" + verificationUrl + "</a></p> " +
                        "                        </td> " +
                        "                      </tr> " +
                        "                    </table> " +
                        "                  </td> " +
                        "                </tr> " +
                        "              </table> " +
                        "            </td> " +
                        "          </tr> " +
                        "          <tr> " +
                        "            <td> " +
                        "              <table class=\"email-footer\" align=\"center\" width=\"570\" cellpadding=\"0\" cellspacing=\"0\"> " +
                        "                <tr> " +
                        "                  <td class=\"content-cell\"> " +
                        "                    <p class=\"sub center\"> " +
                        "                       " +
                        "                    </p> " +
                        "                  </td> " +
                        "                </tr> " +
                        "              </table> " +
                        "            </td> " +
                        "          </tr> " +
                        "        </table> " +
                        "      </td> " +
                        "    </tr> " +
                        "  </table> " +
                        "</body> " +
                        "</html>";
            return htmlContent;
        }


        /// <summary>
        /// reset password by token in PasswordReset Table
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool ResetPassword(string token, string password)
        {
            /// first check either token exists in Password reset table or not, 
            /// also check token validity and IsActive flag.
            /// 
            bool status = false;
            PasswordReset pReset = new PasswordReset();
            RegisteredDevice rDevice = new RegisteredDevice();
            using (SugarLabEntities db = new SugarLabEntities())
            {
                pReset = db.PasswordResets.Where(x => x.Token == token
                && DateTime.Now >= x.ValidFrom
                && DateTime.Now <= x.ValidTill
                && x.IsActive == true
                ).FirstOrDefault();
                // if token is valid, get user details based on it
                if(pReset != null)
                {
                    
                    rDevice = db.RegisteredDevices.Where(x => x.UserCode == pReset.UserCode
                    && x.IsActive == true
                    && DateTime.Now >= x.ValidFrom
                    && DateTime.Now <= x.ValidTill
                    ).FirstOrDefault();
                    
                    if (rDevice != null)
                    {
                        // update user password in 'RegisteredDevice' table and return true
                        try
                        {

                        
                        rDevice.UserPassword = password;
                        db.SaveChanges();
                        status = true;
                        db.PasswordResets.Remove(pReset);
                        db.SaveChanges();
                        }
                        catch(Exception ex)
                        {
                            new Exception(ex.Message);
                            status = false;
                        }
                    }
                }
                else
                {
                    status = false;
                }
            }
            return status;
        }
    }
}

using System;
using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Net;

namespace DataAccess.Repositories
{
    public class EmailRepository
    {
        CryptographyRepository cryptoRepository;
        public EmailRepository()
        {
            cryptoRepository = new CryptographyRepository();
        }
        public  async Task<string> SendVerificationMail(string EmailTo, string EmailToName, string VarficationToken, string verificationOTP, string userCode, string userPassword, string subject= "Birla Sugar Lab Information System Account Verification!")
        {
            string verificationLink = VarficationToken;
            
            var htmlContent = ConfirmEmailTemplate(verificationLink, verificationOTP, EmailToName, userCode, userPassword);
            
            if( await SendEmailAsync(EmailTo, htmlContent, subject, true))
            {
                return "Email send sucessfully";
            }             
           else
            {
                return "Failed to send email";
            }

        }
        public async Task<string> SendDeviceRegistrationMail(string EmailTo, string EmailToName, string userCode, string userPassword, string subject = "Registration Successful on BSLIS Android App")
        {
            
            var htmlContent = DeviceRegistrationEmailTemplate(userCode, userPassword);
            bool result = await SendEmailAsync(EmailTo,htmlContent,subject,true);
            if(result)
            {
                return "Email sent sucessfully";
            }
            else
            {
                return "Failed to send email.";
            }

        }
        private string ConfirmEmailTemplate(string verificationUrl, string OTP, string Name, string UserCode, string UserPassword)
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
                        "                    <h1>Verify your BSLIS account.</h1> " +
                        "                    <p>Greetings Mr. "+ Name+ "Ji, <br/>Thanks for signing up for B.S.L.I.S! We're excited to have you as an early user.</p> " +
                        "                    <br/> Your Login Code is <strong>" + UserCode + "</strong> and password is <strong>"+UserPassword+"</strong>.  " +
                        "                    <br/> Your One Time Password/Verification code is <strong>" +OTP+ "</strong>.  " +
                        "                    <br/> Use One Time Password to verify your account. " +
                        "                    <!-- Action --> " +
                        "                    <table class=\"body-action\" align=\"center\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"> " +
                        "                      <tr> " +
                        "                        <td align=\"center\"> " +
                        "                          <div> " +
                        "                            <!--[if mso]><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"{{action_url}}\" style=\"height:45px;v-text-anchor:middle;width:200px;\" arcsize=\"7%\" stroke=\"f\" fill=\"t\"> " +
                        "                            <v:fill type=\"tile\" color=\"#414EF9\" /> " +
                        "                            <w:anchorlock/> " +
                        "                            <center style=\"color:#ffffff;font-family:sans-serif;font-size:15px;\">Verify Email</center> " +
                        "                          </v:roundrect><![endif]--> " +
                        "                            <a href="+ verificationUrl + " class=\"button button--green\">Verify Email</a> " +
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
                        "                          <p class=\"sub\"><a href="+verificationUrl+">"+ verificationUrl + "</a></p> " +
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
                        "                      Technorator LLP. " +
                        "                      <br>New Delhi-110094 " +
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


        private string DeviceRegistrationEmailTemplate(string userCode, string password)
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
                        " <tr><td class=\"email-masthead\">" +
                        " <img src='http://bslis.com/Content/MainContent/images/birla-sugar.jpg'/>" +
                        " </td></tr>"+
                        "          <tr> " +
                        "            <td class=\"email-masthead\"> " +
                        "              <a class=\"email-masthead_name\">Birla Sugar Lab Information System</a> " +
                        "            </td> " +
                        "          </tr> " +
                        "          <!-- Email Body --> " +
                        "          <tr> " +
                        "            <td class=\"email-body\" width=\"100%\"> " +
                        "              <table class=\"email-body_inner\" align=\"center\" width=\"570\" cellpadding=\"0\" cellspacing=\"0\"> " +
                        "                <!-- Body content --> " +
                        "                <tr> " +
                        "                  <td class=\"content-cell\"> " +
                        "                    <h1>Android App Registration Sucessful!.</h1> " +
                        "                    <p>Respected Sir/Madam, <br/><br/>Birla Sugar Lab App is available on the <strong>Google Play Store.</strong>.</p> " +
                        "                    <br/><br/> You can search it by typing <strong>“Birla Sugar Lab Information System”</strong> in the <code>search box</code> on the Google Play store and install it." +

                        "                    <br/><br/> Your account has been created to access the “Birla Sugar Lab Information System” Android application and credentials are as under" +
                        "                    <!-- Action --> " +
                        "                    <table class=\"body-action\" align=\"center\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"> " +
                        "                      <tr> " +
                        "                        <td align=\"center\"> " +
                        "                          <strong>User Code = "+userCode + "</strong>"+
                        "                        </td> " +
                         "                        <td align=\"center\"> " +
                        "                          <strong>Pasword = " + password + "</strong>" +
                        "                        </td> " +
                        "                      </tr> " +
                        "                    </table> " +
                        "                    <p>*	Kindly do not share your credentials with other users otherwise, the hourly notification won't be delivered on your mobile.</p> " +
                        "                    <p>Thanks,<br>The BSLIS Team</p> " +
                        "                    <!-- Sub copy --> " +
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
                        "                      Birla Sugar (K.K. Birla Group of Sugar Companies)" +
                        "                      <br/>9/1, R.N. Mukherjee Road" +
                        "                      <br/>Birla Building, 5th Floor, Kolkata 700 001 India" +
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
        /// Sent password reset email
        /// </summary>
        /// <param name="EmailTo"></param>
        /// <param name="EmailToName"></param>
        /// <param name="userCode"></param>
        /// <param name="userPassword"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        

        public async  Task<bool> SendEmailAsync(string to, string body, string subject = null,  bool isBodyHtml = false  )
        {
            EmailConfigurationRepository emailConfigRepo = new EmailConfigurationRepository();
            var emailConfig = emailConfigRepo.GetEmailConfigrationByEmailAddress("admin@bslis.com");
            try
            {
                using(MailMessage message = new MailMessage(emailConfig.EmailAddress, to)) 
                {
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = isBodyHtml;
                    using(SmtpClient smtpclient = new SmtpClient(emailConfig.SmtpHost, emailConfig.SmtpPort))

                    {
                        smtpclient.Credentials = new NetworkCredential(emailConfig.EmailAddress, emailConfig.Password);
                        smtpclient.EnableSsl = emailConfig.EnabledSSL;
                        await smtpclient.SendMailAsync(message);
                    }
                    //SmtpClient smtpClient = new SmtpClient();
                    //smtpClient.Host = emailConfig.SmtpHost;
                    //smtpClient.EnableSsl = emailConfig.EnabledSSL;
                    //NetworkCredential credential = new NetworkCredential(emailConfig.EmailAddress, emailConfig.Password);
                    //smtpClient.Port = emailConfig.SmtpPort;
                    //smtpClient.Send(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
                

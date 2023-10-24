using System;
using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EmailRepository
    {
        CryptographyRepository cryptoRepository;
        public EmailRepository()
        {
            cryptoRepository = new CryptographyRepository();
        }
        public async Task<string> SendVerificationMail(string EmailTo, string EmailToName, string VarficationToken, string verificationOTP, string userCode, string userPassword, string subject= "Birla Sugar Lab Information System Account Verification!")
        {
            //string randomString = cryptoRepository.GenerateSalt();
            string verificationLink = VarficationToken;
            //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
            //var apiKey = Environment.GetEnvironmentVariable("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");
            string apiKey = @"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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("bitcodesindia@gmail.com", "B.S.L.I.S Admin");
            var to = new EmailAddress(EmailTo, EmailToName);
            var plainTextContent = "";
            var htmlContent = ConfirmEmailTemplate(verificationLink, verificationOTP, EmailToName, userCode, userPassword);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var response = await client.SendEmailAsync(msg);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            return response.StatusCode.ToString(); 

        }
        public async Task<string> SendDeviceRegistrationMail(string EmailTo, string EmailToName, string userCode, string userPassword, string subject = "Registration Successful on BSLIS Android App")
        {
            //string randomString = cryptoRepository.GenerateSalt();
            //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
            //var apiKey = Environment.GetEnvironmentVariable("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");
            string apiKey = @"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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("bitcodesindia@outlook.com", "B.S.L.I.S Admin");
            var to = new EmailAddress(EmailTo, EmailToName);
            var plainTextContent = "";
            var htmlContent = DeviceRegistrationEmailTemplate(userCode, userPassword);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var response = await client.SendEmailAsync(msg);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            return response.StatusCode.ToString();

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
        
    }
}
                

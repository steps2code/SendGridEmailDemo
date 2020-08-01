using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendgridEmailDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiKey = "<<Put your API Key>>";
            string fromEmail = "<<Put your from email ID>>"; string toEmail = "<<Put your to email ID>>";
            var response = EmailService.SendEmail(apiKey,fromEmail,toEmail);
            Response result = response.Result;
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Console.WriteLine("1. Email sent successfully.");
            }

            var templateResponse = EmailService.SendEmailUsingTemplate(apiKey, fromEmail, toEmail);
            Response templateResult = templateResponse.Result;
            if (templateResult.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Console.WriteLine("2. Email sent successfully.");
            }
            Console.Read();
        }

    }


    public class EmailService
    {
        public static async Task<Response> SendEmail(string apiKey,string fromEmail,string toEmail)
        {
            //Step 1: Create Sendgrid Email client.
            var emailClient = new SendGridClient(apiKey);
            //Step 2: Create Message Object.
            var msgs = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, "Yash "),
                Subject = "Test email using the SendGrid provider!",
                HtmlContent = "<strong>Welcome to SendGrid email demo!<a href=https://www.steps2code.com/>Please visit our web site for more details.</a></strong>",
            };
            //Step 3: Assign destination email address.
            msgs.AddTo(new EmailAddress(toEmail,"steps2mycode"));
            //Step 4: Set email footer & enable click tracking (Optional)
            msgs.SetFooterSetting(true, "<br/><strong>Thanks & Regards,</strong><b> Team Steps2Code");
            msgs.SetClickTracking(true, true);
            var response = await emailClient.SendEmailAsync(msgs);
            return response;
        }

        public static async Task<Response> SendEmailUsingTemplate(string apiKey, string fromEmail, string toEmail)
        {
            //Step 1: Create Sendgrid Email client.
            var emailClient = new SendGridClient(apiKey);
            //Step 2: Create Message Object and assign template id.
            var msgs = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, "Yash "),
                TemplateId = "d-a77ebe1fec3549779b6c28094a212bdd"
            };
            //Step 3: Assign destination email address.
            msgs.AddTo(new EmailAddress(toEmail, "steps2mycode"));
            //Step 4: Initialize tamplate data.
            msgs.SetTemplateData(new RegistrationEmailTemplate
            {
                username = "Yash",
                subject = "Welcome To Steps2Code!!",
                loginurl = "https://www.steps2code.com/"
            });
            var response = await emailClient.SendEmailAsync(msgs);
            return response;
        }
    }

    public class RegistrationEmailTemplate
    {
        public string username { get; set; }
        public string loginurl { get; set; }
        public string subject { get; set; }
    }

}

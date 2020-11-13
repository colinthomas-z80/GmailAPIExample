using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace GmailAPIProject
{
    class MainClass
    {
        static string[] Scopes = { GmailService.Scope.GmailReadonly };

        [STAThread]
        static void Main(string[] args)
        {
            UserCredential credential;
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }





            Program.ReadEmails("mailbotmailbotmailbot@gmail.com", credential);
            Console.WriteLine("Done!");
        }
    }
}


/*Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            AE.Net.Mail.MailMessage msg = Program.CreateMessage(
                "Dear Colin",
                "How are you",
                "mailbotmailbotmailbot@gmail.com",
                "cthomas0687@gmail.com");*/

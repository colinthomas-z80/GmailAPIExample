using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using Microsoft.AspNetCore.WebUtilities;


namespace GmailAPIProject
{
    static class Program
    {
        static string ApplicationName = "Gmail API .NET Quickstart";

        public static void SendIt(UserCredential credential, AE.Net.Mail.MailMessage msg)
        {
            var gmail = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var msgStr = new StringWriter();
            msg.Save(msgStr);

            var result = gmail.Users.Messages.Send(new Message
            {
                Raw = Base64UrlEncode(msgStr.ToString())
            }, "me").Execute();
            Console.WriteLine("Message ID {0} sent.", result.Id);
        }

        public static void ReadEmails(String sender, UserCredential credential)
        {
            var gmail = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var request = gmail.Users.Messages.List("me");
            request.IncludeSpamTrash = false;
            request.LabelIds = "INBOX";

            var responseList = request.Execute();
            var ids = new List<String>();

            foreach(Message m in responseList.Messages)
            {
                ids.Add(m.Id);
            }

            for (int i = 0; i < ids.Count-1; i++)
            {
                var msgRequest = gmail.Users.Messages.Get("me", ids[i]);
                //msgRequest.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Raw;
                
                Message? msgResponse = msgRequest.Execute();
                String? body = msgResponse.Payload.Body.Data;


                if (body != null)
                {
                    Console.WriteLine(System.Text.Encoding.UTF8.GetString(
                    WebEncoders.Base64UrlDecode(body)));
                }
            }


        }

        public static AE.Net.Mail.MailMessage CreateMessage(String subject, String body, String fromAddress, String tooAddress)
        {
            var msg = new AE.Net.Mail.MailMessage
            {
                Subject = subject,
                Body = body,
                From = new MailAddress(fromAddress)
            };
            msg.To.Add(new MailAddress(tooAddress));
            msg.ReplyTo.Add(msg.From); // Bounces without this!!
            var msgStr = new StringWriter();
            msg.Save(msgStr);

            return msg;
        }

        private static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            // Special "url-safe" base64 encode.
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }
    }
}
# Nuget packages required

AE.Net.Mail
Google.Apis.Gmail.v1
Microsoft.AspNetCore.WebUtilities
System.Text.Encoding.CodePages

# Directions
Follow the tutorial at https://developers.google.com/gmail/api/quickstart/dotnet to activate
Google api for your account and setup credentials for your project.

The "Scopes" variable must be set for which method you want to test. GmailReadonly for reading
emails, GmailSend for sending. Each time you change scopes, Go to folder view bin/ and delete 
token.json
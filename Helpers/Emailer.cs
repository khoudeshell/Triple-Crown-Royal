using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HorseLeague.Models.Domain;
using System.Net.Mail;
namespace HorseLeague.Helpers
{
    public class Emailer
    {
        public static void SendEmail(IEmailable emailEntity, string toAddress, LeagueRace leagueRace)
        {
            MailMessage email = new MailMessage();
            email.From = new MailAddress("noreply@triplecrownroyal.com");
            email.To.Add(new MailAddress(toAddress));
            email.Subject = emailEntity.GetSubject(leagueRace);
            email.Body = emailEntity.GetBody(leagueRace);
            
#if !DEBUG
            //SmtpAccess
            SmtpClient client = new SmtpClient();
            client.Host = "localhost";
            client.Send(email);
#endif
        }
    }
}

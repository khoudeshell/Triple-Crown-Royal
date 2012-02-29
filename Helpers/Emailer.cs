using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HorseLeague.Models.Domain;
using System.Net.Mail;
using System.Net;
namespace HorseLeague.Helpers
{
    public class Emailer
    {
        public static void SendEmail(IEmailable emailEntity, string toAddress, LeagueRace leagueRace)
        {
            MailMessage email = new MailMessage();
            email.From = new MailAddress("postmaster@triplecrownroyal.com");
            email.To.Add(new MailAddress(toAddress));
            email.Subject = emailEntity.GetSubject(leagueRace);
            email.Body = emailEntity.GetBody(leagueRace);
            
#if !DEBUG
            SmtpClient client = new SmtpClient();
            client.EnableSsl = false;
            client.Send(email);
#endif
        }
    }
}

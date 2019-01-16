using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Projetos_CGTI.Models;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace Projetos_CGTI
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Credentials:
            //string smtpServer = ConfigurationManager.AppSettings["EmailSmtpServer"];
            //int smtpPort = int.Parse(ConfigurationManager.AppSettings["EmailSmtpPort"]);
            //bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EmailEnableSSL"]);
            //string smtpUsername = ConfigurationManager.AppSettings["EmailSmtpUsername"];
            //string smtpPassword = ConfigurationManager.AppSettings["EmailSmtpPassword"];
            //string sentFrom = ConfigurationManager.AppSettings["EmailSentFrom"];

            // Configure the client:
            //var client = new SmtpClient(smtpServer, Convert.ToInt32(587));

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(message.Destination);
            mail.To.Add(message.Destination); // para
            mail.Subject = (message.Subject); // assunto

            mail.Body = (message.Body);

            using (var smtp = new SmtpClient("smtp.cgti.org.br", Convert.ToInt32(587)))
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Credentials = new NetworkCredential("senha@cgti.org.br", "S3nhas@CGTI");
                smtp.Send(mail);
            }
            //client.Port = smtpPort;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.EnableSsl = enableSsl;

            //// Create the credentials:
            //var credentials = new NetworkCredential(smtpUsername, smtpPassword);
            //client.Credentials = credentials;

            //// Create the message:
            //var mail = new System.Net.Mail.MailMessage(sentFrom, message.Destination);

            //mail.Subject = message.Subject;
            //mail.Body = message.Body;

            //// Send:
            //client.SendMailAsync(mail);

            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}

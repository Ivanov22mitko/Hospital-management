using HM.Core.Constants;
using Hospital_management.Views.Contact;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_management.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailSender _emailSender;

        public ContactController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public IActionResult ContactForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ContactModel contactModel)
        {
            await _emailSender.SendEmailAsync(
              UserConstants.AdminEmail,
               $"Hospital manager message from {contactModel.Input.Name} with email {contactModel.Input.Email}",
               contactModel.Input.Problem);

            ViewData[MessageConstant.SuccessMessage] = "Email sent!";

            return Redirect("/Home/");

        }
    }
}

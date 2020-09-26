using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentManagement.Models;
using StudentManagement.Models.ManageViewModel;

namespace StudentManagement.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger logger;

        public ManageController(
          UserManager<User> userManager,
          SignInManager<User> signInManager,
          ILoggerFactory loggerFactory)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = loggerFactory.CreateLogger<ManageController>();
        }
        //
        // GET: /Manage/Index
        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            this.ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.ChangeEmailSuccess ? "Your email has been changed."
                  : message == ManageMessageId.Error ? "An error has occurred."
                    : "";

            var user = await this.GetCurrentUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }

            return this.View();
        }

        // GET: /Manage/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                var result = await this.userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    this.logger.LogInformation(3, "User changed their password successfully.");
                    return this.RedirectToAction(nameof(ManageController.Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }

                this.AddErrors(result);
                return this.View(model);
            }

            return this.RedirectToAction(nameof(ManageController.Index), new { Message = ManageMessageId.Error });
        }

        [HttpGet]
        public IActionResult ChangeEmail()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                var result = await this.userManager.ChangeEmailAsync(user, model.NewEmail, model.Token);
                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    this.logger.LogInformation(4, "User changed their email successfully.");
                    return this.RedirectToAction(nameof(ManageController.Index), new { Message = ManageMessageId.ChangeEmailSuccess });
                }

                this.AddErrors(result);
                return this.View(model);
            }

            return this.RedirectToAction(nameof(ManageController.Index), new { Message = ManageMessageId.Error });
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public enum ManageMessageId
        {
            ChangeEmailSuccess,
            ChangePasswordSuccess,
            Error
        }

        private Task<User> GetCurrentUserAsync()
        {
            return this.userManager.GetUserAsync(this.HttpContext.User);
        }

        #endregion
    }

}

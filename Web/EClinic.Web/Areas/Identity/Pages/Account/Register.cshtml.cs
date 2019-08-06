using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using EClinic.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;
using EClinic.Common;

namespace EClinic.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<EClinicUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<EClinicUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<EClinicUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<EClinicUser> signInManager,
            ILogger<RegisterModel> logger
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            

            [Required]
            [MinLength(2, ErrorMessage = "First name must be aleest 2 charecters!")]
            [MaxLength(20, ErrorMessage = "First name must not be more then 20 charecters!")]
            public string FirstName { get; set; }

            [MinLength(2, ErrorMessage = "Middle name must be aleest 2 charecters!")]
            [MaxLength(20, ErrorMessage = "Middle name must not be more then 20 charecters!")]
            public string MiddleName { get; set; }

            [Required]
            [MinLength(2, ErrorMessage = "Last name must be aleest 2 charecters!")]
            [MaxLength(20, ErrorMessage = "Last name must not be more then 20 charecters!")]
            public string LastName { get; set; }

            [Range(0, 110, ErrorMessage = "Age must be in range 0 - 110")]
            public int Age { get; set; }

            [MinLength(2, ErrorMessage = "Address name must be aleest 2 charecters!")]
            [MaxLength(50, ErrorMessage = "Address name must not be more then 50 charecters!")]
            public string Address { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var isRoot = _userManager.Users.Any();

                var user = new EClinicUser {
                    UserName = Input.Email,
                    Address = Input.Address,
                    Age = Input.Age,
                    CreatedOn = DateTime.UtcNow,
                    FirstName = Input.FirstName,
                    MiddleName = Input.MiddleName,
                    LastName = Input.LastName,
                    Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!isRoot)
                    {
                        await _userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);
                    }

                    #region Email sender
                    //_logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    #endregion


                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

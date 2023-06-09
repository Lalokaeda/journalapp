// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using journalapp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace journalapp.Areas.Identity.Pages.Account
{
    [Area("Admin")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Emp> _signInManager;
        private readonly UserManager<Emp> _userManager;
        private readonly IUserStore<Emp> _userStore;
        private readonly IUserEmailStore<Emp> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IEnumerable<SelectListItem> RolesDropDown;
        

        public RegisterModel(
            UserManager<Emp> userManager,
            IUserStore<Emp> userStore,
            SignInManager<Emp> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;

            RolesDropDown=_roleManager.Roles.Select(i => new SelectListItem{
                Text=i.Name,
                Value=i.Name
            });
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required (ErrorMessage = "Введите электронную почту")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required (ErrorMessage = "Введите пароль")]
            [StringLength(100, ErrorMessage = "Пароль должен быть длиной от 6 символов")]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>

              /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required (ErrorMessage = "Введите имя")]
            [Display(Name = "Имя")]
            public string Name { get; set; }
            [Required (ErrorMessage = "Введите фамилию")]
            [Display(Name = "Фамилия")]
            public string Surname { get; set; }
            
            [Display(Name = "Отчество")]
            public string Patronymic { get; set; }
            
            [Required (ErrorMessage = "Выберите должность")]
            [Display(Name = "Должность")]
            public string Role  {get; set;}
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            if(!await _roleManager.RoleExistsAsync(WC.AdminRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(WC.AdminRole));
                await _roleManager.CreateAsync(new IdentityRole(WC.PrepodRole));
            }

             if(!await _roleManager.RoleExistsAsync(WC.ZavOtdRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(WC.ZavOtdRole));
            }

           

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.Name=Input.Name;
                user.Surname=Input.Surname;
                if(Input.Patronymic!=null)
                user.Patronymic=Input.Patronymic;
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Input.Role);
                    _logger.LogInformation("User created a new account with password.");
                    returnUrl= Url.Content("/admin");
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            //         if (_userManager.Options.SignIn.RequireConfirmedAccount)
            //         {
            //             return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
            //         }
            //         else
            //         {
            //             await _signInManager.SignInAsync(user, isPersistent: false);
            //             return LocalRedirect(returnUrl);
            //         }
            return RedirectToPage("/EmpList", new { area = "Admin" });
                }
            //     foreach (var error in result.Errors)
            //     {
            //         ModelState.AddModelError(string.Empty, error.Description);
            //     }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private Emp CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Emp>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Emp)}'. " +
                    $"Ensure that '{nameof(Emp)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<Emp> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Emp>)_userStore;
        }
    }
}

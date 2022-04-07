// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using HM.Core.Constants;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hospital_management.Views.Contact
{
    public class ContactModel
    {
        public ContactModel()
        {

        }

        [ViewData]
        public string Title { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(50)]
            public string Name { get; set; }

            [Required]
            public string Problem { get; set; }
        }
    }
}


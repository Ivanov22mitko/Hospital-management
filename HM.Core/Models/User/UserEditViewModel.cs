﻿using System.ComponentModel.DataAnnotations;

namespace HM.Core.Models
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string? Role { get; set; }

        public string? Specialization { get; set; }
    }
}
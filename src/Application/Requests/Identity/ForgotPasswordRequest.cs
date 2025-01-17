﻿using System.ComponentModel.DataAnnotations;

namespace ReturneeManager.Application.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
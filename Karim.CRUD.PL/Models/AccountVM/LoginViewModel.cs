﻿namespace Karim.CRUD.PL.Models.AccountVM
{
    public class LoginViewModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

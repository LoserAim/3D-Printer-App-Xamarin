using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.Models
{
    public class LoginBindingModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegisterBindingModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

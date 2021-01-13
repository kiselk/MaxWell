using System;
using System.Collections.Generic;
using System.Text;

namespace MaxWell
{
    public class LoginResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTimeOffset ExpireAt { get; set; }
        public LoginState LoginState { get; set; }
        public string ErrorString { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Birthday { get; set; }
    }

    public enum LoginState
    {
        Failed,
        Canceled,
        Success
    }
}

﻿using System;

namespace MobilePhoneCardiography.Models
{
    public class User : IUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
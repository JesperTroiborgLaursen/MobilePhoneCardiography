﻿using System;
using System.IO;

namespace DTOs
{
    public interface IUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SSN { get; set; }

    }
}
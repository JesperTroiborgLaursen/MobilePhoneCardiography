using System;
using DTOs;

namespace EventArgss
{
    public class UserChangedEventArgs : EventArgs
    {
        public IUser CurrentUser { get; set; }
    }
}
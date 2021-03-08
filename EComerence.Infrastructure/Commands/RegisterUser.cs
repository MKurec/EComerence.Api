﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Infrastructure.Commands
{
    public class RegisterUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string City { get;  set; }

        public string Address { get;  set; }

        public string PostalCode { get;  set; }
    }
}

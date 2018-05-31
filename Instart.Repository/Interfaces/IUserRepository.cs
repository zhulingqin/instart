﻿using Instart.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface IUserRepository
    {
        User GetUser(string name, string password);
    }
}

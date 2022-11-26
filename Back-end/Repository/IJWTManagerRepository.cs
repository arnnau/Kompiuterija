using Kompiuterija.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuterija.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
    }

}
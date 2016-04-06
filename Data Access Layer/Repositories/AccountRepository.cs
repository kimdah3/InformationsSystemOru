﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class AccountRepository
    {
        //Kollar om användarnamn och lösenord stämmer 
        public bool Exists(string username, string password)
        {
            using (var context = new IsOruDbEntities())

            {
                return context.Account.Any(x => x.Username == username && x.Password == password);
            }
        }
    }
}

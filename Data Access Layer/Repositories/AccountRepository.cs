using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public void Addaccount(Account account)
        {
            using (var context = new IsOruDbEntities())
            {

                context.Account.Add(account);
                context.SaveChanges();
            }
        }

        public int GetIdFromUsername(string username)
        {
            using(var context = new IsOruDbEntities())
            {
                return context.Account.Where(x => x.Username.ToLower() == username.ToLower()).Select(x => x.UserID).First();
            }
        }

        public string GetUserNameFromId(int id)
        {
            using (var context = new IsOruDbEntities())
            {
                return context.Account.Where(x => x.Id == id).Select(x => x.Username).First();
            }
        }
    }
}

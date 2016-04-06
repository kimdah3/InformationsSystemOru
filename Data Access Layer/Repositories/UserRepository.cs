using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class UserRepository
    {
        public User GetUserFromId(int id)
        {
            using (var context = new IsOruDbEntities())
            {
                return context.User.Where(x => x.Id == id).First();
            }
        }

        public void AddUser(User user)
        {
            using (var context = new IsOruDbEntities())
            {

                context.User.Add(user);
                context.SaveChanges();
            }
        }

        public List<User> GetAllUsers()
        {
            using (var context = new IsOruDbEntities())
            {
                return context.User.ToList();
            }
        }
    }
}

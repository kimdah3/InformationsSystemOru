using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class UserRepository
    {
        public void AddUser(User user)
        {
            using (var context = new IsOruDbEntities() )
            {

                context.User.Add(user);
                context.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class AccessRepository
    {
    /*    public void AddAccess(User user, string accesslevel) {
            
            using (var context = new IsOruDbEntities())
            {
                context.User_Access.
            }
                
        }*/
        public bool IsAdmin(User user)
        {
            using (var context = new IsOruDbEntities())
            {
                return context.User_Access.Any(x => user.Id == x.UserId && x.AccessId == 1 || x.AccessId == 2);
            }
        }

        public List<int> GetUserIdsWithAccess()
        {
            using (var context = new IsOruDbEntities())
            {
                return context.User_Access.Select(x => x.UserId).ToList();
            }
        }
    }


}

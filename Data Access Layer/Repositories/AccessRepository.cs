using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class AccessRepository
    {
        public void AddAccess(int userId, int accessId)
        {
            using (var context = new IsOruDbEntities())
            {
                context.User_Access.Add(new User_Access { AccessId = accessId, UserId = userId });
                context.SaveChanges();
            }

        }

        public void RemoveAccess(int userId, int accessId)
        {
            using (var context = new IsOruDbEntities())
            {
                context.User_Access.Remove(context.User_Access.Where(x => userId == x.UserId && accessId == x.AccessId).FirstOrDefault());
                context.SaveChanges();
            }
        }


        public bool IsInformaticsAdmin(User user)
        {
            using (var context = new IsOruDbEntities())
            {
                return context.User_Access.Any(x => user.Id == x.UserId && x.AccessId == 1);
            }
        }

        public bool IsResearchAdmin(User user)
        {
            using (var context = new IsOruDbEntities())
            {
                return context.User_Access.Any(x => user.Id == x.UserId && x.AccessId == 2);
            }
        }

        public List<int> GetUserIdsWithInformaticsAccess()
        {
            using (var context = new IsOruDbEntities())
            {
                return context.User_Access.Where(x => x.AccessId == 3 && x.AccessId != 1).Select(x => x.UserId).ToList();
            }
        }

        public List<int> GetUserIdsWithResearchAccess()
        {
            using (var context = new IsOruDbEntities())
            {
                return context.User_Access.Where(x => x.AccessId == 4 && x.AccessId != 2).Select(x => x.UserId).ToList();
            }
        }

        public List<int> GetAdminIds()
        {
            using (var context = new IsOruDbEntities())
            {
                return context.User_Access.Where(x => x.AccessId == 1 || x.AccessId == 2).Select(x => x.UserId).ToList();
            }
        }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class PostTypeRepository
    {
        public List<PostType> GetAllPostTypes()
        {
            using (var db = new IsOruDbEntities())
            {
                return db.PostType.ToList();
            }
        }

        public PostType GetPostTypeById(int postTypeId)
        {
            using (var db = new IsOruDbEntities())
            {
                return db.PostType.First(x => x.Id == postTypeId);
            }
        }

        public List<PostType> GetAllPostTypesAfterId3()
        {
            using (var db = new IsOruDbEntities())
            {
                return db.PostType.Where(x => x.Id > 3).ToList();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class Post_PostTypeRespository
    {
        
        public void SavePosttype(int postid, int posttypeid)
        {
            using (var db = new IsOruDbEntities())
            {
                var newposttype = new Post_PostType
                {
                    PostId = postid,
                    PostTypeId = posttypeid
                };

                db.Post_PostType.Add(newposttype);
                db.SaveChanges();
            }
        }

        public List<int> GetAllPrivatePostIds()
        {
            using (var context = new IsOruDbEntities())
            {
                return context.Post_PostType.Where(x => x.PostTypeId == 1).Select(x => x.PostId).ToList();
            }
        } 
    }
}

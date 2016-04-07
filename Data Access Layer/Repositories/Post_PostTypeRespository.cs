using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    class Post_PostTypeRespository
    {
        
        public void savePosttype(int postid, int posttypeid)
        {
            using (var db = new IsOruDbEntities())
            {
                var newposttype = new Post_PostType();
                newposttype.PostId = postid;
                newposttype.PostTypeId = posttypeid;

                db.Post_PostType.Add(newposttype);
            }
        }
    }
}

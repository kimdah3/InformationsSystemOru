using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class User_Post_CommentRepository
    {
        public List<int> GetPostCommentIds(int postId)
        {
            using(var context = new IsOruDbEntities())
            {
                return context.User_Post_Comment.Where(x => x.PostId == postId).Select(x => x.CommentId).ToList();
            }
        }
    }
}

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

        public void SaveCommentPostRelation(int postId, int commentId)
        {
            using(var context = new IsOruDbEntities())
            {
                context.User_Post_Comment.Add(new User_Post_Comment
                {
                    PostId = postId,
                    CommentId = commentId
                });
                context.SaveChanges();
            }
        }
    }
}

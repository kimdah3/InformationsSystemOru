using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class CommentRepository
    {
        public Comment GetComment(int commentId)
        {
            using (var context = new IsOruDbEntities())
            {
                return context.Comment.First(x => x.Id == commentId);
            }
        }

        public void SaveComment(Comment comment)
        {
            using(var context = new IsOruDbEntities())
            {
                context.Comment.Add(comment);
                context.SaveChanges();
            }
        }

        public int GetNewId()
        {
            using(var context = new IsOruDbEntities())
            {
                return context.User_Post_Comment.Select(x => x.CommentId).Count()+1;
            }
        }
    }
}

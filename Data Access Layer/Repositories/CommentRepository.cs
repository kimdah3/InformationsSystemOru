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

        public void RemoveComment(int commentId)
        {
            using(var context = new IsOruDbEntities())
            {
                context.Comment.Remove(context.Comment.First(x => x.Id == commentId));
                context.SaveChanges();
            }
        }

        public int GetNewId()
        {
            using(var context = new IsOruDbEntities())
            {
                return context.Comment.Select(x => x.Id).Count()+1;
            }
        }

        public List<Comment> GetPostComments(int postId)
        {
            using (var context = new IsOruDbEntities())
            {
                return context.Comment.Where(x => x.PostId == postId).ToList();
            }
        }
        

    }
}

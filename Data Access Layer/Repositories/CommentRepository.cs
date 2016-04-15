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
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Configuration;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class PostRepository
    {


        public void SavePost(Post post, int posttypeid)
        {

            using (var db = new IsOruDbEntities())
            {
                db.Post.Add(post);
                db.SaveChanges();
            }

        }


        public List<Post> GetProfileBlogPosts(int profileid, List<int> privatePostId)
        {
            using (var db = new IsOruDbEntities())
            {
                return db.Post.Where(x => x.PostingUserID == profileid && privatePostId.Contains(x.Id)).OrderByDescending(x=> x.Date).ToList();
            }
        }

    }

}

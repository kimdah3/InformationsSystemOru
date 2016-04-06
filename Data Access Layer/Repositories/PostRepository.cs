using System;
using System.Collections.Generic;
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

        
        public List<Post> getInformalPostByUser(User user) 
        {
            using (var db = new IsOruDbEntities())
            {
                var postList = new List<Post>();

                var posttype = db.PostType.Where(x => x.Id == 1);
                foreach (var postvar in db.Post)
                {
                    
                }


                var result = db.Post.Where(x => x.PostingUserID.Equals(user.Id));

                    

                foreach (var text in result)
                {
                    postList.Add(text);
                }
                
                return postList;
            }

        }
        
        public void SavePost(Post post, String Type)
        {
            if (Type == "Private")
            {
                using (var db = new IsOruDbEntities())
                    {
                        db.Post.Add(post);
                        
                        int userid = post.PostingUserID;
                        db.Post_PostType.Add(userid, Ty);
                        db.SaveChanges();
                    }

            }

            //if(Type == "Informal")
            //{
            //    using (var db = new IsOruDbEntities())
            //    {
            //        db.Post.Add(post);
            //        int userid = post.PostingUserID;
            //        db.Post_PostType.Add(userid, Type);
            //        db.SaveChanges();
            //    }

            //}


        }

        public Post getAllPosts()
        {
            
        }

    }
}

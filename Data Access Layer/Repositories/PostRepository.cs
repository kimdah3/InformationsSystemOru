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
        public void SavePost(Post post)
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

        public List<Post> GetAllEducationPosts()
        {
            using (var db = new IsOruDbEntities())
            {
                var educationpostIDs = db.Post_PostType.Where(x => x.PostTypeId == 3).Select(x => x.PostId);
                return db.Post.Where(x => educationpostIDs.Contains(x.Id)).OrderByDescending(x => x.Date).ToList();
            }
        }

        public List<Post> GetAllInformalPosts()
        {
            using(var db = new IsOruDbEntities())
            {
                var privatepostIDs = db.Post_PostType.Where(x => x.PostTypeId == 1).Select(x => x.PostId);
                return db.Post.Where(x => privatepostIDs.Contains(x.Id)).OrderByDescending(x => x.Date).ToList();
            }
        }

        public List<Post> GetAllSciencePosts()
        {
            using(var db = new IsOruDbEntities())
            {
                var sciencepostIDs = db.Post_PostType.Where(x => x.PostTypeId == 2).Select(x => x.PostId);
                return db.Post.Where(x => sciencepostIDs.Contains(x.Id)).OrderByDescending(x => x.Date).ToList();
            }
        }

        public List<Post> GetAllNewsPosts()
        {
            using (var db = new IsOruDbEntities())
            {
                var neswpostIDs = db.Post_PostType.Where(x => x.PostTypeId == 4).Select(x => x.PostId);
                return db.Post.Where(x => neswpostIDs.Contains(x.Id)).OrderByDescending(x => x.Date).ToList();
            }
        }

        public void DeletePost(int postid)
        {
            using (var context = new IsOruDbEntities())
            {
                var post = context.Post_PostType.FirstOrDefault(x => x.Id == postid);
                context.Post_PostType.Remove(post);
                var post2 = context.Post.FirstOrDefault(x => x.Id == postid);
                context.Post.Remove(post2);
                context.SaveChanges();
            }
        }

        public List<string> CategoryListforEducation ()
        {
            var posttypeRep = new Post_PostTypeRespository();
            var educationIds = posttypeRep.GetAllEducationPostIds();
            using (var context = new IsOruDbEntities())
            {
                return context.Post.Where(x => educationIds.Contains(x.Id)).Select(x => x.Category).Distinct().ToList();
            }
        }

        public List<string> CategoryListforInformal()
        {
            var posttypeRep = new Post_PostTypeRespository();
            var InformalIds = posttypeRep.GetAllPrivatePostIds();
            using (var context = new IsOruDbEntities())
            {
                return context.Post.Where(x => InformalIds.Contains(x.Id)).Select(x => x.Category).Distinct().ToList();
            }
        }

        public List<Post> GetAllInformalPostsFromCategory(string cat)
        {
            var informalPosts = GetAllInformalPosts();
            return cat == "" ? informalPosts : informalPosts.Where(x => x.Category.ToLower() == cat.ToLower()).ToList();
        }

        public List<Post> GetAllEducationalPostsFromCategory(string cat)
        {
            var informalPosts = GetAllEducationPosts();
            return cat == "" ? informalPosts : informalPosts.Where(x => x.Category.ToLower() == cat.ToLower()).ToList();
        }

    }


}

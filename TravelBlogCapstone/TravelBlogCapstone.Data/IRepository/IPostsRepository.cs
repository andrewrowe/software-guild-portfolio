using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Data.IRepository
{
    public interface IPostsRepository
    {
        List<Post> GetAll();
        Post Get(int postid);
        Post Insert(Post post);
        void Update(Post post);
        void Delete(int postid);
        void ApprovePost(int postid);
        void DisapprovePost(Post post);
        List<Post> SelectPendingPosts();
        void SetUpdatedPostId(int originalPostId, int newPostId);
        void UpdatePost(Post post);
    }
}

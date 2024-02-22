using CodePulse.Models.Domain;

namespace CodePulse.Reposetories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateBlogPost(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllBlogPosts();
    }
}

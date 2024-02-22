using CodePulse.Models.Domain;
using CodePulse.Models.DTO;

namespace CodePulse.Reposetories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateBlogPost(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllBlogPosts();
        Task<BlogPost> GetBlogPostById(Guid id);
        Task<BlogPost?> EditBlogPost(BlogPost request);
    }
}

using CodePulse.Data;
using CodePulse.Models.Domain;
using CodePulse.Reposetories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.Reposetories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPost> CreateBlogPost(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPosts()
        {
            return await dbContext.BlogPosts.Include(b => b.Hotels).ToListAsync();
        }
    }
}

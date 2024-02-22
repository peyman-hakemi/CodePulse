using CodePulse.Data;
using CodePulse.Models.Domain;
using CodePulse.Models.DTO;
using CodePulse.Reposetories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<BlogPost?> GetBlogPostById(Guid id)
        {
            var blogPost = await dbContext.BlogPosts.Include(b => b.Hotels).FirstOrDefaultAsync(b => b.Id == id);

            if (blogPost == null)
            {
                return null;
            }

            return blogPost;
        }

        public async Task<BlogPost?> EditBlogPost(BlogPost request)
        {
            var existingBlogPost = await dbContext.BlogPosts.Include(x => x.Hotels)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (existingBlogPost == null) return null;

            dbContext.Entry(existingBlogPost).CurrentValues.SetValues(request);
            existingBlogPost.Hotels = request.Hotels;
            await dbContext.SaveChangesAsync();

            return request;
        }

    }
}

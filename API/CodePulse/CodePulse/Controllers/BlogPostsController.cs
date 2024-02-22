using CodePulse.Data;
using CodePulse.Models.Domain;
using CodePulse.Models.DTO;
using CodePulse.Reposetories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, IHotelRepository hotelRepository)
        {
            this.blogPostRepository = blogPostRepository;
            HotelRepository = hotelRepository;
        }

        public IHotelRepository HotelRepository { get; }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto blogPost)
        {
            var createBlogPostRequest = new BlogPost
            {
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Hotels = new List<Hotel>()
            };

            foreach (var hotelGuid in blogPost.Hotels)
            {
                var exsistingHotel = await HotelRepository.GetHotelById(hotelGuid);
                if (exsistingHotel is not null)
                {
                    createBlogPostRequest.Hotels.Add(exsistingHotel);
                }
            }

            createBlogPostRequest = await blogPostRepository.CreateBlogPost(createBlogPostRequest);

            var result = new BlogPostDto
            {
                UrlHandle = createBlogPostRequest.UrlHandle,
                Author = createBlogPostRequest.Author,
                Content = createBlogPostRequest.Content,
                FeaturedImageUrl = createBlogPostRequest.FeaturedImageUrl,
                Id = createBlogPostRequest.Id,
                IsVisible = createBlogPostRequest.IsVisible,
                PublishedDate = createBlogPostRequest.PublishedDate,
                ShortDescription = createBlogPostRequest.ShortDescription,
                Title = createBlogPostRequest.Title,
                Hotels = createBlogPostRequest.Hotels.Select(h => new HotelDto
                {
                    Id = h.Id,
                    AvailableFrom = h.AvailableFrom,
                    Description = h.Description,
                    Name = h.Name
                }).ToList(),

            };

            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await blogPostRepository.GetAllBlogPosts();

            var response = new List<BlogPostDto>();

            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Id = blogPost.Id,
                    IsVisible = blogPost.IsVisible,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle,
                    Hotels = blogPost.Hotels.Select(h => new HotelDto
                    {
                        Id = h.Id,
                        Name = h.Name,
                        Description = h.Description,
                        AvailableFrom = h.AvailableFrom,
                    }).ToList(),
                });
            }

            return Ok(response);
        }
    }
}

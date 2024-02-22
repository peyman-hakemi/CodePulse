using CodePulse.Data;
using CodePulse.Models.Domain;
using CodePulse.Models.DTO;
using CodePulse.Reposetories.Implementation;
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
        private readonly IHotelRepository hotelRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, IHotelRepository hotelRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.hotelRepository = hotelRepository;
        }


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
                var exsistingHotel = await hotelRepository.GetHotelById(hotelGuid);
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

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById(Guid id)
        {
            var blogPost = await blogPostRepository.GetBlogPostById(id);

            if(blogPost == null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
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
                Hotels = new List<HotelDto>()
            };

            foreach (var hotel in blogPost.Hotels)
            {
                response.Hotels.Add(new HotelDto
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    AvailableFrom = hotel.AvailableFrom,
                    Description = hotel.Description,
                });
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditBlogPost([FromRoute] Guid id, EditBlogPostRequestDto request)
        {
            var blogPost = new BlogPost
            {
                Id = id,
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Hotels = new List<Hotel>()
            };

            foreach (var hotel in request.Hotels)
            {
                var exsistingHotel = await hotelRepository.GetHotelById(hotel);
                if(exsistingHotel != null)
                blogPost.Hotels.Add(exsistingHotel);
            }

            var updatedBlogPost = await blogPostRepository.EditBlogPost(blogPost);

            if (updatedBlogPost == null) return NotFound();

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Hotels = blogPost.Hotels.Select(x => new HotelDto
                {
                    Id = x.Id,
                    AvailableFrom = x.AvailableFrom,
                    Description = x.Description,
                    Name = x.Name
                }).ToList(),
            };

         

            return Ok(blogPost);
        }
    }
}

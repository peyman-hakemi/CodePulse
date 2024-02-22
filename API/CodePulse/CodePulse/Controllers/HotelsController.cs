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
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository hotelRepository;

        public HotelsController(IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel(CreateHotelRequestDto request)
        {
            var hotel = new Hotel { Name = request.Name, AvailableFrom = request.AvailableFrom, Description = request.Description };

            await hotelRepository.CreateAsync(hotel);

            var result = new HotelDto
            {
                Name = hotel.Name,
                Description = hotel.Description,
                AvailableFrom = hotel.AvailableFrom,
                Id = hotel.Id
            };

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await hotelRepository.GetAllAsync();

            var response = new List<HotelDto>();
            foreach (var hotel in hotels)
            {
                response.Add(new HotelDto
                {
                    Name = hotel.Name,
                    AvailableFrom = hotel.AvailableFrom,
                    Description = hotel.Description,
                    Id = hotel.Id
                });
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetHotelById([FromRoute] Guid id)
        {
            var existingHotel = await hotelRepository.GetHotelById(id);
            if (existingHotel == null) return NotFound();

            var hotel = new HotelDto
            {
                Name = existingHotel.Name,
                AvailableFrom = existingHotel.AvailableFrom,
                Id = existingHotel.Id,
                Description = existingHotel.Description
            };

            return Ok(hotel);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditHotel([FromRoute] Guid id, UpdateHotelRequestDto request)
        {
            var hotel = new Hotel
            {
                AvailableFrom = request.AvailableFrom,
                Id = id,
                Description = request.Description,
                Name = request.Name,
            };

           hotel =  await hotelRepository.EditAsync(hotel);

            if(hotel ==  null) return NotFound();

            var response = new HotelDto
            {
                AvailableFrom = hotel.AvailableFrom,
                Id = hotel.Id,
                Description = hotel.Description,
                Name = hotel.Name,
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteHote([FromRoute] Guid id)
        {
            var hotel = await hotelRepository.DeleteHotelById(id);
            
            if(hotel == null)
            {
                return NotFound();
            }

            var response = new Hotel
            {
                AvailableFrom = hotel.AvailableFrom,
                Id = hotel.Id,
                Description = hotel.Description,
                Name = hotel.Name,
            };

            return Ok(response);
        }

    }
}

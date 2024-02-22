using CodePulse.Data;
using CodePulse.Models.Domain;
using CodePulse.Reposetories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.Reposetories.Implementation
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext dbContext;

        public HotelRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Hotel> CreateAsync(Hotel hotel)
        {
            await dbContext.Hotels.AddAsync(hotel);
            await dbContext.SaveChangesAsync();

            return hotel;
        }

        public async Task<Hotel?> DeleteHotelById(Guid id)
        {
           var existingHotel =  await dbContext.Hotels.FirstOrDefaultAsync(h =>  h.Id == id);

            if (existingHotel == null)
            {
                return null;
            }
            dbContext.Hotels.Remove(existingHotel);
            await dbContext.SaveChangesAsync();
            return existingHotel;
        }

        public async Task<Hotel?> EditAsync(Hotel hotel)
        {
            var exsistingHotel = await GetHotelById(hotel.Id);

            if( exsistingHotel != null )
            {
                dbContext.Entry(exsistingHotel).CurrentValues.SetValues(hotel);
                await dbContext.SaveChangesAsync();
                return hotel;
            }

            return null;

        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await dbContext.Hotels.ToListAsync();
        }

        public async Task<Hotel?> GetHotelById(Guid id)
        {
            return await dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}

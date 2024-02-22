using CodePulse.Models.Domain;

namespace CodePulse.Reposetories.Interface
{
    public interface IHotelRepository
    {
        Task<Hotel> CreateAsync(Hotel hotel);
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task<Hotel?> GetHotelById(Guid id);
        Task<Hotel?> EditAsync(Hotel hotel);
        Task<Hotel?> DeleteHotelById(Guid id);
    }
}

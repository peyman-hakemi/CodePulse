namespace CodePulse.Models.DTO
{
    public class UpdateHotelRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AvailableFrom { get; set; }
    }
}

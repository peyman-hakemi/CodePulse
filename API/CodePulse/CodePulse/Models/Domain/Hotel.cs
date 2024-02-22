namespace CodePulse.Models.Domain
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AvailableFrom { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}

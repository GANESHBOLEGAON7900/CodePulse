namespace CodePulse.API.Models.DTOs
{
    public class AddCategoryRequestDto
    {
        public string Name { get; set; }
        public string UrlHandle { get; set; }

        public ICollection<Guid> BlogPosts { get; set; }
    }
}

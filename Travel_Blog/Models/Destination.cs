namespace Travel_Blog.Models;

public class Destination
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public List<Blog>? Blogs { get; set; } = new List<Blog>();
}

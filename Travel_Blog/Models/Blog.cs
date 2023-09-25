namespace Travel_Blog.Models;

public class Blog
{
    public int Id { get; set; }
    public User User { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Image Image { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public List<Destination>? Destination { get; set; } = new List<Destination>();
}

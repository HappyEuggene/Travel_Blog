using Travel_Blog.Models;

namespace Travel_Blog.External;

public class ViewModelBlog
{
    public Blog Blog { get; set; }
    public Destination Destination { get; set; }
    public IFormFile ImageFile { get; set; }
}

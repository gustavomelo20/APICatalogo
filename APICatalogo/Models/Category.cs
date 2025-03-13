using System.Collections.ObjectModel;

namespace APICatalogo.Models;

public class Category
{
    public Category()
    {
        Products = new Collection<Product>();
    }

    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public string? imageUrl { get; set; }

    public ICollection<Product>? Products { get; set; }
}

using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var products = _context.Products.ToList();

        if (products is null)
        {
            return NotFound();
        }

        return products;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Product> Get(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

        if (product is null) {
            return NotFound(new { message = "Product Not Found" });
        }

        return product;
    }

    [HttpPost]
    public ActionResult Post(Product product)
    {
        if (product == null)
        {
            return BadRequest();
        }

        _context.Products.Add(product);
        _context.SaveChanges();

        return new CreatedAtActionResult("Get", "Products",
            new { id = product.ProductId }, product);
    }


    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Product product)
    {
        if (product == null || product.ProductId != id)
        {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
         var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

        if (product is null)
        {
            return NotFound(new { message = "Product Not Found" });
        }

        _context.Products.Remove(product);
        _context.SaveChanges();

        return Ok(product);
    }
}

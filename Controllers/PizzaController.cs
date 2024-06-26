using ATPizza.Models;
using ATPizza.Services;
using Microsoft.AspNetCore.Mvc;
namespace ATPizza.Controllers;
[ApiController][Route("[controller]")] 
public class PizzaController : ControllerBase 
{
    public PizzaController() { }

    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
        PizzaService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza == null)
            return NotFound();
        return pizza;
    }
        // POST action
        // PUT action
        // DELETE action
    }
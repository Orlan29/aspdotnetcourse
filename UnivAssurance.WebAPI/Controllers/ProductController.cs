using Microsoft.AspNetCore.Mvc;
using UnivAssurance.WebAPI.Logging;
using UnivAssurance.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;

namespace UnivAssurance.WebAPI.Controllers;
/**
    Le flag ApiController ici permet de spécifier que
    le controller en question devient un controller d'API
*/

[ApiController]
[Route("Products")]
public class ProductController : ControllerBase
{

    [HttpGet]
    [Route("")]
    public string GetAll()
    {
        return "Hello tous les Products";
    }

    [HttpGet]
    [Authorize]
    [Route("Products/{productId}")]
    public string GetProductById(int productId)
    {
        return "Hello un Product";
    }
}
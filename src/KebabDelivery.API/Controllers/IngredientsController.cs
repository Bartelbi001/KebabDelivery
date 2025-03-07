using Microsoft.AspNetCore.Mvc;

namespace KebabDelivery.API.Controllers;

[ApiController]
[Route("api/ingredients")]
public class IngredientsController : ControllerBase
{
    public IngredientsController(IIngredientService ingredientService)
    {

    }
}
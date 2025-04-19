using Microsoft.AspNetCore.Mvc;
using Muslim.Application.BusinessCards;
using Muslim.Domain.BusinessCards.Dtos;

namespace Api.Controllers;

public class BusinessCardController(IBusinessCardService service) : ApiControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBusinessCardDto businessCard)
    {
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok();
    }
}

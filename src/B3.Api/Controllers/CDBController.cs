using B3.Api.Interfaces;
using B3.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace B3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CdbController : BaseApiController
    {
        private readonly ICdbService _cdbService;

        public CdbController(ICdbService cdbService)
        {
            _cdbService = cdbService;
        }

        [HttpPost("Calcular")]
        public IResult CalcularCDB(CalculoCdb model)
        {
            if (ModelState.IsValid && !model.EhValido())
                return Results.BadRequest(model.ErrosValidacao);

            var cdb = _cdbService.Calcular(model);
            return Results.Ok(cdb);
        }
    }
}

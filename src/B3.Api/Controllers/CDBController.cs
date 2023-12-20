using B3.Api.Interfaces;
using B3.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace B3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CDBController : BaseApiController
    {
        private readonly ICDBService _cdbService;

        public CDBController(ICDBService cdbService)
        {
            _cdbService = cdbService;
        }

        [HttpPost("Calcular")]
        public IResult CalcularCDB(CalculoCDB model)
        {
            if (ModelState.IsValid)
                if (!model.EhValido())
                    return Results.BadRequest(model.ErrosValidacao);

            var cdb = _cdbService.Calcular(model);
            return Results.Ok(cdb);
        }
    }
}

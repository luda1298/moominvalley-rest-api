using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MoominsApi.Repo;

namespace MoominsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoominValleyController : ControllerBase
{
    // palauttaa API kaikki laakson muumit ilman name parametria
    // Annettaessa name parametri, palauttaa API listan muumeista, joiden nimessä esiintyy name parametriin annettu arvo.
    
        [HttpGet]
    public ActionResult<List<Moomin>> GetAllMoomins([FromQuery] string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Ok(MoominsRepository.GetAllMoomins());

        return Ok(MoominsRepository.GetAllMoomins(name));
    }
    

    // Uuden Muumin lisääminen
    [HttpPost]
    public ActionResult AddMoomin(Moomin moomin)
    {
        if (moomin == null || string.IsNullOrWhiteSpace(moomin.Name))
            return BadRequest("Muumin tiedot puuttuvat tai ovat virheelliset.");

        if (MoominsRepository.GetMoomin(moomin.Number) != null)
            return Conflict($"Muumi numero {moomin.Number} on olemassa");

        MoominsRepository.AddMoomin(moomin);

        // Kirjoitetaan location manuaalisesti 
        var location = $"/api/MoominValley/{moomin.Number}";
        return Created(location, moomin);
    }

    // Muumin haku numerolla
    [HttpGet("{number}")]
    public ActionResult<Moomin> GetMoomin(int number)
    {
        var moomin = MoominsRepository.GetMoomin(number);

        if (moomin == null)
        {
            return NotFound();
        }

        return Ok(moomin);
    }

    // Muumin poistaminen numerolla
    [HttpDelete("{number}")]
    public ActionResult DeleteMoomin(int number)
    {
        bool deleted = MoominsRepository.DeleteMoomin(number);

        if (!deleted)
            return NotFound();

        return NoContent(); // 204
    }




}

using gtm.Db;
using gtm.Dto;
using gtm.Searches;
using gtm.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gtm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        ILogger<DeckController> _logger;
        GtmContext _dbContext;

        public DeckController(ILogger<DeckController> logger, GtmContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DeckDto.ResponseDto>> Get([FromRoute] DeckSearch search)
        {
            var decks = _dbContext.Decks
                .Where(deck => 
                    (search.Id == null || deck.Id == search.Id)
                    && (search.Name == null || deck.Name.ToLower().Equals(search.Name.ToLower())))
                .Select(d => new DeckDto.ResponseDto()
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description
                })
                .AsEnumerable();

            return Ok(decks);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<DeckDto.ResponseDto> GetById(int id)
        {
            var deck = _dbContext.Decks
                .Where(deck => deck.Id == id)
                .Select(deck => new DeckDto.ResponseDto()
                {
                    Id = deck.Id,
                    Name = deck.Name,
                    Description = deck.Description
                });

            return Ok(deck);
        }


}

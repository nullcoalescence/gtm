using gtm.Db;
using gtm.Db.Models;
using gtm.Dto;
using gtm.Searches;
using Microsoft.AspNetCore.Mvc;

namespace gtm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly ILogger<DeckController> _logger;
        private readonly GtmContext _dbContext;

        public DeckController(ILogger<DeckController> logger, GtmContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DeckDto.ResponseDto>> Get([FromQuery] DeckSearch search)
        {
            var deck = _dbContext.Decks
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

            return Ok(deck);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<DeckDto.ResponseDto> GetById([FromRoute] int id)
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

        [HttpPost]
        public ActionResult<DeckDto.ResponseDto> Post([FromBody] DeckDto.RequestDto request)
        {
            var deck = new Deck()
            {
                Name = request.Name,
                Description = request.Description
            };

            var added = _dbContext.Decks.Add(deck).Entity;
            _dbContext.SaveChanges();

            return new DeckDto.ResponseDto()
            {
                Id = added.Id,
                Name = added.Name,
                Description = added.Description
            };
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<DeckDto.ResponseDto> Put([FromRoute] int id, [FromBody] DeckDto.RequestDto request)
        {
            var toUpdate = _dbContext.Decks
                .Where(deck => deck.Id == id)
                .First();

            if (toUpdate == null)
            {
                return NotFound();
            }

            toUpdate.Name = request.Name;
            toUpdate.Description = request.Description;

            _dbContext.Decks.Update(toUpdate);
            _dbContext.SaveChanges();

            return Ok(toUpdate);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var toDelete = _dbContext.Decks
                .Where(deck => deck.Id == id)
                .First();

            if (toDelete == null)
            {
                return NotFound();
            }

            _dbContext.Decks.Remove(toDelete);
            _dbContext.SaveChanges();

            return Ok();
        }

    }
}

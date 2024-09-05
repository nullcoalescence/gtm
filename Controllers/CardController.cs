using gtm.Db;
using gtm.Db.Models;
using gtm.Dto;
using gtm.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gtm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly GtmContext _dbContext;

        public CardController(ILogger<CardController> logger, GtmContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CardDto.ResponseDto>> Get([FromQuery] CardSearch search)
        {
            var cards = _dbContext.Cards
                .Where(card =>
                    (search.Id == null || card.Id == search.Id)
                    && (search.Name == null || card.Name.ToLower().Equals(search.Name.ToLower())))
                .Select(c => new CardDto.ResponseDto()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .AsEnumerable();

            return Ok(cards);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<CardDto.ResponseDto> GetById([FromRoute] int id)
        {
            var card = _dbContext.Cards
                .Where(card => card.Id == id)
                .Select(card => new CardDto.ResponseDto()
                {
                    Id = card.Id,
                    Name = card.Name
                });

            return Ok(card);
        }

        [HttpPost]
        public ActionResult<CardDto.ResponseDto> Post([FromBody] CardDto.RequestDto request)
        {
            var card = new Card()
            {
                Name = request.Name
            };

            var added = _dbContext.Cards.Add(card).Entity;
            _dbContext.SaveChanges();

            return new CardDto.ResponseDto()
            {
                Id = added.Id,
                Name = added.Name
            };
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<CardDto.ResponseDto> Put([FromRoute] int id, [FromBody] CardDto.RequestDto request)
        {
            var toUpdate = _dbContext.Cards
                .Where(card => card.Id == id)
                .First();

            if (toUpdate == null)
            {
                return NotFound();
            }

            toUpdate.Name = request.Name;

            _dbContext.Cards.Update(toUpdate);
            _dbContext.SaveChanges();

            return Ok(toUpdate);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var toDelete = _dbContext.Cards
                .Where(card => card.Id == id)
                .First();

            if (toDelete == null)
            {
                return NotFound();
            }

            _dbContext.Cards.Remove(toDelete);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}

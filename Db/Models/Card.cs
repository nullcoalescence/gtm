using System.ComponentModel.DataAnnotations;

namespace gtm.Db.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } // placholder

        /*
         *  The rest of this model will be filled in once I get an MTG api configured,
         *  because at that point, various things will need to be considered:
         *  - How this card is linked to MTG API (MTG_API_ID)?
         *  - Caching MTG API data
         *      - Caching MTG API data to this model?
         *      - A seperate table?
         *      - How much will be cached vs queried from the MTG API each request?
         *      
         *      
         *  - Linking a card to a deck
         *      - Can a card belong to multiple decks
         *          - Warning
         */
    }
}

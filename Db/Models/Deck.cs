using System.ComponentModel.DataAnnotations;

namespace gtm.Db.Models
{
    public class Deck
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
    }
}

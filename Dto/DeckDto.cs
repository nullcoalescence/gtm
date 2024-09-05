namespace gtm.Dto
{
    public class DeckDto
    {
        public class ResponseDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string? Description { get; set; }
        }

        public class RequestDto
        {
            public string Name { get; set; }
            public string? Description { get; set; }
        }
    }
}

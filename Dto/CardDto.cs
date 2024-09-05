namespace gtm.Dto
{
    public class CardDto
    {   
        public class ResponseDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class RequestDto
        {
            public string Name { get; set; }
        }
    }
}

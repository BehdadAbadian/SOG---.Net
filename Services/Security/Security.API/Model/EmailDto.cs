namespace Security.API.Model
{
    public class EmailDto
    {
        public string Sender { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}

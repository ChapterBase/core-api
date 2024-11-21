namespace admin_bff.Exceptions
{
    public class ChapterBaseException: Exception
    {
        public int StatusCode { get; set; }

        public ChapterBaseException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}

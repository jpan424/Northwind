namespace NorthwindsPractice.Controllers.Models
{
    public class ProductResponseModel<T>
    {
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
    }
}

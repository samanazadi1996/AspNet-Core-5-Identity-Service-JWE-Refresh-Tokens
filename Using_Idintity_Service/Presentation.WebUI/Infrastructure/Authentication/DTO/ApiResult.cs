namespace Presentation.WebUI.Infrastructure.Authentication.DTO
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
namespace Identity.Client.DTO
{
    public class ApiResult<T> : ApiResult
    {
        public T Data { get; set; }
    }
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
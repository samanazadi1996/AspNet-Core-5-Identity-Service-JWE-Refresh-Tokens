namespace Services.CommonServices.Abstraction
{
    public interface IServiceResult<TData>
    {
        public abstract TData Data { get; set; }
        public abstract string ErrorMessage { get; set; }
        public abstract bool IsSuccess { get; set; }

        public abstract IServiceResult<TData> Ok(TData data);
        public abstract IServiceResult<TData> Fail(string errorMessage);

    }
}

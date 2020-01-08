namespace JwtService.Commons
{
    public static class ObjectExtension
    {
        public static Result<T> ToResult<T>(this T obj)
        {
            return new Result<T>().Ok(obj);
        }
    }
}

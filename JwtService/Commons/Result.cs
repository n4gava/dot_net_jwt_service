using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace JwtService.Commons
{
    public class Result : Result<bool?>
    {
        public static Result DoAndReturnResult(Action action)
        {
            var result = new Result();
            try
            {
                action();
            }
            catch (Exception e)
            {
                result.Add(e);
            }

            return result;
        }

        public static async Task<Result> DoAndReturnResultAsync(Func<Task> action)
        {
            var result = new Result();
            try
            {
                await action();
            }
            catch (Exception e)
            {
                result.Add(e);
            }

            return result;
        }
        public Result() : base()
        {

        }

        public Result(string errorMessage) : base(errorMessage)
        {

        }
    }
}

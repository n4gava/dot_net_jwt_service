using System.Collections.Generic;
using System;

namespace JwtService.Commons
{
    public class Result : Result<bool?>
    {
        public Result() : base()
        {

        }

        public Result(string errorMessage) : base(errorMessage)
        {

        }
    }
}

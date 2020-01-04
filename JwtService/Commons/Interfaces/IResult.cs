using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtService.Commons.Interfaces
{
    public interface IResult
    {
        List<string> Errors { get; }

        bool Succeeded { get; }

        bool Failed { get; }
    }
}

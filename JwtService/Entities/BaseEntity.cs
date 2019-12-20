using JwtService.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtService.Entities
{
    public class BaseEntity : IEntity
    {
        public long ID { get; set; }
    }
}

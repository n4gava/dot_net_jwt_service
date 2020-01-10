using JwtService.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtService.Entities
{
    public class BaseEntity : IEntity
    {
        [Key]
        public long ID { get; private set; }
    }
}

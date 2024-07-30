using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Domain.Entities
{
    public class AnonymousUser
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Domain.Entities
{
    public class AnonymousUser
    {
        public Guid UserId { get; set; }
        public string Hash { get; set; } = string.Empty;
    }
}

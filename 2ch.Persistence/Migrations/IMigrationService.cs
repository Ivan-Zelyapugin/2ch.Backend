﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Persistence.Migrations
{
    public interface IMigrationService
    {
        void RunMigrations();
    }
}

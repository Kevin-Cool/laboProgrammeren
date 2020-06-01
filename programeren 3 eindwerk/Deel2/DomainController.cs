using Microsoft.EntityFrameworkCore;
using programeren_3_eindwerk.classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace programeren_3_eindwerk.Deel2
{
    class DomainController
    {
        private readonly ProvinciesContext _context;
        private readonly DbSet<Provincie> _provincies;

        public DomainController()
        {

        }


    }
}

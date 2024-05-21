using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASM2_KSTH.Models;

namespace ASM2_KSTH.Data
{
    public class ASM2_KSTHContext : DbContext
    {
        public ASM2_KSTHContext (DbContextOptions<ASM2_KSTHContext> options)
            : base(options)
        {
        }

        public DbSet<ASM2_KSTH.Models.Test> Test { get; set; } = default!;
        public DbSet<ASM2_KSTH.Models.Diemdanh> Diemdanh { get; set; } = default!;
    }
}

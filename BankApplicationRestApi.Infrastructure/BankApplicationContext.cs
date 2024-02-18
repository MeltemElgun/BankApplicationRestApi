using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplicationRestApi.Infrastructure
{
    public class BankApplicationContext:DbContext
    {
        public BankApplicationContext() { }


        public BankApplicationContext(DbContextOptions<BankApplicationContext> options)
            :base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

    }

}

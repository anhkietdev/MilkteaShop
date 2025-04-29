using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL.Repositories.Implements
{
        public class StoreRepository : Repository<Store>, IStoreRepository
    {

            public StoreRepository(AppDbContext context) : base(context)
            {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Data
{
    public class SQLDb
    {
        private readonly Context _context;
        public SQLDb(Context Context)
        {
            _context = Context;
        }
        
    }
}

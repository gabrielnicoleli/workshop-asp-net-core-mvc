using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class SalesRecordService 
    {
        private readonly SalesWebMVCContext _context;

        public SalesRecordService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? dtMin, DateTime? dtMax)
        {
            var result = from obj in _context.SalesRecord select obj;

            if(dtMin.HasValue)
            {
                result = result.Where(x => x.Date >= dtMin.Value);
            }
            if (dtMax.HasValue)
            {
                result = result.Where(x => x.Date <= dtMax.Value);
            }

            return await result
                 .Include(x => x.Seller)
                 .Include(x => x.Seller.Department)
                 .OrderByDescending(x => x.Date)
                 .ToListAsync();

        }

    }
}

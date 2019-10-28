using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRApplication.BusinessLogic.Services
{
    public class ApplicationService: IApplicationService
    {

        private readonly HRAppDBContext _context;

        public ApplicationService(HRAppDBContext context)
        {
            _context = context;
        }

        public List<Offers> GetAllJobOffers()
        {
            return _context.Offers
                    .Include(x=>x.ContractType)
                    .Where(x => x.EndDate >= DateTime.Now)
                    .ToList();
        }

        public Offers GetOfferById(Guid id)
        {
            return _context.Offers
                    .Include(x=>x.ContractType)
                    .Where(x => x.Id == id)
                    .First();
        }
    }
}

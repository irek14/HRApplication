using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRApplication.BusinessLogic.Services
{
    public class AdminService : IAdminService
    {
        private readonly HRAppDBContext _context;
        private IConfiguration _configuration;

        public AdminService(HRAppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public List<Applications> GetAllApplications()
        {
            var result = _context.Applicationss
                        .Include(x => x.CreatedBy)
                        .Include(x => x.Offer)
                        .Include(x => x.Offer.CreatedBy);

            return result.ToList();
        }
    }
}

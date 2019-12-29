using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRApplication.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        HRAppDBContext _context;

        public UserService()
        {
            //_context = context;
        }

        public void test()
        {
            var a = _context;
        }
    }
}

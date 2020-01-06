using HRApplication.BusinessLogic.Services;
using HRApplication.DataAccess.Entities;
using HRApplication.WWW.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        HRAppDBContext context;
        Guid offerId = Guid.NewGuid();
        Guid contractType = Guid.Parse("4BFCDF2F-4051-4417-BC9C-81B3B2B70828");
        Guid createdById = Guid.Parse("D5B5066E-9E18-470B-90B0-79A8671ED365");
        Guid hrRole = Guid.Parse(Ids.HRRoleId);
        Guid userRole = Guid.Parse(Ids.UserRoleId);

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HRAppDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            context = new HRAppDBContext(options);

            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque tincidunt mauris vel nulla ultrices, quis aliquam libero lacinia. Phasellus at fringilla metus. Praesent ut orci ligula. Phasellus finibus ex elit, et cursus ipsum varius eu. Vivamus ipsum diam, posuere ac elementum a, malesuada lobortis elit. Aliquam mollis hendrerit sapien. Mauris iaculis sodales leo eu elementum. Pellentesque augue tortor, rutrum eget malesuada eget, interdum eu dui. Nunc ac bibendum elit. Duis quis auctor est. ";
            string position = "Test";
            int SalaryFrom = 3000;
            int SalaryTo = 4000;
            //

            if (!context.Users.Any())
                context.Users.Add(new Users() { Id = createdById });

            if (!context.ContractTypes.Any())
                context.ContractTypes.Add(new ContractTypes() { Id = contractType });

            if (!context.Offers.Any())
                context.Offers.Add(new Offers() { Id = offerId, Description = description, CreatedById = createdById, Position = position, SalaryFrom = SalaryFrom, SalaryTo = SalaryTo, ContractTypeId = contractType });

            if (!context.Applicationss.Any())
                context.Applicationss.Add(new Applications() { Id = Guid.NewGuid(), CreatedById = createdById, OfferId = offerId });

            context.SaveChanges();
        }

        [Test]
        public void GetJobOfferToEditTest()
        {
            JobOfferService service = new JobOfferService(context);

            var result = service.GetJobOfferToEdit(offerId);

            if (result != null)
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void DeleteJobOffer()
        {
            var service = new JobOfferService(context);

            service.DeleteJobOffer(offerId);

            var result = context.Offers.First();

            if (result.IsArchived)
                Assert.Pass();

            Assert.Fail();
        }

        [Test]
        public void GetAllApplications()
        {
            var service = new AdminService(context, new ConfigurationRoot(new List<IConfigurationProvider>()));

            Assert.Throws<System.NullReferenceException>(() => service.GetAllApplications(null, null, "", ""));

        }

        [Test]
        public void GetAllUsersTest()
        {
            var service = new AdminService(context, new ConfigurationRoot(new List<IConfigurationProvider>()));

            var result = service.GetAllUsers();

            if (result.Count == context.Users.Count())
                Assert.Pass();

            Assert.Fail();

        }

    }
}
﻿using HRApplication.BuisnessEntities.Enums;
using HRApplication.BusinessLogic.Interfaces;
using HRApplication.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRApplication.BusinessLogic.Models.JobOffer;
using HRApplication.WWW.Models.JobOffer;
using DetailsJobOfferViewModel = HRApplication.BusinessLogic.Models.JobOffer.DetailsJobOfferViewModel;

namespace HRApplication.BusinessLogic.Services
{
    public class JobOfferService : IJobOfferService
    {
        private readonly HRAppDBContext _context;

        public JobOfferService(HRAppDBContext context)
        {
            _context = context;
        }

        public async Task CreateJobOffer(string title, string description, Guid contractType, string salaryFrom, string salaryTo, bool partTime, decimal? weekHours, string position, DateTime? endDate, Guid userId)
        {
            Offers offer = new Offers {
                Id = Guid.NewGuid(),
                ContractTypeId = contractType,
                CreatedById = userId,
                CreatedOn = DateTime.Now,
                Description = description,
                HoursPerWeek = weekHours,
                EndDate = endDate,
                Position = position,
                PartTimeWork = partTime,
                Title = title,
                SalaryFrom = salaryFrom == null ?(int?)null : int.Parse(salaryFrom),
                SalaryTo = salaryTo == null ? (int?)null : int.Parse(salaryTo)
            };

            _context.Offers.Add(offer);

            await _context.SaveChangesAsync();
        }

        public void DeleteJobOffer(Guid offerId)
        {
            Offers offer = (from offers in _context.Offers
                            where offers.Id == offerId
                            select offers).FirstOrDefault();

            if (offer == null)
                return;

            offer.IsArchived = true;

            _context.SaveChanges();
        }

        public void EditJobOffer(NewJobOfferViewModel offer)
        {
            Offers toEdit = (from offers in _context.Offers
                             where offers.Id == offer.Id
                             select offers).FirstOrDefault();

            if (toEdit == null)
                return;

            toEdit.HoursPerWeek = offer.HoursPerWeek;
            toEdit.PartTimeWork = offer.PartTimeWork;
            toEdit.Position = offer.Position;
            toEdit.SalaryFrom = offer.SalaryFrom == null ? (int?)null : int.Parse(offer.SalaryFrom);
            toEdit.SalaryTo = offer.SalaryTo == null ? (int?)null : int.Parse(offer.SalaryTo);
            toEdit.Title = offer.Title;
            toEdit.EndDate = offer.EndDate;
            toEdit.Description = offer.Description;
            toEdit.ContractTypeId = offer.ContractTypeId;

            _context.SaveChanges();
        }

        public List<TableJobOfferViewModel> GetAllMyOffers(Guid hrMemberId)
        {
            return (from offer in _context.Offers
                   where offer.CreatedById == hrMemberId && !offer.IsArchived
                   select new TableJobOfferViewModel()
                   {
                       Id = offer.Id,
                       ContractType = offer.ContractType.ContractTypeName,
                       Position = offer.Position,
                       Salary = offer.SalaryFrom + "-" + offer.SalaryTo,
                       Title = offer.Title
                   }).ToList();
        }

        public List<SelectListItem> GetContractTypes()
        {
            var contractTypes = from type in _context.ContractTypes
                         select new { typeId = type.Id.ToString(),typeName = type.ContractTypeName };

            List<SelectListItem> result = new List<SelectListItem>();

            foreach(var type in contractTypes)
            {
                result.Add(new SelectListItem(type.typeName,type.typeId));
                
            }

            return result;
        }

        public NewJobOfferViewModel GetJobOfferToEdit(Guid offerId)
        {
            var result = (from offer in _context.Offers
                         where offer.Id == offerId
                         select new NewJobOfferViewModel
                         {
                             Id = offer.Id,
                             ContractTypeId = (Guid)offer.ContractTypeId,
                             Description = offer.Description,
                             EndDate = offer.EndDate,
                             HoursPerWeek = (decimal)offer.HoursPerWeek,
                             PartTimeWork = offer.PartTimeWork,
                             Position = offer.Position,
                             SalaryFrom = offer.SalaryFrom.ToString(),
                             SalaryTo = offer.SalaryTo.ToString(),
                             Title = offer.Title
                         }).FirstOrDefault();

            return result;
        }

        public DetailsJobOfferViewModel GetJobOfferWithId(Guid id)
        {
            var result = (from jobOffer in _context.Offers
                         join contract in _context.ContractTypes on jobOffer.ContractTypeId equals contract.Id
                         join hr in _context.Users on jobOffer.CreatedById equals hr.Id
                         where jobOffer.Id == id
                         select new DetailsJobOfferViewModel()
                         {
                             Id = jobOffer.Id,
                             Salary = jobOffer.SalaryFrom.ToString() + "-" + jobOffer.SalaryTo.ToString(),
                             Title = jobOffer.Title,
                             Position = jobOffer.Position,
                             IsArchived = jobOffer.IsArchived,
                             EndDate = jobOffer.EndDate,
                             Description = jobOffer.Description,
                             HoursPerWeek = jobOffer.HoursPerWeek,
                             PartTimeWork = jobOffer.PartTimeWork,
                             ContractType = contract.ContractTypeName,
                             CreatedBy = hr.FirstName + " " + hr.LastName,
                             CreatedOn = jobOffer.CreatedOn
                         }).FirstOrDefault();

            return result;
        }
    }
}

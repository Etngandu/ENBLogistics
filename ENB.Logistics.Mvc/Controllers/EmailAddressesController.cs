using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ENB.Test.Logistics.Infrastructure;
using ENB.Test.Logistics.Entities;
using ENB.Logistics.Mvc.Models;
using ENB.Test.Logistics.Repositories.EF;
using ENB.Test.Logistics.Entities.Repositories;


namespace ENB.Logistics.Mvc.Controllers
{
    public class EmailAddressesController : Controller
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;

        /// <summary>
        /// Initializes a new instance of the EmailAddressesController class.
        /// </summary>
        public EmailAddressesController(IOperatorRepository operatorRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper iMapper)
        {
            _operatorRepository = operatorRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = iMapper;
        }

        public ActionResult List(int operatorId)
        {
            ViewBag.operatorId = operatorId;
            var operatorx = _operatorRepository.FindById(operatorId, x => x.EmailAddresses);
            ViewBag.Message = operatorx.FullName;
            var data = new List<DisplayEmailAddress>();
            _imapper.Map(operatorx.EmailAddresses, data);
            return View(data);
        }

        public ActionResult Details(int id, int operatorId)
        {
            ViewBag.OperatorId = operatorId;
            var operatorx = _operatorRepository.FindById(operatorId, x => x.EmailAddresses);
            ViewBag.Message = operatorx.FullName;
            var data = new DisplayEmailAddress();
            _imapper.Map(operatorx.EmailAddresses.First(x => x.Id == id), data);
            return View(data);
        }

        public ActionResult Create(int operatorId)
        {
            ViewBag.OperatorId = operatorId;
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateAndEditEmailAddress createAndEditEmailAddress)
        {
            ViewBag.operatorid = createAndEditEmailAddress.OperatorId;
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var operatorx = _operatorRepository.FindById(createAndEditEmailAddress.OperatorId);
                        var emailAddress = new EmailAddress();
                        _imapper.Map(createAndEditEmailAddress, emailAddress);
                        operatorx.EmailAddresses.Add(emailAddress);
                        return RedirectToAction("List", new { createAndEditEmailAddress.OperatorId });
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage);
                    }
                }
            }
            return View();
        }

        public ActionResult Edit(int id, int operatorId)
        {
            ViewBag.OperatorId = operatorId;

            var operatorx = _operatorRepository.FindById(operatorId, x => x.EmailAddresses);
            if (operatorx == null)
            {
                return HttpNotFound();
            }
            var data = new CreateAndEditEmailAddress();
            _imapper.Map(operatorx.EmailAddresses.Single(x => x.Id == id), data);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(CreateAndEditEmailAddress createAndEditEmailAddress)
        {
            ViewBag.OperatorId = createAndEditEmailAddress.OperatorId;

            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var operatorx = _operatorRepository.FindById(createAndEditEmailAddress.OperatorId, x => x.EmailAddresses);
                        var emailAddress = operatorx.EmailAddresses.Single(x => x.Id == createAndEditEmailAddress.Id);
                        _imapper.Map(createAndEditEmailAddress, emailAddress);
                        return RedirectToAction("List", new { createAndEditEmailAddress.OperatorId });
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage);
                    }
                }
            }
            return View();
        }

        public ActionResult Delete(int id, int operatorId)
        {
            ViewBag.OperatorId = operatorId;
            var operatorx = _operatorRepository.FindById(operatorId, x => x.EmailAddresses);
            if (operatorx == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = operatorx.FullName;
            var data = new DisplayEmailAddress();
            _imapper.Map(operatorx.EmailAddresses.Single(x => x.Id == id), data);
            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(DisplayEmailAddress displayEmailAddress)
        {
            using (_unitOfWorkFactory.Create())
            {
                var operatorx = _operatorRepository.FindById(displayEmailAddress.OperatorId, x => x.EmailAddresses);
                var address = operatorx.EmailAddresses.Single(x => x.Id == displayEmailAddress.Id);
                operatorx.EmailAddresses.Remove(address);
                return RedirectToAction("List", new { displayEmailAddress.OperatorId });
            }
        }
    }

}
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
    public class PhoneNumbersController : Controller
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;

        /// <summary>
        /// Initializes a new instance of the PhoneNumbersController class.
        /// </summary>
        public PhoneNumbersController(IOperatorRepository operatorRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _operatorRepository = operatorRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }

        public ActionResult List(int operatorId)
        {
            ViewBag.OperatorId = operatorId;
            var operatox = _operatorRepository.FindById(operatorId, x => x.PhoneNumbers);
            ViewBag.Message = operatox.FullName;
            var data = new List<DisplayPhoneNumber>();
            _imapper.Map(operatox.PhoneNumbers, data);
            return View(data);
        }

        public ActionResult Details(int id, int operatorId)
        {
            ViewBag.OperatorId = operatorId;

            var operatorx = _operatorRepository.FindById(operatorId, x => x.PhoneNumbers);
            ViewBag.Message = operatorx.FullName;
            var data = new DisplayPhoneNumber();
            _imapper.Map(operatorx.PhoneNumbers.Single(x => x.Id == id), data);
            return View(data);
        }

        public ActionResult Create(int operatorId)
        {
            ViewBag.OperatorId = operatorId;
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateAndEditPhoneNumber createAndEditPhoneNumber, int operatorId)
        {
            ViewBag.OperatorId = operatorId;
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var operatorx = _operatorRepository.FindById(operatorId);
                        var phoneNumber = new PhoneNumber();
                        _imapper.Map(createAndEditPhoneNumber, phoneNumber);
                        operatorx.PhoneNumbers.Add(phoneNumber);
                        return RedirectToAction("List", new { operatorId });
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

            var operatorx = _operatorRepository.FindById(operatorId, x => x.PhoneNumbers);
            if (operatorx == null)
            {
                return HttpNotFound();
            }
            var data = new CreateAndEditPhoneNumber();
            _imapper.Map(operatorx.PhoneNumbers.Single(x => x.Id == id), data);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(CreateAndEditPhoneNumber createAndEditPhoneNumber, int operatorId)
        {
            ViewBag.OperatorId = operatorId;
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var operatorx = _operatorRepository.FindById(operatorId, x => x.PhoneNumbers);
                        var phoneNUmber = operatorx.PhoneNumbers.Single(x => x.Id == createAndEditPhoneNumber.Id);
                        _imapper.Map(createAndEditPhoneNumber, phoneNUmber);
                        return RedirectToAction("List", new { operatorId });
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

            var operatorx = _operatorRepository.FindById(operatorId, x => x.PhoneNumbers);
            var data = new DisplayPhoneNumber();
            _imapper.Map(operatorx.PhoneNumbers.Single(x => x.Id == id), data);
            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(DisplayPhoneNumber displayPhoneNumber, int operatorId)
        {
            ViewBag.OperatorId = operatorId;

            using (_unitOfWorkFactory.Create())
            {
                var operatorx = _operatorRepository.FindById(operatorId, x => x.PhoneNumbers);
                var phoneNumber = operatorx.PhoneNumbers.Single(x => x.Id == displayPhoneNumber.Id);
                operatorx.PhoneNumbers.Remove(phoneNumber);
                return RedirectToAction("List", new { operatorId });
            }
        }

    }

}
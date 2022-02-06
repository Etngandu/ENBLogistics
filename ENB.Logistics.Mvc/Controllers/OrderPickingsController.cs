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
    public class OrderPickingsController : Controller
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;

        /// <summary>
        /// Initializes a new instance of the OrderPickingsController class.
        /// </summary>
        public OrderPickingsController(IOperatorRepository operatorRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _operatorRepository = operatorRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }

        public ActionResult List(int operatorId)
        {
            ViewBag.OperatorId = operatorId;
            var operatorx = _operatorRepository.FindById(operatorId, x => x.OrderPickings);
            ViewBag.Message = operatorx.FullName;
            var data = new List<DisplayOrderPicking>();
            _imapper.Map(operatorx.OrderPickings, data);
            return View(data);
        }

        public ActionResult Details(int id, int operatorId)
        {
            ViewBag.OperatorId = operatorId;

            var operatorx = _operatorRepository.FindById(operatorId, x => x.OrderPickings);
            ViewBag.Message = operatorx.FullName;
            var data = new DisplayOrderPicking();
            _imapper.Map(operatorx.OrderPickings.Single(x => x.Id == id), data);

            return View(data);
        }

        public ActionResult Create(int OperatorId)
        {
            ViewBag.OperatorId = OperatorId;
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateAndEditOrderPicking createAndEditOrderPicking, int operatorId)
        {
            ViewBag.OperatorId = operatorId;
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var operatorx = _operatorRepository.FindById(operatorId);
                        var orderpicking = new OrderPicking();

                        ViewBag.Message = operatorx.FullName;

                        _imapper.Map(createAndEditOrderPicking, orderpicking);
                        operatorx.OrderPickings.Add(orderpicking);
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

            var operatorx = _operatorRepository.FindById(operatorId, x => x.OrderPickings);
            if (operatorx == null)
            {
                return HttpNotFound();
            }
            var data = new CreateAndEditOrderPicking();
            _imapper.Map(operatorx.OrderPickings.Single(x => x.Id == id), data);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(CreateAndEditOrderPicking createAndEditOrderPicking, int operatorId)
        {
            ViewBag.OperatorId = operatorId;
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var operatorx = _operatorRepository.FindById(operatorId, x => x.OrderPickings);
                        var orderpicking = operatorx.OrderPickings.Single(x => x.Id == createAndEditOrderPicking.Id);
                        _imapper.Map(createAndEditOrderPicking, orderpicking);
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

            var operatorx = _operatorRepository.FindById(operatorId, x => x.OrderPickings);
            var data = new DisplayOrderPicking();
            _imapper.Map(operatorx.OrderPickings.Single(x => x.Id == id), data);
            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(DisplayOrderPicking displayOrderPicking, int operatorId)
        {
            ViewBag.OperatorId = operatorId;

            using (_unitOfWorkFactory.Create())
            {
                var operatorx = _operatorRepository.FindById(operatorId, x => x.OrderPickings);
                var orderpicking = operatorx.OrderPickings.Single(x => x.Id == displayOrderPicking.Id);
                operatorx.OrderPickings.Remove(orderpicking);
                return RedirectToAction("List", new { operatorId });
            }
        }

    }
}
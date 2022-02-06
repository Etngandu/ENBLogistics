using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using ENB.Test.Logistics.Infrastructure;
using ENB.Test.Logistics.Entities;
using ENB.Logistics.WebAPI.Models;
using ENB.Test.Logistics.Repositories.EF;
using ENB.Test.Logistics.Entities.Repositories;
using System.Web.Http.Cors;

namespace ENB.Logistics.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrderPickingApiController : ApiController
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;

        /// <summary>
        /// Initializes a new instance of the OrderPickingsController class.
        /// </summary>
        public OrderPickingApiController(IOperatorRepository operatorRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _operatorRepository = operatorRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }


        [Route("api/operator/{operatorId:int}/pickings")]
        [HttpGet]
        public IHttpActionResult List(int operatorId)
        {
          
            var operatorx = _operatorRepository.FindById(operatorId, x => x.OrderPickings);
          
            var data = new List<DisplayOrderPicking>();
            _imapper.Map(operatorx.OrderPickings, data);
            return Ok(data);
        }

        [Route("api/orderpicking/{id:int}/operator/{operatorId:int}")]
        [HttpGet]
        public IHttpActionResult Details(int id, int operatorId)
        {
            //ViewBag.OperatorId = operatorId;

            var operatorx = _operatorRepository.FindById(operatorId, x => x.OrderPickings);
           // ViewBag.Message = operatorx.FullName;
            var data = new DisplayOrderPicking();
            _imapper.Map(operatorx.OrderPickings.Single(x => x.Id == id), data);

            return Ok(data);
        }

        [Route("api/operator/{operatorId:int}/create")]
        [HttpGet]
        public IHttpActionResult Create(int OperatorId)
        {
           // ViewBag.OperatorId = OperatorId;
            return Ok();
        }

        [Route("api/operator/{operatorId:int}/createPicking")]
        [HttpPost]
        public IHttpActionResult Create(CreateAndEditOrderPicking createAndEditOrderPicking, int operatorId)
        {
          //  ViewBag.OperatorId = operatorId;
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var operatorx = _operatorRepository.FindById(operatorId);
                        var orderpicking = new OrderPicking();

                      //  ViewBag.Message = operatorx.FullName;

                        _imapper.Map(createAndEditOrderPicking, orderpicking);
                        operatorx.OrderPickings.Add(orderpicking);
                    //    return RedirectToAction("List", new { operatorId });
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
            return Ok("Create confirmed!");
        }

        [Route("api/editorderpickings/{id:int}/operators/{operatorId:int}")]
        [HttpGet]
        public IHttpActionResult Edit(int id, int operatorId)
        {
           // ViewBag.OperatorId = operatorId;

            var operatorx = _operatorRepository.FindById(operatorId, x => x.OrderPickings);
            if (operatorx == null)
            {
                return NotFound();
            }
            var data = new CreateAndEditOrderPicking();
            _imapper.Map(operatorx.OrderPickings.Single(x => x.Id == id), data);
            return Ok(data);
        }

        [Route("api/operator/EditPicking/{operatorId:int}")]
        [HttpPut]
        public IHttpActionResult Edit(CreateAndEditOrderPicking createAndEditOrderPicking, int operatorId)
        {
           // ViewBag.OperatorId = operatorId;
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var operatorx = _operatorRepository.FindById(operatorId, x => x.OrderPickings);
                        var orderpicking = operatorx.OrderPickings.Single(x => x.Id == createAndEditOrderPicking.Id);
                        _imapper.Map(createAndEditOrderPicking, orderpicking);
                    //    return RedirectToAction("List", new { operatorId });
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
            return Ok("");
        }


        [Route("api/orderpickings/{id:int}/operators/{operatorId:int}")]
        [HttpGet]
        public IHttpActionResult Delete(int id, int operatorId)
        {
           // ViewBag.OperatorId = operatorId;

            var operatorx = _operatorRepository.FindById(operatorId, x => x.OrderPickings);
            var data = new DisplayOrderPicking();
            _imapper.Map(operatorx.OrderPickings.Single(x => x.Id == id), data);
            return Ok(data);
        }


       // [HttpPost, ActionName("Delete")]
        [Route("api/orderpicking/deleteconfirm/operator/{operatorId:int}")]
        [HttpPost]
        public IHttpActionResult Delete(DisplayOrderPicking displayOrderPicking, int operatorId)
        {
           // ViewBag.OperatorId = operatorId;

            using (_unitOfWorkFactory.Create())
            {
                var operatorx = _operatorRepository.FindById(operatorId, x => x.OrderPickings);
                var orderpicking = operatorx.OrderPickings.Single(x => x.Id == displayOrderPicking.Id);
                operatorx.OrderPickings.Remove(orderpicking);
               // return RedirectToAction("List", new { operatorId });
            }
            return Ok("Delete confirmed");
        }
    }
}
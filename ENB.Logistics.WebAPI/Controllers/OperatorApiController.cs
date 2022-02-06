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
    public class OperatorApiController : ApiController
    {

        // GET: Operator

        private readonly IOperatorRepository _operatorRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;
        const int PageSize = 10;

        

        /// <summary>
        /// Initializes a new instance of the OperatorController class.
        /// </summary>
        public OperatorApiController(IOperatorRepository operatorRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _operatorRepository = operatorRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }

        [Route("api/Getalloperator")]
        [HttpGet]
        public IHttpActionResult GetAllOperators()
        {         
           

            IQueryable<Operator> allOperator = _operatorRepository.FindAll();


            var data = _imapper.Map<List<DisplayOperator>>(allOperator);

         
            return Ok(data);
        }     


        [Route("api/Getoperator/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetOperator(int id)
        {
            Operator dbOperator = _operatorRepository.FindById(id);

           // ViewBag.Message = dbOperator.FullName;

            if (dbOperator == null)
            {
                return NotFound();
            }

            var data = _imapper.Map<DisplayOperator>(dbOperator);

            return Ok(data);
        }


        [Route("api/operator/create")]
        [HttpGet]
        public IHttpActionResult Create()
        {
            return Ok();
        }


        [Route("api/operator/createnew")]
        [HttpPost]       
        public IHttpActionResult Create(CreateAndEditOperator createAndEditOperator)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        Operator dbOperator = new Operator();

                        _imapper.Map(createAndEditOperator, dbOperator);
                        _operatorRepository.Add(dbOperator);                        
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage);
                    }
                }

                return Ok<String>("Operator Added");
            }
            else
            {
                return Ok<String>("Operator Not Added");
            }
        }

        [Route("api/operator/showedit/{id:int}")]
        [HttpGet]
        public IHttpActionResult Edit(int id)
        {
            Operator dbOperator = _operatorRepository.FindById(id);
            if (dbOperator == null)
            {
                return NotFound();
            }
            var data = _imapper.Map<CreateAndEditOperator>(dbOperator);

            return Ok(new { result = data });
        }

        
        [Route("api/operator/edit")]
        [HttpPut]
        public IHttpActionResult Edit(CreateAndEditOperator createAndEditOperator)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        Operator dbOperatorToUpdate = _operatorRepository.FindById(createAndEditOperator.Id);                       
                        _imapper.Map(createAndEditOperator, dbOperatorToUpdate, typeof(CreateAndEditOperator), typeof(Operator));
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
            return Ok<String>("Modified succeed API");
        }

        [Route("api/operator/delete/{id:int}")]
        [HttpGet]
        public IHttpActionResult Delete(int id)
        {
            Operator dbOperator = _operatorRepository.FindById(id);
            if (dbOperator == null)
            {
                return NotFound();
            }
            var data = _imapper.Map<DisplayOperator>(dbOperator);
           
           
            return Ok( data );
        }

        
        [Route("api/operator/deleteconfirm/{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteConfirmed(int id)
        {
            using (_unitOfWorkFactory.Create())
            {
                _operatorRepository.Remove(id);
            }
            return Ok<String>("Record deleted API");
        }
    }
}

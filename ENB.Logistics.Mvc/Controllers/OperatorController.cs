using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
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
    public class OperatorController : Controller
    {
    
        // GET: Operator

        private readonly IOperatorRepository _operatorRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;
        const int PageSize = 10;

        //public OperatorController()
        //{ }

        /// <summary>
        /// Initializes a new instance of the OperatorController class.
        /// </summary>
        public OperatorController(IOperatorRepository operatorRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _operatorRepository = operatorRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }
        public ActionResult Index(int page = 1, string sort = "Id", string sortDir = "ASC", string search="")
        {
            int totalRecords = _operatorRepository.FindAll().Count();

            ViewBag.search = search;
           

            IQueryable<Operator> allOperator = _operatorRepository.FindAll().Where(o => o.FirstName.Contains(search) 
                                               || o.LastName.Contains(search)).OrderBy(BuildOrderBy(sort, sortDir)).Skip((page * PageSize) - PageSize).Take(PageSize);


           

            var data = _imapper.Map<List<DisplayOperator>>(allOperator);

            var model = new PagerModel<DisplayOperator> { Data = data, PageNumber = page, PageSize = PageSize, TotalRows = totalRecords };
            return View(model);
        }

        private string BuildOrderBy(string sortOn, string sortDirection)
        {
            if (sortOn.ToLower() == "fullname")
            {
                return String.Format("FirstName {0}, LastName {0}", sortDirection);
            }
            return string.Format("{0} {1}", sortOn, sortDirection);
        }

        public ActionResult Details(int id)
        {
            Operator dbOperator = _operatorRepository.FindById(id);

            ViewBag.Message = dbOperator.FullName;
            
            if (dbOperator == null)
            {
                return HttpNotFound();
            }

            var data = _imapper.Map<DisplayOperator>(dbOperator);

            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAndEditOperator createAndEditOperator)
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
                        return RedirectToAction("Index");
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

        public ActionResult Edit(int id)
        {
            Operator dbOperator = _operatorRepository.FindById(id);
            if (dbOperator == null)
            {
                return HttpNotFound();
            }
            var data = _imapper.Map<CreateAndEditOperator>(dbOperator);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateAndEditOperator createAndEditOperator)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        Operator dbOperatorToUpdate = _operatorRepository.FindById(createAndEditOperator.Id);                       
                        _imapper.Map(createAndEditOperator, dbOperatorToUpdate, typeof(CreateAndEditOperator), typeof(Operator));
                        return RedirectToAction("Index");
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

        public ActionResult Delete(int id)
        {
            Operator dbOperator = _operatorRepository.FindById(id);
            if (dbOperator == null)
            {
                return HttpNotFound();
            }
            var data = _imapper.Map<DisplayOperator>(dbOperator);
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (_unitOfWorkFactory.Create())
            {
                _operatorRepository.Remove(id);
            }
            return RedirectToAction("Index");
        }
    }

    }


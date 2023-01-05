using CalculatorTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CalculatorTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (ViewBag.Calculations != null) //if there is any calculation 
            {
                return View(ViewBag.Calculations);
            }
            else
            {
                GetLastFiveCalculations();
                return View(ViewBag.Calculations);
            }
        }
        /// <summary>
        /// Delete Selected Calculation from Database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public JsonResult DeleteCalculation([FromBody] int Id)
        {
            List<CalculationModel> calculationsList = new List<CalculationModel>();
            using (var context = new CalculatorDatabaseContext())
            {
                var RecordToDelete = context.TblCalculations.Where(d => d.Id == Convert.ToInt32(Id)).First();
                context.TblCalculations.Remove(RecordToDelete);
                context.SaveChanges();
                var items = context.TblCalculations.OrderByDescending(u => u.CreatedDate).Take(5);
                var calculations = items.ToList();
                foreach (var calculation in calculations)
                {
                    calculationsList.Add(new CalculationModel()
                    {
                        Id = calculation.Id,
                        Input = calculation.Input,
                        FinalResult = calculation.FinalResult
                    });
                }
                ViewBag.Calculations = calculationsList;
            }
            return Json(true);
        }
        /// <summary>
        /// Get Last 5 Calculations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetLastFiveCalculations()
        {
            List<CalculationModel> calculationsList = new List<CalculationModel>();
            CalculationModel calculationdetails = new CalculationModel();
            using (var context = new CalculatorDatabaseContext())
            {
                var items = context.TblCalculations.OrderByDescending(u => u.CreatedDate).Take(5);
                var calculations = items.ToList();
                if (calculations.Count > 0)
                {
                    foreach (var calculation in calculations)
                    {
                        calculationsList.Add(new CalculationModel()
                        {
                            Id = calculation.Id,
                            Input = calculation.Input,
                            FinalResult = calculation.FinalResult
                        });
                    }
                }
                ViewBag.Calculations = calculationsList;
            }
            return Json(calculationsList);
        }
        /// <summary>
        /// To save current Calculation to the database
        /// </summary>
        /// <param name="calculationModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveCurrentCalculation([FromBody] CalculationModel calculationModel)
        {
            using (var context = new CalculatorDatabaseContext())
            {
                var calculation = new TblCalculation()
                {
                    Input = calculationModel.Input.ToString(),
                    FinalResult = calculationModel.FinalResult.ToString(),
                    CreatedDate = DateTime.Now,
                };
                context.TblCalculations.Add(calculation);
                context.SaveChanges();
            }

            return Json(true);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// To delete Last 5 Calculations at once
        /// </summary>
        /// <returns></returns>
        public JsonResult DeleteLastFiveCalculations()
        {
            List<CalculationModel> calculationsList = new List<CalculationModel>();
            using (var context = new CalculatorDatabaseContext())
            {
                var items = context.TblCalculations.OrderByDescending(u => u.CreatedDate).Take(5);
                foreach (var item in items)
                {
                    context.TblCalculations.Remove(item);
                }
                context.SaveChanges();
                var calculations = items.ToList();
                if (calculations.Count > 0)
                {
                    foreach (var calculation in calculations)
                    {
                        calculationsList.Add(new CalculationModel()
                        {
                            Id = calculation.Id,
                            Input = calculation.Input,
                            FinalResult = calculation.FinalResult
                        });
                    }
                }
                ViewBag.Calculations = calculationsList;
            }
            return Json(true);
        }

    }
}
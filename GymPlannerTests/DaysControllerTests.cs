using GymPlanner.Controllers;
using GymPlanner.Data;
using GymPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace GymPlannerTests
{
    [TestClass]
    public class DaysControllerTests
    {
        //Shared db variable 
        private ApplicationDbContext context;
        DaysController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                          .UseInMemoryDatabase(Guid.NewGuid().ToString())
                          .Options;
            context = new ApplicationDbContext(options);

            var workout = new Workout { WorkoutId = 123, Name = "Some Workout", Reps = 12, Sets = 2, Weight = "135" };
            context.Add(workout);

            for (var i = 100; i < 150; i++)
            {
                var day = new Day { DayId = i, Name = "Day" + i.ToString() };
                context.Add(day);
            }
            context.SaveChanges();


            controller = new DaysController(context);
        }

        #region "Index
        [TestMethod]
        public void IndexLoadsView()
        {
            //Test Initialize method runs 

            //act 
            var result = (ViewResult)controller.Index().Result;

            //assert
            Assert.AreEqual("404", result.ViewName);

        }

        [TestMethod]
        public void IndexLoadsDays()
        {
            //act 
            var result = (ViewResult)controller.Index().Result;
            List<Day> model = (List<Day>)result.Model;

            //assert
            CollectionAssert.AreEqual(context.Day.ToList(), model);
        }

        [TestMethod]
        public void DaysDetailsNoId()
        {
            //act
            var result = (ViewResult)controller.Details(null).Result;

            //assert
            Assert.AreEqual("404", result.ViewName);
        }
        #endregion

        #region "Details"
        [TestMethod]
        public void DetailsNoDaysTableLoads()
        {
            //arrange
            context.Day = null;

            //act
            var result = (ViewResult)controller.Details(null).Result;

            //assert
            Assert.AreEqual("404", result.ViewName);
        }
        [TestMethod]
        public void DaysDetailsInvalidId()
        {
            // act
            var result = (ViewResult)controller.Details(23).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdLoads()
        {
            //act
            var result = (ViewResult)controller.Details(115).Result;

            //assert
            Assert.AreEqual("Details", result.ViewName);
        }
        [TestMethod]
        public void DetailsValidIdLoadsDays()
        {
            //act
            var result = (ViewResult)controller.Details(115).Result;

            //assert
            Assert.AreEqual(context.Day.Find(115), result.Model);
        }
        #endregion

        #region "Create"
        [TestMethod]
        public void CreateValiddayIdLoads()
        {
            //arrange
            var day = new Day { DayId = 122, Name = "Test", Description = "test" };

            //act
            controller.ModelState.AddModelError("", "UnValid");
            var result = (ViewResult)controller.Create(day).Result;

            //assert
            Assert.AreEqual(day, result.Model);
        }





        #endregion

        #region "Edit"
        [TestMethod]
        public void EditNoIdLoads404()
        {
           
            // act
            var result = (NotFoundObjectResult)controller.Edit(null).Result;

            // assert 
            Assert.AreEqual("404", result.Value);
        }

        [TestMethod]
        public void EditNoProductsTableLoads404()
        {
            // arrange
            context.Day = null;

            // act
            var result = (NotFoundObjectResult)controller.Edit(null).Result;

            // assert 
            Assert.AreEqual("404", result.Value);
        }

        [TestMethod]
        public void EditInvalidIdLoads404()
        {
            // act
            var result = (NotFoundObjectResult)controller.Edit(12).Result;

            // assert 
            Assert.AreEqual("404", result.Value);
        }

        [TestMethod]
        public void EditValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Edit(102).Result;

            // assert 
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditValidIdLoadsProduct()
        {
            // act
            var result = (ViewResult)controller.Edit(102).Result;

            // assert 
            Assert.AreEqual(context.Day.Find(102), result.Model);
        }
        

        #endregion

        #region "Delete"
        [TestMethod]
        public void DeleteNoDaysTableLoads()
        {
            //arrange
            context.Day = null;

            //act
            var result = (NotFoundObjectResult)controller.Delete(null).Result;

            //assert
            Assert.AreEqual("404", result.Value);
        }
        [TestMethod]
        public void DaysDeleteInvalidId()
        {
            // act
            var result = (NotFoundObjectResult)controller.Delete(23).Result;

            // assert 
            Assert.AreEqual("404", result.Value);
        }

        [TestMethod]
        public void DeleteValidIdLoads()
        {
            //act
            var result = (ViewResult)controller.Delete(115).Result;

            //assert
            Assert.AreEqual("Delete", result.ViewName);
        }
        [TestMethod]
        public void DeleteValidIdLoadsDays()
        {
            //act
            var result = (ViewResult)controller.Delete(115).Result;

            //assert
            Assert.AreEqual(context.Day.Find(115), result.Model);
        }

        [TestMethod]
        public void DeleteValiddayIdLoads()
        {
            //arrange
            var day = new Day { DayId = 44, Name = "Test", Description = "test" };

            //act
            controller.ModelState.AddModelError("", "UnValid");
            var result = (ViewResult)controller.Create(day).Result;

            //assert
            Assert.AreEqual(day, result.Model);
        }
        #endregion

        #region "DeleteConfirmed"
        [TestMethod]
        public void DeleteConfirmedNoIdLoads404()
        {

            // act
            var result = (RedirectToActionResult)controller.DeleteConfirmed(1).Result;

            // assert 
            Assert.AreEqual("404", result.RouteValues);
        }

        [TestMethod]
        public void DeleteConfirmedNoProductsTableLoads404()
        {
            // arrange
            context.Day = null;

            // act
            var result = (NotFoundObjectResult)controller.DeleteConfirmed(12).Result;

            // assert 
            Assert.AreEqual("404", result.ContentTypes);
        }

        [TestMethod]
        public void DeleteConfirmedInvalidIdLoads404()
        {
            // act
            var result = (RedirectToActionResult)controller.DeleteConfirmed(85).Result;

            // assert 
            Assert.AreEqual("404", result.RouteValues);
        }

        [TestMethod]
        public void DeleteConfirmedValidIdLoadsView()
        {
            // act
            var result = (RedirectToActionResult)controller.DeleteConfirmed(109).Result;

            // assert 
            Assert.AreEqual("DeleteConfirmed", result.RouteValues);
        }

        [TestMethod]
        public void DeleteConfirmedValidIdLoadsProduct()
        {
            // act
            var result = (RedirectToActionResult)controller.DeleteConfirmed(109).Result;

            // assert 
            Assert.AreEqual(context.Day.Find(109), result.RouteValues);
        }
        #endregion

    }
}
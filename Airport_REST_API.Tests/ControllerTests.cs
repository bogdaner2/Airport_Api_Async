using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Airport_API_Async.Controllers;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;


namespace Airport_REST_API.Tests
{
    [TestFixture]
    public class ControllerTests
    {
        [Test]
        public void ControllerReturnOkStatus_When_ServiceReturnTrue()
        {
            //Arrange
            var mock = new Mock<ITicketService>();
            mock.Setup(service => service.CreateObjectAsync(It.IsAny<TicketDTO>())).Returns(Task.FromResult(true));
            var controller = new TicketController(mock.Object);
            var ticket = new TicketDTO();
            //Act
            var result = controller.Post(ticket).Result as StatusCodeResult;
            //Assert
            Assert.AreEqual(new OkResult().StatusCode,result.StatusCode);
        }
        [Test]
        public void Controller_StatusCode500_When_ServiceReturnFalse()
        {
            //Arrange
            var mock = new Mock<ITicketService>();
            mock.Setup(service => service.DeleteObjectAsync(It.IsAny<int>())).Returns(Task.FromResult(false));
            var controller = new TicketController(mock.Object);
            //Act
            var result = controller.Delete(It.IsAny<int>()).Result as StatusCodeResult;
            //Assert
            Assert.AreEqual(new StatusCodeResult(500).StatusCode, result.StatusCode);
        }
        [Test]
        public void ValidateModel_ReturnFalse_When_ModelIsntValid()
        {
            //Arrange
            var ticket = new TicketDTO {Id = 5, Number = "G", Price = 500000};
            var context = new ValidationContext(ticket);
            var result = new List<ValidationResult>();
            //Act
            var isValid = Validator.TryValidateObject(ticket, context, result, true);
            //Assert
            Assert.IsFalse(isValid);
        }
        [Test]
        public void ControllerReturnOkStatus_When_PostValidModel()
        {
            //Arrange
            var mock = new Mock<ITicketService>();
            mock.Setup(service => service.CreateObjectAsync(It.IsAny<TicketDTO>())).Returns(Task.FromResult(true));
            var controller = new TicketController(mock.Object);
            var ticket = new TicketDTO { Id = 5, Number = "GRB300", Price = 500 };
            var context = new ValidationContext(ticket);
            var resultValid = new List<ValidationResult>();
            //Act
            var isValid = Validator.TryValidateObject(ticket, context, resultValid, true);
            var result = controller.Post(ticket).Result as StatusCodeResult;
            //Assert
            Assert.True(isValid);
            Assert.AreEqual(new OkResult().StatusCode, result.StatusCode);
        }
        [Test]
        public void Get_CollectionInOkResult()
        {
            var mock = new Mock<ITicketService>();
            mock.Setup(service => service.GetCollectionAsync()).Returns(Task.FromResult(new List<TicketDTO>
            {
                new TicketDTO { Id = 1,Number = "HRBE4",Price = 1000},
                new TicketDTO { Id = 2,Number = "HRBE5",Price = 1500},
                new TicketDTO { Id = 3,Number = "HRBE6",Price = 1800},
            }.AsEnumerable()) );
            var controller = new TicketController(mock.Object);
            var result = controller.GetAll().Result as OkObjectResult;
            Assert.IsTrue(result.StatusCode == 200 && ((List<TicketDTO>)result.Value).Count == 3);
        }
    }
}

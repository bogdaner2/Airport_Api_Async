﻿
using System.Collections.Generic;
using System.Linq;
using Airport_API_Async.Controllers;
using Airport_REST_API.DataAccess;
using Airport_REST_API.DataAccess.Models;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Services.Service;
using Airport_REST_API.Shared.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Airport_REST_API.Tests
{
    [TestFixture]
    public class IntegrationTest
    {
        private TicketController _ticketController;
        private ITicketService _ticketService;
        private IUnitOfWork _uow;
        private AirportContext _context;
        private IMapper _mapper;
        private ICrewService _crewService;
        private CrewController _crewController;
        [SetUp]
        public void StartUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AirportContext>();
            optionsBuilder.UseSqlServer(@"Server = (localdb)\mssqllocaldb; Database = AirportDB; Trusted_Connection = True; ConnectRetryCount = 0");
            _context = new AirportContext(optionsBuilder.Options);
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TicketDTO, Ticket>()
                .ForMember(x => x.Id,opt => opt.Ignore());
                cfg.CreateMap<Ticket, TicketDTO>();
                cfg.CreateMap<CrewDTO, Crew>()
                    .ForMember(i => i.Id, opt => opt.Ignore())
                    .ForMember(i => i.Stewardesses, opt => opt.Ignore())
                    .ForMember(i => i.Pilot, opt => opt.Ignore());
                cfg.CreateMap<Crew, CrewDTO>()
                    .ForMember(i => i.StewardessesId, opt => opt.MapFrom(m => m.Stewardesses.Select(s => s.Id)))
                    .ForMember(i => i.PilotId, opt => opt.MapFrom(m => m.Pilot.Id));
                cfg.CreateMap<Ticket, TicketDTO>();
            }).CreateMapper();
            _uow = new UnitOfWork(_context);
            _ticketService = new TicketService(_uow,_mapper);
            _crewService = new CrewService(_uow, _mapper);
            _crewController = new CrewController(_crewService);
            _ticketController = new TicketController(_ticketService);
        }
        [Test]
        public void Get_ReturnOkStatusCode()
        {
            //Act
            var result = _ticketController.GetAll().Result as OkObjectResult;
            //Assert
            Assert.True(result.StatusCode == 200);
        }      
        [Test]
        public void Get_Should_ReturnObject_When_IdIsCorrect()
        {
            //Act
            var result = _ticketController.Get(2).Result as OkObjectResult;
            //Assert
            Assert.True(result.Value != null);
        }
        [Test]
        public void Get_Should_ReturnObject_When_IdIsNegative()
        {
            //Act
            var result = _ticketController.Get(-1).Result as OkObjectResult;
            //Assert
            Assert.True(result.Value == null);
        }
        [Test]
        public void AddItem_When_InputCorrect_Than_CountIncrease()
        {
            //Arrange
            var correctItem = new TicketDTO {Number = "TestAdd2", Price = 1000};
            var initialCount = _context.Tickets.ToList().Count;
            //Act
            _ticketController.Post(correctItem).Wait();
            var afterCount = _context.Tickets.ToList().Count;
            //Assert
            Assert.IsFalse(initialCount == afterCount);
            //Reset
            _ticketController.Delete(_context.Tickets.Last().Id).Wait();
        }
        [Test]
        public void AddItem_ReturnOKStatus_When_ItemAdded()
        {
            //Arrange
            var correctItem = new TicketDTO { Number = "TestAdd1", Price = 1500 };
            //Act
            var result = _ticketController.Post(correctItem).Result as StatusCodeResult;
            //Assert
            Assert.AreEqual(result.StatusCode,200);
            //Reset
            _ticketController.Delete(_context.Tickets.Last().Id);
        }
        [Test]
        public void CheckIgnoreID_When_CreateNewObjectFromDTO()
        {
            //Arrange
            var correctItem = new TicketDTO { Id = 15, Number = "HRB100", Price = 1000 };
            //Act
            var mapper = _mapper.Map<Ticket>(correctItem);
            //Assert
            Assert.IsFalse(correctItem.Id == mapper.Id);
        }
        [Test]
        public void Post_Return500Status_WhenModelIsInvalid()
        {
            var result = _ticketController.Post(new TicketDTO()).Result as StatusCodeResult;
            //Assert
            Assert.True(result.StatusCode == 500);
        }
        [Test]
        public void Remove_Return_OkStatusCode()
        {
            _ticketController.Post(new TicketDTO { Number = "RemoveTest", Price = 100 }).Wait();
            var lastIndex = _context.Tickets.Last().Id;
            //Act 
            var result = _ticketController.Delete(lastIndex).Result as StatusCodeResult;
            //Assert
            Assert.True(result.StatusCode == 200);
            //Reset
            _ticketController.Delete(lastIndex);
        }
        [Test]
        public void Remove_DecreaseCountOfSet()
        {
            //Arrange
            _ticketController.Post(new TicketDTO {Number = "RemoveTest", Price = 100}).Wait();
            var initialCount = _context.Tickets.ToList().Count;
            var lastIndex = _context.Tickets.Last().Id;
            //Act
            _ticketController.Delete(lastIndex).Wait();
            var afterCount = _context.Tickets.ToList().Count;
            //Assert
            Assert.IsFalse(initialCount == afterCount);
        }
        [Test]
        public void Add_Crew_With_NestedListOfStewardess_ReturnOk()
        {
            //Arrange
            var correctItem = new CrewDTO { PilotId = 1,StewardessesId = new List<int> { 1,2}};
            //Act
            var result = _crewController.Post(correctItem).Result as StatusCodeResult;
            //Assert
            Assert.AreEqual(result.StatusCode, 200);
            //Reset
            _crewController.Delete(_context.Crews.Last().Id).Wait();
        }
        [Test]
        public void Post_Return500_When_CrewWithoutStewardesses()
        {
            //Arrange
            var correctItem = new CrewDTO { PilotId = 1 };
            //Act
            var result = _crewController.Put(1,correctItem).Result as StatusCodeResult;
            //Assert
            Assert.AreEqual(result.StatusCode, 500);
        }
        [Test]
        public void Check_That_ServiceFindStewardesses_When_GiveStewardessesId()
        {
            //Arrange
            var item = new CrewDTO { PilotId = 1, StewardessesId = new List<int> { 1, 2 } };
            //Act
            _crewController.Post(item);
            var result = _context.Crews.OrderByDescending(c => c.Id).First().Stewardesses;
            //Assert
            Assert.IsTrue(result[0] == _uow.Stewardess.GetAsync(1).Result && result[1] == _uow.Stewardess.GetAsync(2).Result);
            //Reset
            _crewController.Delete(_context.Crews.Last().Id);
        }
        [Test]
        public void Update_Return_OkStatusCode()
        {
            var ticket = new TicketDTO {Id = 1 ,Number = "Test", Price = 1000};
            var result = _ticketController.Put(3,ticket).Result;
            Assert.AreEqual(new StatusCodeResult(200).StatusCode,((StatusCodeResult)result).StatusCode);
        }
        [Test]
        public void Update_ChangedObject_Should_NotEqualInitialObject()
        {
            //Arrange
            var temp = _uow.Tickets.GetAsync(2).Result;
            var initial = new { temp.Id, temp.Number, temp.Price};
            var ticket = new TicketDTO { Number = "Test", Price = 1000 };
            //Act
            _ticketController.Put(2, ticket).Wait();
            var current = _uow.Tickets.GetAsync(2).Result;
            //Assert
            Assert.IsFalse(current.Number == initial.Number);
            //Reset
            _ticketController.Put(2, new TicketDTO { Number = initial.Number, Price = initial.Price});
        }
        [Test]
        public void ReturnFalse_When_UpdateObjectWithNegativeId()
        {
            //Act
            var result = _ticketService.UpdateObjectAsync(-1, It.IsAny<TicketDTO>()).Result;
            //Assert
            Assert.IsFalse(result);    
        }

        [TearDown]
        public void Reset()
        {
            _ticketController = null;
            _uow = null;
            _mapper = null;
            _ticketService = null;
        }
    }
}

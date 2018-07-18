using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport_REST_API.DataAccess;
using Airport_REST_API.DataAccess.Models;
using Airport_REST_API.DataAccess.Repositories;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Services.Service;
using Airport_REST_API.Shared.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Airport_REST_API.Tests
{
    [TestFixture]
    public class ServiceTests
    {
        private Mock<IRepository<Ticket>> ticketRepository;
        private Mock<IUnitOfWork> mockUoW;
        private Mock<IMapper> mapper;
        private Mock<DbSet<Ticket>> mockSet;
        private ITicketService service;
        

        [SetUp]
        public void Initialize()
        {
            mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<TicketDTO>(It.IsAny<Ticket>())).Returns(new TicketDTO());
            ticketRepository = new Mock<IRepository<Ticket>>();
            mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(m => m.Tickets).Returns(ticketRepository.Object);
            service = new TicketService(mockUoW.Object, mapper.Object);
            var tickets = new List<Ticket>
            {
                new Ticket {Id = 1,Number = "AAABRT",Price = 100},
                new Ticket {Id = 2,Number = "AABBRT",Price = 120}
            }.AsQueryable();
            mockSet = new Mock<DbSet<Ticket>>();
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Provider).Returns(tickets.Provider);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Expression).Returns(tickets.Expression);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.ElementType).Returns(tickets.ElementType);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.GetEnumerator()).Returns(tickets.GetEnumerator());
        }
        [Test]
        public void ReturnSave()
        {
            //Act
            service.CreateObjectAsync(new TicketDTO());
            //Assert
            mockUoW.Verify(x => x.SaveAsync());
        }
        [Test]
        public void Service_Should_ReturnFalse_When_UpdateNoExistingObject()
        {
            var context = new Mock<AirportContext>();
            context.Setup(x => x.Tickets).Returns(mockSet.Object);
            var rep = new TicketRepository(context.Object);
            mockUoW.Setup(x => x.Tickets).Returns(rep);
            var result = service.UpdateObjectAsync(0, new TicketDTO()).Result;
            //Assert
            Assert.True(result == false);
        }
        [Test]
        public void GetMappedCollection_Test()
        {
            //Arrange
            List<Ticket> tickets = new List<Ticket>
            {
                new Ticket {Id = 1,Number = "AAABRT",Price = 100},
                new Ticket {Id = 2,Number = "AABBRT",Price = 120},
                new Ticket {Id = 3,Number = "AAABR2",Price = 180},
            };
            mockUoW.Setup(x => x.Tickets.GetAllAsync()).Returns(Task.FromResult(tickets.AsEnumerable()));
            var serviceWithMapper = new TicketService(mockUoW.Object, new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TicketDTO, Ticket>()
                    .ForMember(x => x.Id, opt => opt.Ignore());
                cfg.CreateMap<Ticket, TicketDTO>();
            }).CreateMapper());
            // Act
            var result = serviceWithMapper.GetCollectionAsync().Result;
            // Assert
            Assert.AreEqual(tickets.Count, result.ToList().Count);
        }
        [Test]
        public void Service_ReturnFalse_When_InputNoExistingObjectId()
        {
            var context = new Mock<AirportContext>();
            context.Setup(x => x.Tickets).Returns(mockSet.Object);
            var rep = new TicketRepository(context.Object);
            mockUoW.Setup(x => x.Tickets).Returns(rep);
            var result = service.DeleteObjectAsync(0).Result;
            //Assert
            Assert.True(result == false);
        }
        [Test]
        public void GetTicketById_WithNegativeId_ShouldReturnEmptyObject()
        {
            // Arrange
            mockUoW.Setup(x => x.Tickets.GetAsync(It.Is<int>(i => i < 0))).Returns(Task.FromResult((Ticket)null));
            // Act
            var result = service.GetObjectAsync(-10).Result;
            // Assert
            Assert.IsTrue(result.Price == 0 && result.Number == null && result.Id == 0);
        }
        [TearDown]
        public void Deinitialize()
        {
            ticketRepository = null;
            mockUoW = null;
            mapper = null;
            service = null;
        }

    }
}

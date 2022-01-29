using Invelop.Project.Client.Controllers;
using Invelop.Project.Client.Models;
using Invelop.Project.Client.Services.Person;
using Invelop.Project.Services.Person;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Invelop.Project.Client.Tests.Controllers.PersonContactsTests
{
    public class PersonContactsGetAllTests
    {
        private IPersonContactsService _mockPersonContactsService;
        private IPersonContactsMapService _mockPersonContactsMapService;

        [SetUp]
        public void Setup()
        {
            _mockPersonContactsService = Mock.Of<IPersonContactsService>();
            _mockPersonContactsMapService = Mock.Of<IPersonContactsMapService>();
        }

        [Test]
        public void Given_GetAllIsCalledAndDataCollectionIsNull_Then_EmptyCollectionShouldBeReturned()
        {
            Mock.Get(_mockPersonContactsService).Setup(m => m.GetAll()).ReturnsAsync(default(List<Core.Models.PersonContacts>));

            var controller = GetPersonContactsController();
            var result = controller.GetAll().GetAwaiter().GetResult() as ObjectResult;
            var resultModel = result?.Value as IEnumerable<PersonContactsViewModel>;

            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(default, resultModel);
            Mock.Get(_mockPersonContactsService).Verify(m => m.GetAll(), Times.Once());
        }

        [Test]
        public void Given_GetAllIsCalledAndDataCollectionHasNoRows_Then_EmptyCollectionShouldBeReturned()
        {
            Mock.Get(_mockPersonContactsService).Setup(m => m.GetAll()).ReturnsAsync(new List<Core.Models.PersonContacts>());

            var controller = GetPersonContactsController();
            var result = controller.GetAll().GetAwaiter().GetResult() as ObjectResult;
            var resultModel = result?.Value as IEnumerable<PersonContactsViewModel>;

            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(default, resultModel);
            Mock.Get(_mockPersonContactsService).Verify(m => m.GetAll(), Times.Once());
        }

        [Test]
        public void Given_GetAllIsCalledAndDataIsAvailable_Then_DataShouldBeReturned()
        {
            var stubObject = new List<Core.Models.PersonContacts>
            {
                new Core.Models.PersonContacts
                {
                    Address = "Address",
                    DateOfBirth = new DateTime(2022, 1,1),
                    Firstname = "Firstname",
                    IBAN= "IBAN",
                    Id = 1,
                    PhoneNumber="Phonenumber",
                    Surname = "Surname"
                },
                new Core.Models.PersonContacts
                {
                    Address = "Address1",
                    DateOfBirth = new DateTime(2021, 1,1),
                    Firstname = "Firstname1",
                    IBAN= "IBAN1",
                    Id = 2,
                    PhoneNumber="Phonenumber2",
                    Surname = "Surname2"
                }
            };

            Mock.Get(_mockPersonContactsService).Setup(m => m.GetAll()).ReturnsAsync(stubObject);
            foreach (var stub in stubObject)
            {
                Mock.Get(_mockPersonContactsMapService).Setup(m => m.MapModelToView(stub))
                    .Returns(new PersonContactsViewModel
                    {
                        Address = stub.Address,
                        DateOfBirth = stub.DateOfBirth,
                        Surname = stub.Surname,
                        PhoneNumber = stub.PhoneNumber,
                        Id = stub.Id,
                        IBAN = stub.IBAN,
                        Firstname = stub.Firstname
                    });
            }

            var controller = GetPersonContactsController();
            var result = controller.GetAll().GetAwaiter().GetResult() as ObjectResult;
            var resultModel = result?.Value as IEnumerable<PersonContactsViewModel>;
            var resultModelList = resultModel.ToArray();

            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(2, resultModelList.Length);

            Assert.AreEqual("Address", resultModelList[0].Address);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2022, 1, 1)), resultModelList[0].DateOfBirth);
            Assert.AreEqual("Firstname", resultModelList[0].Firstname);
            Assert.AreEqual("IBAN", resultModelList[0].IBAN);
            Assert.AreEqual(1, resultModelList[0].Id);
            Assert.AreEqual("Phonenumber", resultModelList[0].PhoneNumber);
            Assert.AreEqual("Surname", resultModelList[0].Surname);

            Assert.AreEqual("Address1", resultModelList[1].Address);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2021, 1, 1)), resultModelList[1].DateOfBirth);
            Assert.AreEqual("Firstname1", resultModelList[1].Firstname);
            Assert.AreEqual("IBAN1", resultModelList[1].IBAN);
            Assert.AreEqual(2, resultModelList[1].Id);
            Assert.AreEqual("Phonenumber2", resultModelList[1].PhoneNumber);
            Assert.AreEqual("Surname2", resultModelList[1].Surname);

            Mock.Get(_mockPersonContactsService).Verify(m => m.GetAll(), Times.Once());
            Mock.Get(_mockPersonContactsMapService).Verify(m => m.MapModelToView(It.IsAny<Core.Models.PersonContacts>()), Times.Exactly(2));
        }

        private PersonContactsController GetPersonContactsController()
        {
            return new PersonContactsController(_mockPersonContactsService, _mockPersonContactsMapService);
        }
    }
}

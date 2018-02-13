using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watermelons.EntityFramework;
using Watermelons.Repository;
using Watermelons.Services;

namespace Watermelons.Tests.Services
{
    [TestClass]
    public class MoqTestCompetitionEntryService
    {
        ICompetitionEntryService _service;
        Mock<ICompetitionEntryRepository> _repo;
        Mock<IUnitOfWork> _unit;

        [TestInitialize]
        public void Initialise()
        {
            _repo = new Mock<ICompetitionEntryRepository>();
            _unit = new Mock< IUnitOfWork >();
            _service = new CompetitionEntryService(_repo.Object, _unit.Object);
        }

        [TestMethod]
        [TestCategory("Moq Test Competition Entry Service")]
        public void LessThan100WordsReturnsFalse()
        {
            string errorMessage;
            var entry = new CompetitionEntry();
            entry.FavouriteWatermelonPlace = "This is a free online calculator which counts the number of words or units in a text";
            var actual = _service.InsertCompetitionEntry(entry, out errorMessage);
            Assert.AreEqual(true, actual);
            Assert.AreEqual("", errorMessage);
        }

        [TestMethod]
        [TestCategory("Moq Test Competition Entry Service")]
        public void GreaterThan100WordsReturnsFalse()
        {
            string errorMessage;
            var entry = new CompetitionEntry();
            entry.FavouriteWatermelonPlace = "This is a free online calculator which counts the number of words or units in a text. Authors writing your book, pupils working on your essay, self-employed word smiths, teachers, translators, professors, or simply curious individuals: please feel free to use this tool to count the number of words in your document. It will also be useful for database, grammatical research, translation breakdowns, dictionary creation, commercial writing, etc.This is a free online calculator which counts the number of words or units in a text. It will also be useful for database, grammatical research, translation breakdowns, dictionary creation, commercial writing, etc. 101";
            var actual = _service.InsertCompetitionEntry(entry, out errorMessage);
            Assert.AreEqual(false, actual);
            Assert.AreEqual("Too many words entered", errorMessage);
        }

        [TestMethod]
        [TestCategory("Moq Test Competition Entry Service")]
        public void OneHundredWordsReturnsTrue()
        {
            string errorMessage;
            var entry = new CompetitionEntry();
            entry.FavouriteWatermelonPlace = "This is a free online calculator which counts the number of words or units in a text. Authors writing your book, pupils working on your essay, self-employed word smiths, teachers, translators, professors, or simply curious individuals: please feel free to use this tool to count the number of words in your document. It will also be useful for database, grammatical research, translation breakdowns, dictionary creation, commercial writing, etc.This is a free online calculator which counts the number of words or units in a text. It will also be useful for database, grammatical research, translation breakdowns, dictionary creation, commercial writing, etc.";
            var actual = _service.InsertCompetitionEntry(entry, out errorMessage);
            Assert.AreEqual(true, actual);
            Assert.AreEqual("", errorMessage);
        }

        [TestMethod]
        [TestCategory("Moq Test Competition Entry Service")]
        public void ExceptionReturnsFalse()
        {
            string errorMessage;
            var entry = new CompetitionEntry();

            _repo.Setup(item => item.Insert(It.IsAny<CompetitionEntry>()))
              .Throws(new Exception("Exception Thrown"));

            var actual = _service.InsertCompetitionEntry(entry, out errorMessage);
            Assert.AreEqual(false, actual);
            Assert.AreEqual("Exception Thrown", errorMessage);
            _repo.Verify(foo => foo.Insert(It.IsAny<CompetitionEntry>()), Times.Once());
        }


        [TestMethod]
        [TestCategory("Moq Test Competition Entry Service")]
        public void ExistingEmailReturnsTrue()
        {
            IList<CompetitionEntry> fakeData = new List<CompetitionEntry>();
            var entry = new CompetitionEntry();
            entry.Email = "bob@smith.com";
            fakeData.Add(entry);
            _repo.Setup(item => item.GetAll()).Returns(fakeData);
            
            var actual = _service.EntryAlreadyExists("bob@smith.com");
            Assert.AreEqual(true, actual);
            _repo.Verify(foo => foo.GetAll(), Times.Once());
        }

        [TestMethod]
        [TestCategory("Moq Test Competition Entry Service")]
        public void NonExistingEmailReturnsFalse()
        {
            IList<CompetitionEntry> fakeData = new List<CompetitionEntry>();
            var entry = new CompetitionEntry();
            entry.Email = "bob@smith.com";
            fakeData.Add(entry);
            _repo.Setup(item => item.GetAll()).Returns(fakeData);

            var actual = _service.EntryAlreadyExists("bobby@smith.com");
            Assert.AreEqual(false, actual);
        }

    }
}

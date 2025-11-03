using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net_Lab6;

namespace laba6_tests
{
    [TestClass]
    public class FileHandlerTest
    {
        private string _testFilePath;

        [TestInitialize]
        public void TestInitialize()
        {
            _testFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".json");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [TestMethod]
        public async Task SaveAsync_Should_WriteDataToFile()
        {
            var students = new List<Student>
            {
                new Student("Hanna Washington", "Lyceum 39", 2000),
                new Student("Beth Washington", "Lyceum 39", 2001)
            };

            var fileSaver = new JsonFileWriter();
            await fileSaver.SaveAsync(_testFilePath, students);

            Assert.IsTrue(File.Exists(_testFilePath));
            string fileContent = await File.ReadAllTextAsync(_testFilePath);
            Assert.IsFalse(string.IsNullOrEmpty(fileContent));
        }

        [TestMethod]
        public async Task LoadAsync_Should_ReadDataFromFile()
        {
            var students = new List<Student>
            {
                new Student("Hanna Washington", "Lyceum 39", 2000)
            };

            var fileSaver = new JsonFileWriter();
            await fileSaver.SaveAsync(_testFilePath, students);

            var fileReader = new JsonFileReader();
            var loadedStudents = await fileReader.LoadAsync<Student>(_testFilePath);

            Assert.IsNotNull(loadedStudents);
            Assert.AreEqual(1, loadedStudents.Count);
            Assert.AreEqual("Hanna Washington", loadedStudents[0].Name);
            Assert.AreEqual("Lyceum 39", loadedStudents[0].School);
        }

        [TestMethod]
        public async Task LoadAsync_WhenFileDoesntExist_ReturnsNull()
        {
            var fileReader = new JsonFileReader();
            var loadedStudents = await fileReader.LoadAsync<Student>(_testFilePath);
            Assert.IsNull(loadedStudents);
        }
    }
}

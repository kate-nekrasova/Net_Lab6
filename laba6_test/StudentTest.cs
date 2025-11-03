using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net_Lab6;
using System;

namespace laba6_tests
{
    [TestClass]
    public class StudentTest
    {
        [TestMethod]
        public void Constructor_WithValidArguments_CreateStudentObject()
        {
            string name = "Kateryna Nekrasova";
            string school = "School #3";
            int year = 2006;

            var student = new Student(name, school, year);

            Assert.AreEqual(name, student.Name);
            Assert.AreEqual(school, student.School);
            Assert.AreEqual(year, student.Year);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Constructor_WithInvalidName_ThrowException(string invalidName)
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new Student(invalidName, "Gymnasium 114", 2000));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Constructor_WithInvalidSchool_ThrowException(string invalidSchool)
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new Student("Maria Spencer", invalidSchool, 2000));
        }

        [TestMethod]
        public void Constructor_WithInvalidYear_ThrowException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new Student("Maria Spencer", "School 15", 1800)); 
            Assert.ThrowsException<ArgumentException>(() =>
                new Student("Maria Spencer", "School 15", DateTime.Now.Year + 1)); 
        }

        [TestMethod]
        public void CompareTo_WithOtherStudent_ReturnsCorrectValue()
        {
            var student1 = new Student("Jane Doe", "Lyceum 39", 1990);
            var student2 = new Student("Mark Steinmann", "College 23", 2004);
            var student3 = new Student("Michael Pol", "School 39", 2004);

            Assert.IsTrue(student1.CompareTo(student2) < 0, "Older student should be less than younger one");
            Assert.IsTrue(student2.CompareTo(student1) > 0, "Younger student should be greater than older one");
            Assert.AreEqual(0, student2.CompareTo(student3), "Students with same year should compare as equal");
        }

        [TestMethod]
        public void CompareTo_WithNull_ReturnsPositiveValue()
        {
            var student = new Student("Diana Spencer", "Academy 15", 1989);
            Assert.IsTrue(student.CompareTo(null) > 0, "Comparing to null should return a positive value.");
        }

        [TestMethod]
        public void ToString_ReturnsCorrectlyFormattedString()
        {
            var student = new Student("Jane Doe", "Lyceum 39", 1990);
            string expected = "??'?: Jane Doe, ?????: Lyceum 39, ã? : 1990 ";
            Assert.AreEqual(expected, student.ToString());
        }
    }
}

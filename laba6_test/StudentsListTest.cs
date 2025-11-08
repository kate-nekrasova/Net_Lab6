using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net_Lab6.interfaces;
using Net_Lab6;
namespace laba6_test
{
    [TestClass]
    public class StudentsListTest
    {
        private IFileSaver _mockFileSaver;
        private IFileLoader _mockFileLoader;

        private Student _student1;
        private Student _student2;
        private Student _student3;
        private Student _student4;
        private StudentsList _students;

        public StudentsListTest()
        {

            _student1 = new Student("Anna", "School 1", 2006);
            _student2 = new Student("Bohdan", "School 2", 2006);
            _student3 = new Student("Olena", "School 3", 2000);
            _student4 = new Student("Petro", "School 4", 2000);

            _students = new StudentsList(_mockFileSaver, _mockFileLoader, "test.json");

            _students.AddStudent(_student1);
            _students.AddStudent(_student2);
            _students.AddStudent(_student3);
            _students.AddStudent(_student4);
        }
        [TestMethod]
        public void AddStudent_ShouldIncreaseCount()
        {
            var newStudent = new Student("Ivan", "School 5", 2008);
            _students.AddStudent(newStudent);
            Assert.AreEqual(5, _students.Students.Count);
        }

        [TestMethod]
        public void DeleteStudent_ShouldDecreaseCount()
        {
            _students.DeleteStudent(_student1);
            Assert.AreEqual(3, _students.Students.Count);
        }

        [TestMethod]
        public void SortByYear_ShouldSortAscending()
        {
            _students.SortByYear();
            var years = _students.Students.Select(s => s.Year).ToList();
            var sorted = years.OrderBy(y => y).ToList();
            CollectionAssert.AreEqual(sorted, years);
        }


        [TestMethod]
        public void FilterBySchool_ShouldReturnCorrectStudents()
        {
            _students.FilterBySchool("School 1");
            Assert.AreEqual(1, _students.Students.Count);
            Assert.AreEqual("School 1", _students.Students[0].School);
        }

        [TestMethod]
        public void ResetFilter_ShouldRestoreFullList()
        {
            _students.FilterBySchool("School 1");
            _students.ResetFilter();
            Assert.AreEqual(4, _students.Students.Count);
        }
    }
}
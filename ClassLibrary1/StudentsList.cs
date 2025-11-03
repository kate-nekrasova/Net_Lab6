using Net_Lab6.interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Net_Lab6
{
    public class StudentsList
    {
        private readonly IFileSaver _fileSaver;
        private readonly IFileLoader _fileLoader;
        private readonly string _filePath;

        public ObservableCollection<Student> Students { get; }
        private List<Student> _allStudents;

        public StudentsList(IFileSaver fileSaver, IFileLoader fileLoader, string filePath)
        {
            _fileSaver = fileSaver;
            _fileLoader = fileLoader;
            _filePath = filePath;

            Students = new ObservableCollection<Student>();
            _allStudents = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
            _allStudents.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            Students.Remove(student);
            _allStudents.Remove(student);
        }

        public void SortByYear()
        {
            var sortedStudents = new List<Student>(Students);
            sortedStudents.Sort(new StudentYearComparer());
            UpdateVisibleCollection(sortedStudents);
        }

        public void FilterBySchool(string school)
        {
            var filtered = _allStudents.Where(s => s.School == school).ToList();
            UpdateVisibleCollection(filtered);
        }

        public void ResetFilter()
        {
            UpdateVisibleCollection(_allStudents);
        }

        private void UpdateVisibleCollection(List<Student> newList)
        {
            Students.Clear();
            foreach (var student in newList)
            {
                Students.Add(student);
            }
        }

        public async Task SaveAsync()
        {
            await _fileSaver.SaveAsync(_filePath, _allStudents);
        }

        public async Task LoadAsync()
        {
            var loadedStudents = await _fileLoader.LoadAsync<Student>(_filePath);
            if (loadedStudents != null)
            {
                _allStudents = loadedStudents;
                UpdateVisibleCollection(_allStudents);
            }
        }
    }
}

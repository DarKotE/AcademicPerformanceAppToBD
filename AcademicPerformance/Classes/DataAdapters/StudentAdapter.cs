using System.Collections.Generic;
using AcademicPerformance.Classes.DataModels;
using AcademicPerformance.Classes.DataSqlGateways;

namespace AcademicPerformance.Classes.DataAdapters
{
    public class StudentAdapter
    {
        public StudentAdapter()
        {
            DataAccess = new SqlDataAccess();
        }

        public SqlDataAccess DataAccess { get; }


        public bool Add(StudentModel newStudent)
        {
            return DataAccess != null && DataAccess.InsertStudent(newStudent);
        }

        public bool Delete(int idUser)
        {
            return DataAccess != null && DataAccess.DeleteStudent(idUser);
        }

        public List<StudentModel> GetAll()
        {
            var studentList = DataAccess.GetStudentList();
            return studentList ?? new List<StudentModel>();
        }

        public StudentModel GetById(int idUser)
        {
            return DataAccess != null ?
                DataAccess.GetStudent(idUser) : new StudentModel();
        }

        public bool Update(StudentModel studentToUpdate)
        {
            return DataAccess != null &&
                   DataAccess.UpdateStudent(studentToUpdate);
        }
    }
}
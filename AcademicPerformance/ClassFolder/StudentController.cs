using System.Collections.Generic;

namespace AcademicPerformance.ClassFolder
{
 
    public class StudentController
    {
        public CDataAccess DataAccess { get; }

        public StudentController()
        {
            DataAccess = new CDataAccess();
        }

        public List<StudentModel> GetAll()
        {
            var studentList = DataAccess.GetStudentList();
            return studentList ?? new List<StudentModel>();
        }

        public bool Add(StudentModel newStudent)
        {
            return DataAccess != null && DataAccess.InsertStudent(newStudent);
        }

        public bool Update(StudentModel studentToUpdate)
        {
            return DataAccess != null && DataAccess.UpdateStudent(studentToUpdate);
        }

        public bool Delete(int idUser)
        {
            return DataAccess != null && DataAccess.DeleteStudent(idUser);
        }

        public StudentModel Select(int idUser)
        {
            return DataAccess != null ? DataAccess.GetStudent(idUser) : new StudentModel();
        }

    }

}

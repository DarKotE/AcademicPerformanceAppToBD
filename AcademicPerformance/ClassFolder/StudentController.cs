using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicPerformance.ClassFolder
{
 
    public class StudentController
    {
        CDataAccess dataAccess = new CDataAccess();

        public StudentController()
        {

        }

        public List<StudentModel> GetAll()
        {
            List<StudentModel> StudentList = new List<StudentModel>();
            StudentList = dataAccess.GetStudentList();
            return StudentList;
        }

        public bool Add(StudentModel newStudent)
        {
            return dataAccess.InsertStudent(newStudent);
        }

        public bool Update(StudentModel StudentToUpdate)
        {
            return dataAccess.UpdateStudent(StudentToUpdate);
        }

        public bool Delete(int idUser)
        {
            return dataAccess.DeleteStudent(idUser);
        }

        public StudentModel Select(int idUser)
        {
            return dataAccess.GetStudent(idUser);
        }

    }

}

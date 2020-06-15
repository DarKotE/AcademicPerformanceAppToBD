using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicPerformance.ClassFolder
{
   public class TeacherController
    {
        CDataAccess dataAccess = new CDataAccess();

        public TeacherController()
        {

        }

        public List<TeacherModel> GetAll()
        {
            List<TeacherModel> teacherList = new List<TeacherModel>();
            teacherList = dataAccess.GetTeacherList();
            return teacherList;
        }
        
        public bool Add(TeacherModel newTeacher)
        {
            return dataAccess.InsertTeacher(newTeacher);
        }

        public bool Update(TeacherModel teacherToUpdate)
        {
            return dataAccess.UpdateTeacher(teacherToUpdate);
        }

        public bool Delete(int idUser)
        {
            return dataAccess.DeleteTeacher(idUser);
        }

        public TeacherModel Select(int idUser)
        {
            return dataAccess.GetTeacher(idUser);
        }

    }


}

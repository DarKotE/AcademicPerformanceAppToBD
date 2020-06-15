using System.Collections.Generic;

namespace AcademicPerformance.ClassFolder
{
   public class TeacherController
    {
        public CDataAccess DataAccess { get; }

        public TeacherController()
        {
            DataAccess = new CDataAccess();
        }

        public List<TeacherModel> GetAll()
        {
            var teacherList = DataAccess.GetTeacherList();
            return teacherList ?? new List<TeacherModel>();
        }
        
        public bool Add(TeacherModel newTeacher)
        {
            return DataAccess != null && DataAccess.InsertTeacher(newTeacher);
        }

        public bool Update(TeacherModel teacherToUpdate)
        {
            return DataAccess != null && DataAccess.UpdateTeacher(teacherToUpdate);
        }

        public bool Delete(int idUser)
        {
            return DataAccess != null && DataAccess.DeleteTeacher(idUser);
        }

        public TeacherModel Select(int idUser)
        {
            if (DataAccess != null) return DataAccess.GetTeacher(idUser);else return new TeacherModel();
        }

    }


}

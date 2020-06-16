using System.Collections.Generic;

namespace AcademicPerformance.ClassFolder
{
    public class TeacherController
    {
        public TeacherController()
        {
            DataAccess = new CDataAccess();
        }

        public CDataAccess DataAccess { get; }

        public bool Add(TeacherModel newTeacher)
        {
            return DataAccess != null && DataAccess.InsertTeacher(newTeacher);
        }

        public bool Delete(int idUser)
        {
            return DataAccess != null && DataAccess.DeleteTeacher(idUser);
        }

        public List<TeacherModel> GetAll()
        {
            var teacherList = DataAccess.GetTeacherList();
            return teacherList ?? new List<TeacherModel>();
        }

        public TeacherModel Select(int idUser)
        {
            if (DataAccess != null) return DataAccess.GetTeacher(idUser);
            return new TeacherModel();
        }

        public bool Update(TeacherModel teacherToUpdate)
        {
            return DataAccess != null && DataAccess.UpdateTeacher(teacherToUpdate);
        }
    }
}
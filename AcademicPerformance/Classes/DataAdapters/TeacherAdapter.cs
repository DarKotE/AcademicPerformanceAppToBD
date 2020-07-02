using System.Collections.Generic;
using AcademicPerformance.Classes.DataModels;
using AcademicPerformance.Classes.DataSqlGateways;

namespace AcademicPerformance.Classes.DataAdapters
{
    public class TeacherAdapter
    {
        public TeacherAdapter()
        {
            DataAccess = new SqlDataAccess();
        }

        public SqlDataAccess DataAccess { get; }

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
            return DataAccess != null ? 
                DataAccess.GetTeacher(idUser) : new TeacherModel();
        }

        public bool Update(TeacherModel teacherToUpdate)
        {
            return DataAccess != null && 
                   DataAccess.UpdateTeacher(teacherToUpdate);
        }
    }
}
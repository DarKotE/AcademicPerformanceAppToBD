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

        public bool AddTeacher(TeacherModel newTeacher)
        {
            return DataAccess != null && DataAccess.InsertTeacher(newTeacher);
        }

        public bool DeleteTeacherById(int idUser)
        {
            return DataAccess != null && DataAccess.DeleteTeacher(idUser);
        }

        public List<TeacherModel> GetAllTeacher()
        {
            var teacherList = DataAccess.GetTeacherList();
            return teacherList ?? new List<TeacherModel>();
        }

        public TeacherModel GetTeacherById(int idUser)
        {
            return DataAccess != null ? 
                DataAccess.GetTeacher(idUser) : new TeacherModel();
        }

        public bool SetTeacher(TeacherModel teacherToUpdate)
        {
            return DataAccess != null && 
                   DataAccess.UpdateTeacher(teacherToUpdate);
        }
    }
}
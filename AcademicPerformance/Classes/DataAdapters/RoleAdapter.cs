using System.Collections.Generic;
using AcademicPerformance.Classes.DataModels;
using AcademicPerformance.Classes.DataSqlGateways;

namespace AcademicPerformance.Classes.DataAdapters
{
    public class RoleAdapter
    {
        public SqlDataAccess DataAccess { get; }

        public RoleAdapter()
        {
            DataAccess = new SqlDataAccess();
        }

        public List<RoleModel> GetAllRole()
        {
            var roleList = DataAccess.GetRoleList();
            return roleList ?? new List<RoleModel>();
        }

        public bool AddRole(RoleModel newRole)
        {
            return DataAccess != null && DataAccess.InsertRole(newRole);
        }

        public bool SetRole(RoleModel roleToUpdate)
        {
            return DataAccess != null && DataAccess.UpdateRole(roleToUpdate);
        }

        public bool DeleteRoleById(int idRole)
        {
            return DataAccess != null && DataAccess.DeleteRole(idRole);
        }

        public RoleModel GetRoleById(int idRole)
        {
            return DataAccess != null 
                ? DataAccess.GetRole(idRole) 
                : new RoleModel();
        }
    }
}
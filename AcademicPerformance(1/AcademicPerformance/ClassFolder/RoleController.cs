using System.Collections.Generic;


namespace AcademicPerformance.ClassFolder
{
    public class RoleController
    {
        public CDataAccess DataAccess { get; }

        public RoleController()
        {
            DataAccess = new CDataAccess();
        }

        public List<RoleModel> GetAll()
        {
            var roleList = DataAccess.GetRoleList();
            return roleList ?? new List<RoleModel>();
        }

        public bool Add(RoleModel newRole)
        {
            return DataAccess != null && DataAccess.InsertRole(newRole);
        }

        public bool Update(RoleModel roleToUpdate)
        {
            return DataAccess != null && DataAccess.UpdateRole(roleToUpdate);
        }

        public bool Delete(int idRole)
        {
            return DataAccess != null && DataAccess.DeleteRole(idRole);
        }

        public RoleModel SelectId(int idRole)
        {
            return DataAccess != null ? DataAccess.GetRole(idRole) : new RoleModel();
        }
    }
}
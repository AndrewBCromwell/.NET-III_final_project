using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IEmployeeAccessor
    {
        int AuthenticateUserWithEmailAndPasswordHash(
            string email, string passwordHash);
        Employee SelectUserByEmail(string email);
        List<string> SelectRolesByEmployeeID(int employeeID);
        List<string> SelectRoles();
        int InsertEmployee(Employee employee);
        int InsertEmployeeRole(int id, string role);
        int DeleteEmployeeRole(int id, string role);
    }
}

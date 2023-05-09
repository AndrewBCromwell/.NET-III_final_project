using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IEmployeeManager
    {
        Employee LoginUser(string email, string password);
        string HashSha256(string source);
        List<string> RetreiveRoles();
        bool FindUser(string email);
        int CreateEmployee(Employee employee);
        bool AddEmployeeRole(int id, string role);
        bool RemoveEmployeeRole(int id, string role);
    }
}

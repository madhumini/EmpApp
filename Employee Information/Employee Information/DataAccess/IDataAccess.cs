using Employee_Information.Models;

namespace Employee_Information.DataAccess
{
    public interface IDataAccess
    {
        IEnumerable<Employee> GetAllEmployees();
        void InsertEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeNo);

        decimal? GetAverageSalary();
    }
}

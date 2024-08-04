using Employee_Information.Models;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;

namespace Employee_Information.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration configuration;
        private readonly string dbconnection;

        public DataAccess(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbconnection = this.configuration["ConnectionStrings:DB"];
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(dbconnection))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employees.Add(new Employee
                    {
                        EmployeeNo = Convert.ToInt32(rdr["EmployeeNo"]),
                        FirstName = rdr["FirstName"].ToString(),
                        LastName = rdr["LastName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(rdr["DateOfBirth"]),
                        Salary = Convert.ToDecimal(rdr["Salary"])
                    });
                }
            }
            return employees;
        }

        public void InsertEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(dbconnection))
            {
                SqlCommand cmd = new SqlCommand("spInsertEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeNo", employee.EmployeeNo);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(dbconnection))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeNo", employee.EmployeeNo);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int employeeNo)
        {
            using (SqlConnection conn = new SqlConnection(dbconnection))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeNo", employeeNo);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public decimal? GetAverageSalary()
        {
            using (SqlConnection conn = new SqlConnection(dbconnection))
            {
                SqlCommand cmd = new SqlCommand("spGetAverageSalary", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result == DBNull.Value || result == null)
                {
                    return null;
                }
                else
                {
                    return (decimal)result;
                }
            }
        }
    }

}

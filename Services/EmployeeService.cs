using Dapper;
using Models;

namespace Services;

public class EmployeeService
{
    private readonly DbContext _db;

    public EmployeeService()
    {
        _db = new DbContext();
    }
    
    public void AddEmployee(string employeeName, int departmentId)
    {
        try
        {
            using var conn = _db.CreateConnection();
            string departmentCheck = "select department_id as Id, department_name as DepartmentName, branch_id as BranchId from departments where department_id = @Id";
            var isDepartment = conn.QueryFirstOrDefault<Department>(departmentCheck, new { Id = departmentId });
            if (isDepartment is not null)
            {
                string sql = "INSERT INTO employees (employee_name, department_id) VALUES (@Name, @DepartmentId) returning employee_id";
                var check = conn.Execute(sql, new { Name = employeeName, DepartmentId = departmentId });
                if (check > 0)
                {
                    Console.WriteLine($"{employeeName} was added successfully");
                }
                else
                {
                    throw new Exception("Some error occured while adding the employee");
                }
            }
            else
            {
                throw new Exception($"Department with id {departmentId} is not found. Can't add the employee there!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<Employee> GetAllEmployees()
    {
        using var conn = _db.CreateConnection();
        return conn.Query<Employee>("select employee_id as Id, employee_name as Fullname, department_id as DepartmentId from employees").ToList();

    }

    public Employee GetEmployeeById(int employeeId)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var sql = "select employee_id as Id, employee_name as Fullname, department_id as DepartmentId from employees where employee_id = @Id";
            var listEmployees = conn.Query<Employee>(sql, new { Id = employeeId }).ToList();
            if (listEmployees.Count > 0)
            {
                return listEmployees.First();
            }
            
            throw new Exception("No Employee with the id " + employeeId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public void UpdateEmployee(int employeeId)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var sql = "select employee_id as Id, employee_name as Fullname, department_id as DepartmentId from employees where employee_id = @Id";
            var listEmployees = conn.Query<Employee>(sql, new { Id = employeeId }).ToList();
            if (listEmployees.Count > 0)
            {
                Console.WriteLine($"Current Employee name: {listEmployees.First().Fullname}");
                Console.Write($"New Employee name: ");
                string newName = Console.ReadLine()!;
                sql = "update employees set employee_name=@Name where employee_id=@Id returning employee_id";
                var check = conn.Execute(sql, new { Id = employeeId, Name = newName });
                if (check == 1)
                {
                    Console.WriteLine("Update Completed!");
                }
                else
                {
                    throw new Exception("Some error occured, no update is made");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void DeleteEmployee(int employeeId)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var sql = "select employee_id as Id, employee_name as Fullname, department_id as DepartmentId from employees where employee_id = @Id";
            var listEmployees = conn.Query<Employee>(sql, new { Id = employeeId }).ToList();
            if (listEmployees.Count > 0)
            {
                Console.WriteLine($"Employee to delete: {listEmployees.First().Fullname}");
                Console.Write($"Confirm delete (y/n): ");
                char confirmation = Console.ReadKey()!.KeyChar;
                if (char.ToLower(confirmation) == 'y')
                {
                    sql = "delete from employees where employee_id=@Id";
                    var check = conn.Execute(sql, new { Id = employeeId});
                    Console.WriteLine();
                    if (check == 1)
                    {
                        Console.WriteLine("Employee Deleted!");
                    }
                    else
                    {
                        throw new Exception("Some error occured, no update is made");

                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Cancelling operation...");
                    Thread.Sleep(1500);
                }
            }
            else
            {
                throw new Exception($"No employee with id {employeeId}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    //TODO: Impelement CRUD operations
}
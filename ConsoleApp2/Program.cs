using Services;

namespace ConsoleApp2;

class Program
{
    static void Main()
    {

        Console.WriteLine("""
                          1. Manage Companies
                          2. Manage Branches
                          3. Manage Departments
                          4. Manage Employees
                          5. Exit the Program
                          """);
        Console.Write("Please choose your option: ");
        int choice = int.Parse(Console.ReadLine()!);

        switch (choice)
        {
            case 1:
                var companyService = new CompanyService();
                Console.WriteLine("""
                                  1. Add Company
                                  2. List Companies
                                  3. Get Company
                                  4. Update Company
                                  5. Delete Company
                                  6. Exit
                                  """);
                Console.Write("Please choose your option: ");
                choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {
                    case 1:
                        companyService.AddCompany("Apple");
                        break;
                    case 2:
                        var companies = companyService.GetAll();
                        companies.ForEach(c => Console.WriteLine($"Id: {c.Id} | Company Name: {c.Name}"));
                        break;
                    case 3:
                        var company = companyService.GetById(5);
                        var companyName = company?.Name;
                        
                        if (companyName is not null)
                        {
                            Console.WriteLine($"You got: {companyName} with id {company!.Id}");
                        }
                        else
                        {
                            Console.WriteLine("Exiting...");
                        }
                        break;
                    case 4:
                        companyService.UpdateCompany(2);
                        break;
                    case 5:
                        companyService.Delete(3);
                        break;
                    case 6:
                        Console.WriteLine("Exiting the program...");
                        Thread.Sleep(1500);
                        break;
                    default:
                        Console.WriteLine("Please choose the available option (1-5)");
                        break;
                }
                break;
            case 2:
                var branchService = new BranchService();
                Console.WriteLine("""
                                  1. Add Branch
                                  2. List Branches
                                  3. Get Branch
                                  4. Update Branch
                                  5. Delete Branch
                                  6. Exit
                                  """);
                Console.Write("Please choose your option: ");
                choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {
                    case 1:
                        branchService.AddBranch("Headquarters", 5);
                        break;
                    case 2:
                        var branches = branchService.GetAllBranches();
                        branches.ForEach(b => Console.WriteLine($"Id: {b.Id} | Branch Name: {b.BranchName}"));
                        break;
                    case 3:
                        var branch = branchService.GetBranchById(1);
                        var branchName = branch?.BranchName;
                        
                        if (branchName is not null)
                        {
                            Console.WriteLine($"You got: {branchName} with id {branch!.Id}");
                        }
                        else
                        {
                            Console.WriteLine("Exiting...");
                        }
                        break;
                    case 4:
                        branchService.UpdateBranch(2);
                        break;
                    case 5:
                        branchService.DeleteBranch(3);
                        break;
                    case 6:
                        Console.WriteLine("Exiting the program...");
                        Thread.Sleep(1500);
                        break;
                    default:
                        Console.WriteLine("Please choose the available option (1-5)");
                        break;
                }
                break;
            case 3:
                var departmentService = new DepartmentService();
                Console.WriteLine("""
                                  1. Add Department
                                  2. List Departments
                                  3. Get Department
                                  4. Update Department
                                  5. Delete Department
                                  6. Exit
                                  """);
                Console.Write("Please choose your option: ");
                choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {
                    case 1:
                        departmentService.AddDepartment("Engineering", 2);
                        break;
                    case 2:
                        var departments = departmentService.GetAllDepartments();
                        departments.ForEach(d => Console.WriteLine($"Id: {d.Id} | Department Name: {d.DepartmentName}"));
                        break;
                    case 3:
                        var department = departmentService.GetDepartmentById(1);
                        var departmentName = department?.DepartmentName;
                        
                        if (departmentName is not null)
                        {
                            Console.WriteLine($"You got: {departmentName} with id {department!.Id}");
                        }
                        else
                        {
                            Console.WriteLine("Exiting...");
                        }
                        break;
                    case 4:
                        departmentService.UpdateDepartment(2);
                        break;
                    case 5:
                        departmentService.DeleteDepartment(3);
                        break;
                    case 6:
                        Console.WriteLine("Exiting to main menu...");
                        Thread.Sleep(1500);
                        break;
                    default:
                        Console.WriteLine("Please choose the available option (1-5)");
                        break;
                }
                break;
            case 4:
                var employeeService = new EmployeeService();
                Console.WriteLine("""
                                  1. Add Employee to Department
                                  2. List Employees
                                  3. Get Employee
                                  4. Update Employee
                                  5. Delete Employee
                                  6. Exit
                                  """);
                Console.Write("Please choose your option: ");
                choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {
                    case 1:
                        employeeService.AddEmployee("Samuel L. Jackson", 5);
                        break;
                    case 2:
                        var employees = employeeService.GetAllEmployees();
                        employees.ForEach(e => Console.WriteLine($"Id: {e.Id} | Employee Name: {e.Fullname}"));
                        break;
                    case 3:
                        var employee = employeeService.GetEmployeeById(15);
                        var employeeName = employee?.Fullname;
                        
                        if (employeeName is not null)
                        {
                            Console.WriteLine($"You got: {employeeName} with id {employee!.Id}");
                        }
                        else
                        {
                            Console.WriteLine("Exiting...");
                        }
                        break;
                    case 4:
                        employeeService.UpdateEmployee(2);
                        break;
                    case 5:
                        employeeService.DeleteEmployee(3);
                        break;
                    case 6:
                        Console.WriteLine("Exiting to main menu...");
                        Thread.Sleep(1500);
                        break;
                    default:
                        Console.WriteLine("Please choose the available option (1-5)");
                        break;
                }
                break;
            case 5:
                Console.WriteLine("Exiting...");
                Thread.Sleep(1500);
                break;
            default:
                Console.WriteLine("Please choose the available option (1-5)");
                break;
        }
    }
}
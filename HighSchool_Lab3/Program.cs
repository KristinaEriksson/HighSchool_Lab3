using HighSchool_Lab3.Data;
using HighSchool_Lab3.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;

namespace HighSchool_Lab3
{
    internal class Program
    {
        // Kristina Eriksson .NET22

        static void Main(string[] args)
        {
            MainMenu();
        }
        public static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Forza HighSchool");
                Console.WriteLine(new string('^', (36)));
                Console.WriteLine("1. Employees");
                Console.WriteLine("2. Students");
                Console.WriteLine("3. Add new student");
                Console.WriteLine("4. Add new employee");
                Console.WriteLine("0. Exit");
                Console.WriteLine(new string('^', (36)));
                Console.Write("Enter a number to select an option: ");
                int input = Convert.ToInt32(Console.ReadLine());

                while (true)
                {
                    switch (input)
                    {
                        case 0:
                            Environment.Exit(0);
                            break;
                        case 1:
                            EmployeeMenu();
                            break;
                        case 2:
                            Students();
                            break;
                        case 3:
                            AddStudent();
                            break;
                        case 4:
                            AddEmployee();
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine(new string('^', (36)));
                            Console.WriteLine("Invalid option!!\nPlease try again by hit the enter key.");
                            Console.WriteLine(new string('^', (36)));
                            Console.ReadLine();
                            MainMenu();
                            break;
                    }
                }
            }
        }
    
            
        
        public static void EmployeeMenu()
        {
            Console.Clear();
            Console.WriteLine("Employee Menu");
            Console.WriteLine(new string('^', (36)));
            Console.WriteLine("1. All Employees");
            Console.WriteLine("2. Teacher");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine(new string('^', (36)));
            Console.Write("Enter a number to select an option: ");

            int keyInput = Convert.ToInt32(Console.ReadLine());

            switch (keyInput)
            {
                case 1:
                    Employees();
                    break;
                case 2:
                    Teachers();
                    break;
                case 0:
                    MainMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine(new string('^', (36)));
                    Console.WriteLine("Invalid choice!!");
                    Console.WriteLine("Press Enter to try again.");
                    Console.WriteLine(new string('^', (36)));
                    Console.ReadKey();
                    break;
            }
        }
        public static void Employees()
        {
            // All Employees SQL
            Console.Clear();
            Console.WriteLine("All Employees");
            Console.WriteLine(new string('^', (33)));

            // Connection between VS and SSMS database
            SqlConnection sqlCon = new SqlConnection(@"Data Source = DESKTOP-8VGM57M; Initial Catalog = HighSchool3.0; Integrated Security = true");

            // Fetch data from HighSchool3.0
            SqlDataAdapter sqlData = new SqlDataAdapter("select * from Employee", sqlCon);

            // Empty data table
            DataTable dtTbl = new DataTable();

            // Add data to the empty data table
            sqlData.Fill(dtTbl);

            // Write out the data from database
            foreach (DataRow dr in dtTbl.Rows)
            {
                Console.WriteLine("Employment number: " + dr["EmploymentNumber"]);
                Console.WriteLine(dr["FirstName"] + " " + dr["LastName"]);
                Console.WriteLine("Social Security number: " + dr["SocialSecurityNumber"]);
                Console.WriteLine("Title: " + dr["Title"]);
                Console.WriteLine("Salary: " + dr["Salary"]);
                Console.WriteLine("Emplyment date: " + dr["EmploymentDate"]);
                Console.WriteLine(new string('-', (33)));
            }
            Console.WriteLine("Press Enter to go to the menu.");
            Console.ReadLine();
            EmployeeMenu();
        }
        public static void Teachers()
        {
            // Employees 'Teachers' SQL
            Console.Clear();
            Console.WriteLine("Teachers");
            Console.WriteLine(new string('^', (33)));

            // SQL string, used to get some data from the database
            string teachers = "select * from Employee where Title = 'Teacher'";

            // Connection between VS and SMSS database
            SqlConnection sqlCon = new SqlConnection(@"Data Source = DESKTOP-8VGM57M; Initial Catalog = HighSchool3.0; Integrated Security = true");

            // Fetch data from HighSchool3.0
            SqlDataAdapter dataSql = new SqlDataAdapter(teachers, sqlCon);

            // Empty data table
            DataTable dtTbl = new DataTable();

            // Add data to the empty data table
            dataSql.Fill(dtTbl);

            // Write out the data from the database
            foreach (DataRow dr in dtTbl.Rows)
            {
                Console.WriteLine("Employment number: " + dr["EmploymentNumber"]);
                Console.WriteLine(dr["FirstName"] + " " + dr["LastName"]);
                Console.WriteLine("Social Security Number: " + dr["SocialSecurityNumber"]);
                Console.WriteLine("Title: " + dr["Title"]);
                Console.WriteLine("Salary: " + dr["Salary"]);
                Console.WriteLine("Employment date: " + dr["EmploymentDate"]);
                Console.WriteLine(new string('-', (33)));
            }
            Console.WriteLine("Press Enter to go to the menu.");
            Console.ReadLine();
            EmployeeMenu();
        }
        public static void Students()
        {
            // All students EF
            
            Console.Clear();
            Console.WriteLine("All students: ");
            Console.WriteLine(new string('^', (33)));

            // Connection between VS and the database
            using var context = new Data.HighSchoolContext();

            // Get the data from the database and it order by firstname in descending order
            var myStudents = context.Students.OrderByDescending(s => s.FirstName);

            // write out the data
            foreach (var item in myStudents)
            {
                Console.WriteLine("Name: " + item.FirstName + " " + item.LastName);
                Console.WriteLine("Social security number: " + item.SocialSecurityNumber);
                Console.WriteLine("Class: " + item.Class);
                Console.WriteLine(new string('-', (33)));
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            Console.Clear();

            // All the classes EF
            Console.WriteLine("All the Classes");
            Console.WriteLine(new string('^', (33)));

            // Get the data from the database, all classes, using distinct to remove duplicates
            var myStudents2 = context.Students.Select(c => new { c.Class }). Distinct();

            // Write out the data
            foreach (var item in myStudents2)
            {
                Console.WriteLine(item.Class);
                Console.WriteLine(new string('*', (33)));
            }
            Console.WriteLine("Press Enter key to continue");
            Console.ReadLine();
            Console.Clear();

            // One class EF
            Console.WriteLine("All the students in class Teknik");
            Console.WriteLine(new string('^', (33)));

            // Get all the students in class Teknik
            var myStudents3 = from student in context.Students
                              where student.Class == "Teknik"
                              select student;
            // Write out the data
            foreach (var item in myStudents3)
            {
                Console.WriteLine("Name: " + item.FirstName + " " + item.LastName);
                Console.WriteLine("Class: " + item.Class);
                Console.WriteLine(new string('-', (33)));
            }

            Console.WriteLine("Press Enter key to continue");
            Console.ReadLine();
            Console.Clear();

            // Recent grades SQL
            Console.WriteLine("Recent grades: ");
            Console.WriteLine(new string('^', (33)));

            // Connection between VS and SSMS
            SqlConnection sqlCon = new SqlConnection(@"Data Source = DESKTOP-8VGM57M; Initial Catalog = HighSchool3.0; Integrated Security = true");

            // Get the data from the database HighSchool3.0, Subject which are set later then 2022-11-01
            SqlDataAdapter dtGrade = new SqlDataAdapter("select StudentName, Subject, Grade from Grade where SetDate > '2022-11-01'", sqlCon);
            // Empty data table
            DataTable dataGrade = new DataTable();
            // Add data to the empty data table
            dtGrade.Fill(dataGrade);

            // Write out data
            foreach (DataRow dr in dataGrade.Rows)
            {
                Console.WriteLine(dr["StudentName"]);
                Console.WriteLine(dr["Subject"] + " " + "Grade: " + dr["Grade"]);
                Console.WriteLine(new string('-', (33)));
            }

            Console.WriteLine("Press Enter key to continue");
            Console.ReadLine();
            Console.Clear();

            // Average grade SQL
            Console.WriteLine("Average, Max and Min grades");
            Console.WriteLine(new string('^', (33)));

            // Get data from database, Average, Max and Min grade in each subject
            SqlDataAdapter dataGrades = new SqlDataAdapter("select subject, AVG(Grade)as AverageGrade, Max(Grade)as HighestGrade, Min(Grade)as LowestGrade from Grade group by subject", sqlCon);

            // Empty data table
            DataTable recentGrades = new DataTable();
            // Add data to the empty data table
            dataGrades.Fill(recentGrades);
            
            // Write out the data
            foreach (DataRow dr in recentGrades.Rows)
            {
                Console.WriteLine(dr["Subject"]);
                Console.WriteLine("Averagegrade: " + dr["AverageGrade"]);
                Console.WriteLine("Highestgrade: " +dr["HighestGrade"]);
                Console.WriteLine("Lowestgrade: " +dr["LowestGrade"]);  
                Console.WriteLine(new string('-', (33)));
            }
            Console.WriteLine("Press Enter key to go to the menu.");
            Console.ReadLine();
            EmployeeMenu();
            
        }
        public static void AddStudent()
        {
            //Add new student SQL
            Console.Clear();
            Console.WriteLine("Add a new student");
            Console.WriteLine(new string('^', (33)));

            // Get input from the user, firstname, lastname, social security number and class
            Console.Write("Enter firstName: ");
            string fName = Console.ReadLine();
            Console.Write("Enter lastName: ");
            string lName = Console.ReadLine();
            Console.Write("Enter social security number(YYYYMMDDXXXX): ");
            string ssn = Console.ReadLine();
            Console.Write("Enter class: ");
            string stuClass = Console.ReadLine();

            // Connection between VS and database
            SqlConnection sqlCon = new SqlConnection(@"Data Source = DESKTOP-8VGM57M; Initial Catalog = HighSchool3.0; Integrated Security = true");
            // SQL string, to insert the values to the database
            string addNew = "insert into Student (FirstName, LastName, SocialSecurityNumber, Class) " +
                "values (@FirstName, @LastName, @SocialSecurityNumber, @Class) ";

            // Insert values into the database with users input
            using(SqlCommand cmd = new SqlCommand(addNew, sqlCon))
            {
                cmd.Parameters.Add(@"FirstName", SqlDbType.NVarChar, 50).Value = fName;
                cmd.Parameters.Add(@"LastName", SqlDbType.NVarChar, 50).Value = lName;
                cmd.Parameters.Add(@"SocialSecurityNumber", SqlDbType. VarChar, 12).Value = ssn;
                cmd.Parameters.Add(@"Class", SqlDbType. NVarChar, 50).Value = stuClass;

                sqlCon.Open();
                cmd.ExecuteNonQuery();
                sqlCon.Close();
            }
            Console.WriteLine("A new student has been added to the database.");
            Console.WriteLine("Press Enter key to go to the menu.");
            Console.ReadLine();
            MainMenu();

        }
        public static void AddEmployee()
        {
            while (true)
            {
                Console.Clear();
                
                // Connection between VS and database
                using var context = new Data.HighSchoolContext();
                // Create a object
                Employee E1 = new Employee();

                // Create a random number to employee
                Random empNr = new Random();
                int randNumber = empNr.Next(00000, 99999);

                Console.WriteLine("Add a new employee");
                Console.WriteLine(new string('^', (33)));

                // Users input, firstname,lastname,social security number,title and salary
                // Employment Date and Employment Number is filled in automatically  
                Console.Write("Enter firstName: ");
                string fName = Console.ReadLine();
                Console.Write("Enter lastName: ");
                string lName = Console.ReadLine();
                Console.Write("Enter Social Security Number(YYYYMMDDXXXX): ");
                string ssn = Console.ReadLine();
                Console.Write("Enter title: ");
                string title = Console.ReadLine();
                Console.Write("Enter salary: ");
                decimal salary = Convert.ToDecimal(Console.ReadLine());
                DateTime currentDateTime = DateTime.Now;

                // Adding users input to the object E1
                E1.EmploymentNumber = randNumber;
                E1.FirstName = fName;
                E1.LastName = lName;
                E1.SocialSecurityNumber = ssn;
                E1.Title = title;
                E1.Salary = salary;
                E1.EmploymentDate = currentDateTime;

                // Adding the object E1 to the database
                context.Employees.Add(E1);
                context.SaveChanges();

                Console.WriteLine("A new employee has been added to the database.");
                Console.WriteLine("Press Enter key to go to the menu.");
                Console.ReadLine();
                MainMenu();
            }
        }
    }
}
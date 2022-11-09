using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Employee_WebApp
{/// <summary>
/// 
/// </summary>
    public class Employee
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\RC\PROG 2B\Employee_WebApp\Employee_WebApp\App_Data\EmployeeDB.mdf;Integrated Security=True");//Paste a connection string here
       
        public string EmpNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Salary { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }

        public Employee()
        {

        }
  
        public Employee(string empNumber, string name, string surname, double salary, DateTime theDOB)
        {
            EmpNumber = empNumber;
            Name = name;
            Surname = surname;
            Salary = salary;
            DOB = theDOB;
            Age = calcAge();
        }
      
        public void addNew()
        {
            SqlCommand cmd = new SqlCommand($"INSERT INTO tblEmployee VALUES('{EmpNumber}','{Name}','{Surname}',{Salary},'{DOB}',{Age})",con);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void delete(string empNum)
        {
            string strDelete = $"DELETE FROM tblEmployee WHERE EmpID = '{empNum}'";
            SqlCommand cmdDelete = new SqlCommand(strDelete,con);

            con.Open();
            cmdDelete.ExecuteNonQuery();
            con.Close();
        }

        public List<Employee> allEmployees()
        {
            string strSelect = "SELECT * FROM tblEmployee";
            SqlCommand cmdSelect = new SqlCommand(strSelect,con);
            DataTable myTable = new DataTable();
            DataRow myRow;
            SqlDataAdapter myAdapter = new SqlDataAdapter(cmdSelect);
            List<Employee> eList = new List<Employee>();

            con.Open();
            myAdapter.Fill(myTable);

            if (myTable.Rows.Count > 0)
            {
                for (int i = 0; i < myTable.Rows.Count; i++)
                {
                    myRow = myTable.Rows[i];
                    EmpNumber = (string)myRow["EmpID"]; //using a column name
                    Name = (string)myRow[1]; //using a column index
                    Surname = (string)myRow[2];
                    Salary = Convert.ToDouble(myRow[3]);
                    DOB = (DateTime)myRow[4];
                    Age = Convert.ToInt32(myRow[5]);

                    eList.Add(new Employee(EmpNumber, Name, Surname, Salary, DOB));
                }           
            }

            return eList;
        }

        public Employee getEmployee(string empID)
        {
            string strSelect = $"SELECT * FROM tblEmployee WHERE EmpID = '{empID}' ";
            SqlCommand cmdSelect = new SqlCommand(strSelect,con);

            con.Open();
            using (SqlDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    EmpNumber = (string)reader[0];
                    Name = (string)reader["Name"];
                    Surname = (string)reader[2];
                    Salary = Convert.ToDouble(reader["Salary"]);
                    DOB = (DateTime)reader[4];
                    Age = Convert.ToInt32(reader[5]);
                }
            }

            con.Close();

            return new Employee(EmpNumber,Name,Surname,Salary,DOB);

            
        }

         public int calcAge()
        {
            return DateTime.Now.Year - DOB.Year;
        }
     
        /// <summary>
        /// Returns employee details
        /// 
        /// </summary>
        /// <returns>Employee details</returns>
        public string getDetails()
        {
            return EmpNumber + " - " + Name + " " 
                + Surname + " " + Salary.ToString("c2") + " - " + calcAge() + " years old";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Employee_WebApp.Pages
{
    public class AllEmployeesModel : PageModel
    {
        public List<Employee> emList = 
            new List<Employee>();
        public void OnGet()
        {//on form load
            Employee em = new Employee();
            //emList = em.allEmployees();
        }
    }
}

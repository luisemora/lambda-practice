﻿using lamda_practice.Data;
using System;
using System.Globalization;
using System.Linq;

namespace lambda_practice
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var ctx = new DatabaseContext())
            {
                //1. Listar todos los empleados cuyo departamento tenga una sede en Chihuahua
                  var punto1 = ctx.Employees
 
                  .Where(c => c.City.Name == "Chihuahua")
                  .Select(s => new { s.Id, s.FirstName, s.LastName, s.City });
 
                     foreach (var employee in punto1)
                     {
                         Console.WriteLine(" Id: {0} Name: {1} Last Name: {2} City Name: {3}",
                         employee.Id, employee.FirstName, employee.LastName, employee.City.Name);
                     }

                //2. Listar todos los departamentos y el numero de empleados que pertenezcan a cada departamento.
                    var punto2 = ctx.Employees
                   .GroupBy(e => e.Department.Name)
                   .Select(s => new { DpName = s.Key, Count = s.Count() });
 
                     foreach (var Dept in punto2)
                     {
                         Console.WriteLine("Department: {0},  Count: {1}", 
                         Dept.DpName, Dept.Count);
                    }


                //3. Listar todos los empleados remotos. Estos son los empleados cuya ciudad no se encuentre entre las sedes de su departamento.
             
                    var punto3 = ctx.Employees
 
                     .Where(e => e.Department.Cities.Any(sede => sede.Name == e.City.Name))
                     .Distinct()
                     .Select(sede => new { sede.FirstName, sede.LastName });
 
                   foreach (var employee in punto3)
                   {
                       Console.WriteLine("Name: {0} Last Name: {1}  ",
                       employee.FirstName, employee.LastName);
                   }


                //4. Listar todos los empleados cuyo aniversario de contratación sea el próximo mes.
              
              
              

                //Listar los 12 meses del año y el numero de empleados contratados por cada mes.
                    var punto5 = ctx.Employees
  
                      .GroupBy(e => e.HireDate.Month)
                      .OrderBy(i => i.Key)
                      .Select(select => new { month = select.Key, Count = select.Count() });
  
                     foreach (var mes in punto5)
                     {
                         Console.WriteLine("Month: {0}, Employees: {1}", 
                          mes.month, mes.Count);
                     }
              }


            


            Console.Read();
        }
    }
}

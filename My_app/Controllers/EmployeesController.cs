using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using My_app.Models;
public class EmployeesController : Controller
{
    private readonly HttpClient _httpClient;
    private const string apiUrl = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

    public EmployeesController()
    {
        _httpClient = new HttpClient();
    }

    public async Task<IActionResult> Index()
    {
        List<Employee> employees = await GetEmployeesAsync();
        employees.Sort((emp1, emp2) => emp2.TotalHoursWorked.CompareTo(emp1.TotalHoursWorked));

        return View(employees);
    }

    private async Task<List<Employee>> GetEmployeesAsync()
    {
        List<Employee> employees = new List<Employee>();
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                employees = JsonConvert.DeserializeObject<List<Employee>>(jsonResponse);
            }
            else
            {
                // Handle unsuccessful response
                // Log or handle the error accordingly
                // You can also return an empty list or throw an exception
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            // Log or handle the exception accordingly
            // You can also return an empty list or throw an exception
        }

        return employees;
    }
}
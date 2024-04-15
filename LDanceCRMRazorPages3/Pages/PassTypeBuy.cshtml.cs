using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace LDanceCRMRazorPages3.Pages
{
    //чтобы страницу нельзя было просмотреть неавторизованным
    [Authorize]

    public class PassTypeBuyModel : PageModel
    {
        public string PassTypeID, SearchString;
        public PassTypeInfo passTypeInfo = new PassTypeInfo();
        public string errorMessage = "", successMessage = "", paymentErrorMessage = "";

        private readonly ILogger<PassTypeBuyModel> _logger;
        private readonly IConfiguration _configuration;

        public PassTypeBuyModel(IConfiguration configuration, ILogger<PassTypeBuyModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void OnGet()
        {
            //получаем id из формы
            SearchString = Request.Query["searchString"];
            PassTypeID = Request.Query["PassTypeID"];

            //получение строки подключения из файла конфигурации
            string cs = _configuration.GetConnectionString("AuthConnectionString");

            //вывод подходящего абонемента
            PassOut(cs, PassTypeID);
        }

        public void OnPost()
        {
            paymentErrorMessage = "В настоящее время оплата онлайн недоступна!";

            //получаем id из формы
            SearchString = Request.Query["searchString"];
            PassTypeID = Request.Query["PassTypeID"];

            //получение строки подключения из файла конфигурации
            string cs = _configuration.GetConnectionString("AuthConnectionString");

            //вывод подходящего абонемента
            PassOut(cs, PassTypeID);
        }

        private void PassOut(string cs, string PassTypeID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    string sql = @"SELECT passtypes.PassTypeName, passtypes.PassTypePrice
                                   FROM passtypes
                                   WHERE passtypes.PassTypeID = @passtypeId";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@passtypeId", PassTypeID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                passTypeInfo.PassTypeName = reader.GetString(0);
                                passTypeInfo.PassTypePrice = reader.GetDecimal(1).ToString() + " руб.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}

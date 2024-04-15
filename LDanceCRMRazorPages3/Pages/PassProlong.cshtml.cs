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

    public class PassProlongModel : PageModel
    {
        private string PassID;
        public PassTypeInfo passTypeInfo = new PassTypeInfo();
        public string errorMessage = "", successMessage = "", paymentErrorMessage = "";

        private readonly ILogger<PassProlongModel> _logger;
        private readonly IConfiguration _configuration;

        public PassProlongModel(IConfiguration configuration, ILogger<PassProlongModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void OnGet()
        {
            //получаем id из формы
            PassID = Request.Query["PassID"];

            //получение строки подключения из файла конфигурации
            string cs = _configuration.GetConnectionString("AuthConnectionString");

            //вывод подходящего абонемента
            PassOut(cs, PassID);
        }

        public void OnPost()
        {
            paymentErrorMessage = "В настоящее время оплата онлайн недоступна!";

            //получаем id из формы
            PassID = Request.Query["PassID"];

            //получение строки подключения из файла конфигурации
            string cs = _configuration.GetConnectionString("AuthConnectionString");

            //вывод подходящего абонемента
            PassOut(cs, PassID);

        }

        private void PassOut(string cs, string PassID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    string sql = @"SELECT passtypes.PassTypeID, passtypes.PassTypeName, passtypes.PassTypePrice
                                   FROM passtypes, passes
                                   WHERE passes.PassTypeID = passtypes.PassTypeID
								   AND passes.PassID = @passId";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@passId", PassID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                passTypeInfo.PassTypeID = reader.GetInt32(0).ToString();
                                passTypeInfo.PassTypeName = reader.GetString(1);
                                passTypeInfo.PassTypePrice = reader.GetDecimal(2).ToString() + " руб.";
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

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

    public class TimetableBuyModel : PageModel
    {
        public string TimetableID, SearchDate;
        public PassTypeInfo passtypeInfo = new PassTypeInfo();
        public string errorMessage = "", successMessage = "", paymentErrorMessage = "";

        private readonly ILogger<TimetableBuyModel> _logger;
        private readonly IConfiguration _configuration;

        public TimetableBuyModel(IConfiguration configuration, ILogger<TimetableBuyModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void OnGet()
        {
            //получаем id из формы
            SearchDate = Request.Query["searchDate"];
            TimetableID = Request.Query["TimetableID"];

            //получение строки подключения из файла конфигурации
            string cs = _configuration.GetConnectionString("AuthConnectionString");

            //вывод подходящего абонемента
            PassOut(cs, TimetableID);
        }

        public void OnPost()
        {
            //получаем id из формы
            SearchDate = Request.Query["searchDate"];
            TimetableID = Request.Query["TimetableID"];

            //получение строки подключения из файла конфигурации
            string cs = _configuration.GetConnectionString("AuthConnectionString");

            //вывод подходящего абонемента
            PassOut(cs, TimetableID);

            paymentErrorMessage = "В настоящее время оплата онлайн недоступна!";
        }

        private void PassOut(string cs, string TimetableID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    string sql = @"SELECT passtypes.PassTypeID, passtypes.PassTypeName, passtypes.PassTypePrice
                                   FROM passtypes
                                   JOIN timetable ON passtypes.TrainingID = timetable.TrainingID
                                   WHERE timetable.TimetableID = @timetableId";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@timetableId", TimetableID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                passtypeInfo.PassTypeID = reader.GetInt32(0).ToString();
                                passtypeInfo.PassTypeName = reader.GetString(1);
                                passtypeInfo.PassTypePrice = reader.GetDecimal(2).ToString() + " руб.";
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace LDanceCRMRazorPages3.Pages
{
    //чтобы страницу нельзя было просмотреть неавторизованным
    [Authorize]

    public class PassesModel : PageModel
    {
        public List<PassInfo> passesInfoList = new List<PassInfo>();//список активных абонементов
        public List<PassInfo> activePassesInfoList = new List<PassInfo>();//список неактивных абонементов
        public string activeSuccessMessage = "", successMessage = "", errorMessage = "";//сообщения

        private readonly ILogger<PassesModel> _logger;
        private readonly IConfiguration _configuration;

        public PassesModel(IConfiguration configuration, ILogger<PassesModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void OnGet()
        {
            //получение строки подключения из файла конфигурации
            string cs = _configuration.GetConnectionString("AuthConnectionString");

            //вывод списка активных абонементов этого клиента
            GetActivePasses(cs);

            //вывод списка неактивных абонементов этого клиента
            GetInactivePasses(cs);         
            
        }

        private void GetActivePasses(string cs)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    string sql = @"SELECT passes.PassID, trainings.TrainingName, passtypes.PassTypeNumberOfVisits, passes.PassDate, passes.NumberOfVisits 
                                   FROM passes, passtypes, clients, trainings 
                                   WHERE passes.ClientID = clients.ClientID 
                                   AND passes.PassTypeID = passtypes.PassTypeID 
                                   AND passes.ClientID = clients.ClientID 
                                   AND clients.ClientPhone = @phone 
                                   AND passtypes.TrainingID = trainings.TrainingID
                                   AND passes.NumberOfVisits > 0
                                   ORDER BY passes.PassDate;";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@phone", HttpContext.User.Identity.Name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PassInfo passInfo = new PassInfo();
                                passInfo.PassID = reader.GetInt32(0).ToString();
                                passInfo.PassTypeName = reader.GetString(1) + "/" + reader.GetInt32(2).ToString() + " посещений";
                                passInfo.PassDate = reader.GetDateTime(3);
                                passInfo.NumberOfVisits = reader.GetInt32(4).ToString();

                                activePassesInfoList.Add(passInfo);

                            }
                        }
                    }
                }

                if (activePassesInfoList.Count == 0)
                {
                    activeSuccessMessage = "У вас нет активных абонементов.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        private void GetInactivePasses(string cs)
        {
            try
            {
                using (SqlConnection connection1 = new SqlConnection(cs))
                {
                    connection1.Open();
                    string sql1 = @"SELECT passes.PassID, trainings.TrainingName, passtypes.PassTypeNumberOfVisits, passes.PassDate, passes.NumberOfVisits 
                                   FROM passes, passtypes, clients, trainings 
                                   WHERE passes.ClientID = clients.ClientID 
                                   AND passes.PassTypeID = passtypes.PassTypeID 
                                   AND passes.ClientID = clients.ClientID 
                                   AND clients.ClientPhone = @phone 
                                   AND passtypes.TrainingID = trainings.TrainingID
                                   AND passes.NumberOfVisits = 0
                                   ORDER BY passes.PassDate;";

                    using (SqlCommand cmd1 = new SqlCommand(sql1, connection1))
                    {
                        cmd1.Parameters.AddWithValue("@phone", HttpContext.User.Identity.Name);
                        using (SqlDataReader reader1 = cmd1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                PassInfo passInfo1 = new PassInfo();
                                passInfo1.PassID = reader1.GetInt32(0).ToString();
                                passInfo1.PassTypeName = reader1.GetString(1) + "/" + reader1.GetInt32(2).ToString() + " посещений";
                                passInfo1.PassDate = reader1.GetDateTime(3);
                                passInfo1.NumberOfVisits = reader1.GetInt32(4).ToString();

                                passesInfoList.Add(passInfo1);

                            }
                        }
                    }
                }

                if (passesInfoList.Count == 0)
                {
                    successMessage = "У вас нет неактивных абонементов.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }

    //класс для хранения данных о посещениях клиента
    public class PassInfo
    {
        public string PassID;
        public DateTime PassDate;

        public string PassTypeID;
        public string PassTypeName;

        public string NumberOfVisits;
    }
}

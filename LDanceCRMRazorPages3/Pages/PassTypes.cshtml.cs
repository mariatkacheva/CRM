using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace LDanceCRMRazorPages3.Pages
{
    public class PassTypesModel : PageModel
    {
        public string SearchString;
        public List<PassTypeInfo> passtypesList = new List<PassTypeInfo>();//список видов абонементов
        public string errorMessage = "", successMessage = "";

        private readonly ILogger<PassTypesModel> _logger;
        private readonly IConfiguration _configuration;

        public PassTypesModel(IConfiguration configuration, ILogger<PassTypesModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void OnGet()
        {
            SearchString = Request.Query["searchString"];

            //получение строки подключения из файла конфигурации
            string cs = _configuration.GetConnectionString("AuthConnectionString");
           
            if ((SearchString == null) || (SearchString.Length == 0))//если поисковая строка пуста то выводится обычный список видов абонементов
            {
                //загрузка списка видов абонементов
                LoadPassTypesList(cs);                
            }
            else//если в поисковой строке написано что-то
            {
                //поиск по названию или типу
                LoadForSearch(cs, SearchString);
            }
        }

        //ЗАГРУЗКА СПИСКА ВИДОВ АБОНЕМЕНТОВ
        private void LoadPassTypesList(string cs)
        {
            try
            {
                //подключение к бд
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    //получение списка видов абонементов
                    string sql = @"SELECT passtypes.PassTypeID, passtypes.PassTypeName, passtypes.PassTypeNumberOfVisits, passtypes.PassTypePrice, trainings.TrainingName, trainingtypes.TrainingTypeName
                                       FROM   passtypes INNER JOIN
                                       trainings ON passtypes.TrainingID = trainings.TrainingID INNER JOIN
                                       trainingtypes ON trainings.TrainingTypeID = trainingtypes.TrainingTypeID;";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PassTypeInfo passTypeInfo = new PassTypeInfo();
                                passTypeInfo.PassTypeID = reader.GetInt32(0).ToString();
                                passTypeInfo.PassTypeName = reader.GetString(1);
                                passTypeInfo.PassTypeNumberOfVisits = reader.GetInt32(2).ToString();
                                passTypeInfo.PassTypePrice = reader.GetDecimal(3).ToString();
                                passTypeInfo.TrainingName = reader.GetString(4);
                                passTypeInfo.TrainingTypeName = reader.GetString(5);

                                //Куплен ли абонемент текущим клиентом?

                                int count = -1;

                                using (SqlConnection connection1 = new SqlConnection(cs))
                                {
                                    connection1.Open();
                                    string sql1 = @"SELECT COUNT(passes.PassID) AS NumberOfPasses
                                                        FROM passes
                                                        JOIN clients ON passes.ClientID = clients.ClientID
                                                        WHERE passes.PassTypeID = @passtypeId 
                                                        AND clients.ClientPhone = @phone;";

                                    using (SqlCommand cmd1 = new SqlCommand(sql1, connection1))
                                    {
                                        cmd1.Parameters.AddWithValue("@passtypeId", passTypeInfo.PassTypeID);
                                        cmd1.Parameters.AddWithValue("@phone", HttpContext.User.Identity.Name);
                                        count = (int)cmd1.ExecuteScalar();
                                    }
                                }

                                if (count > 0)
                                {
                                    passTypeInfo.isSold = true;
                                }
                                else
                                {
                                    passTypeInfo.isSold = false;
                                }

                                passtypesList.Add(passTypeInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exeption: " + ex.ToString());
            }            
        }

        //ПОИСК ПО НАЗВАНИЮ ИЛИ ТИПУ
        private void LoadForSearch(string cs, string SearchString)
        {
            try
            {
                //подключение к бд
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    string sql = @"SELECT passtypes.PassTypeID, passtypes.PassTypeName, passtypes.PassTypeNumberOfVisits, passtypes.PassTypePrice, trainings.TrainingName, trainingtypes.TrainingTypeName
                                       FROM   passtypes INNER JOIN
                                       trainings ON passtypes.TrainingID = trainings.TrainingID INNER JOIN
                                       trainingtypes ON trainings.TrainingTypeID = trainingtypes.TrainingTypeID
	                                   WHERE  trainings.TrainingName = @s;";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@s", SearchString);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PassTypeInfo passTypeInfo = new PassTypeInfo();
                                passTypeInfo.PassTypeID = reader.GetInt32(0).ToString();
                                passTypeInfo.PassTypeName = reader.GetString(1);
                                passTypeInfo.PassTypeNumberOfVisits = reader.GetInt32(2).ToString();
                                passTypeInfo.PassTypePrice = reader.GetDecimal(3).ToString();
                                passTypeInfo.TrainingName = reader.GetString(4);
                                passTypeInfo.TrainingTypeName = reader.GetString(5);


                                //Куплен ли абонемент текущим клиентом?

                                int count = -1;

                                using (SqlConnection connection1 = new SqlConnection(cs))
                                {
                                    connection1.Open();
                                    string sql1 = @"SELECT COUNT(passes.PassID) AS NumberOfPasses
                                                        FROM passes
                                                        JOIN clients ON passes.ClientID = clients.ClientID
                                                        WHERE passes.PassTypeID = @passtypeId 
                                                        AND clients.ClientPhone = @phone;";

                                    using (SqlCommand cmd1 = new SqlCommand(sql1, connection1))
                                    {
                                        cmd1.Parameters.AddWithValue("@passtypeId", passTypeInfo.PassTypeID);
                                        cmd1.Parameters.AddWithValue("@phone", HttpContext.User.Identity.Name);
                                        count = (int)cmd1.ExecuteScalar();
                                    }
                                }

                                if (count > 0)
                                {
                                    passTypeInfo.isSold = true;
                                }
                                else
                                {
                                    passTypeInfo.isSold = false;
                                }

                                passtypesList.Add(passTypeInfo);
                            }
                        }
                    }
                }

                if (passtypesList.Count < 1)
                {
                    errorMessage = "По данному запросу ничего не найдено.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exeption: " + ex.ToString());
            }
        }
        
    }

    //класс для хранения данных типа абонемента
    public class PassTypeInfo
    {
        public String PassTypeID;
        public String PassTypeName;
        public String PassTypePrice;
        public String PassTypeNumberOfVisits;

        public String TrainingID;
        public String TrainingName;
        public String TrainingTypeName;

        //куплен ли абонемент
        public bool isSold = false;
    }
}

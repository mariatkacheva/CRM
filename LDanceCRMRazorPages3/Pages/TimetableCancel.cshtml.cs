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

    public class TimetableCancelModel : PageModel
    {
        public string TimetableID, SearchDate;
        public TimetableInfo timetableInfo = new TimetableInfo();
        public string errorMessage = "", successMessage = "";

        private readonly ILogger<TimetableCancelModel> _logger;
        private readonly IConfiguration _configuration;

        public TimetableCancelModel(IConfiguration configuration, ILogger<TimetableCancelModel> logger)
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

            //вывод текущей записи
            VisitOut(cs, TimetableID);
        }

        public void OnPost()
        {
            //получаем id из формы
            SearchDate = Request.Query["searchDate"];
            TimetableID = Request.Query["TimetableID"];

            //получение строки подключения из файла конфигурации
            string cs = _configuration.GetConnectionString("AuthConnectionString");

            //отмена брони текущей записи
            VisitDelete(cs, TimetableID);          
        }

        private void VisitOut(string cs, string TimetableID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    string sql = @"SELECT trainings.TrainingName, 
                            employees.EmployeeSurname + ' ' + 
                            employees.EmployeeName + ' ' + 
                            employees.EmployeeMiddleName AS 'TrainerName',  
                            halls.HallName, 
                            timetable.TimetableDate, 
                            timetable.StartTime, 
                            timetable.EndTime,
                            trainingtypes.TrainingTypeName 
                            FROM timetable, trainings, halls, employees, trainingtypes 
                            WHERE timetable.TrainingID = trainings.TrainingID 
                            AND timetable.HallID = halls.HallID 
                            AND timetable.TrainerID = employees.EmployeeID
                            AND trainings.TrainingTypeID = trainingtypes.TrainingTypeID
                            AND timetable.TimetableID = @timetableId";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@timetableId", TimetableID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                timetableInfo.TrainingName = reader.GetString(0);
                                timetableInfo.TrainerName = reader.GetString(1);
                                timetableInfo.HallName = reader.GetString(2);
                                timetableInfo.TimetableDate = reader.GetDateTime(3);
                                timetableInfo.StartTime = reader.GetTimeSpan(4);
                                timetableInfo.EndTime = reader.GetTimeSpan(5);
                                timetableInfo.TrainingTypeName = reader.GetString(6);
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

        private void VisitDelete(string cs, string TimetableID)
        {
            //1. получим PassID с которого осуществялось бронирование
            int passId = -1;

            using (SqlConnection connection4 = new SqlConnection(cs))
            {
                connection4.Open();
                string sql4 = @"SELECT PassID FROM visits WHERE TimetableID = @timetableId;";

                using (SqlCommand cmd4 = new SqlCommand(sql4, connection4))
                {
                    cmd4.Parameters.AddWithValue("@timetableId", TimetableID);

                    //получаем только первый результат - PassID с самым большим кол-вом оставшихся посещений
                    passId = (int)cmd4.ExecuteScalar();
                }
            }

            //2.Удалим запись в таблице visits

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                string sql = "DELETE FROM visits WHERE TimetableID = @timetableId;";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("timetableId", TimetableID);
                    cmd.ExecuteNonQuery();
                }
            }

            //3.увелисить кол-во оставшихся посещений в абонементе
            using (SqlConnection connection2 = new SqlConnection(cs))
            {
                connection2.Open();
                string sql2 = "UPDATE passes SET NumberOfVisits = NumberOfVisits + 1 WHERE PassID = @passId;";
                using (SqlCommand cmd2 = new SqlCommand(sql2, connection2))
                {
                    cmd2.Parameters.AddWithValue("@passId", passId);
                    cmd2.ExecuteNonQuery();
                }
            }

            //4.увеличить кол-во оставшихся мест в записи расписания
            using (SqlConnection connection3 = new SqlConnection(cs))
            {
                connection3.Open();
                string sql3 = "UPDATE timetable SET NumberOfPlaces = NumberOfPlaces + 1 WHERE TimetableID = @timetableId;";
                using (SqlCommand cmd3 = new SqlCommand(sql3, connection3))
                {
                    cmd3.Parameters.AddWithValue("@timetableId", TimetableID);
                    cmd3.ExecuteNonQuery();
                }
            }

            successMessage = "Отмена бронирования подтверждена.";
            Response.Redirect($"/Timetable/?searchDate={SearchDate}");
        }
    }
}

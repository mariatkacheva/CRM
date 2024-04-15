using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LDanceCRMRazorPages3.Pages
{
   
    //чтобы страницу нельзя было просмотреть неавторизованным
    [Authorize]

    public class IndexModel : PageModel
    {
        public List<VisitInfo> visitInfoList = new List<VisitInfo>();//список запланированных записей
        public List<VisitInfo> futureVisitInfoList = new List<VisitInfo>();//список прошедших записей
        public string futureSuccessMessage = "", successMessage = "", errorMessage = "";//сообщения

        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void OnGet()
        {
            //получение строки подключения из файла конфигурации
            string cs = _configuration.GetConnectionString("AuthConnectionString");

            //вывод списка запланированных посещений этого клиента
            GetFutureVisits(cs);

            //вывод списка прошедших посещений этого клиента
            GetPastVisits(cs);
        }
       
        private void GetFutureVisits(string cs)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    string sql = @"SELECT visits.VisitID,
                           timetable.TimetableDate, 
                           timetable.StartTime, 
                           timetable.EndTime, 
                           trainings.TrainingName,
                           trainingtypes.TrainingTypeName,
                           halls.HallName,
                           employees.EmployeeSurname + ' ' + employees.EmployeeName + ' ' + employees.EmployeeMiddleName AS 'TrainerName',
                           timetable.TimetableID
                           FROM timetable, trainings, halls, employees, trainingtypes, visits, clients, passes 
                           WHERE timetable.TrainingID = trainings.TrainingID 
                           AND timetable.HallID = halls.HallID 
                           AND timetable.TrainerID = employees.EmployeeID
                           AND trainings.TrainingTypeID = trainingtypes.TrainingTypeID
                           AND visits.TimetableID = timetable.TimetableID
                           AND visits.PassID = passes.PassID
                           AND passes.ClientID = clients.ClientID                                  
                           AND timetable.TimetableDate > CAST(GETDATE() AS DATE) 
                           AND (timetable.TimetableDate > CAST(GETDATE() AS DATE) OR timetable.StartTime > CAST(GETDATE() AS TIME))
                           AND clients.ClientPhone = @phone
                           ORDER BY timetable.TimetableDate, timetable.StartTime";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@phone", HttpContext.User.Identity.Name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VisitInfo visitInfo = new VisitInfo();
                                visitInfo.VisitID = reader.GetInt32(0).ToString();
                                visitInfo.TimetableDate = reader.GetDateTime(1);
                                visitInfo.StartTime = reader.GetTimeSpan(2);
                                visitInfo.EndTime = reader.GetTimeSpan(3);

                                visitInfo.TrainingName = reader.GetString(4);
                                visitInfo.TrainingTypeName = reader.GetString(5);
                                visitInfo.HallName = reader.GetString(6);
                                visitInfo.TrainerName = reader.GetString(7);
                                visitInfo.TimetableID = reader.GetInt32(8).ToString();

                                futureVisitInfoList.Add(visitInfo);
                            }
                        }
                    }
                }

                if (futureVisitInfoList.Count == 0)
                {
                    futureSuccessMessage = "Нет запланированных записей.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        private void GetPastVisits(string cs)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    string sql = @"SELECT visits.VisitID,
                           timetable.TimetableDate, 
                           timetable.StartTime, 
                           timetable.EndTime, 
                           trainings.TrainingName,
                           trainingtypes.TrainingTypeName,
                           halls.HallName, 
                           employees.EmployeeSurname + ' ' + employees.EmployeeName + ' ' + employees.EmployeeMiddleName AS 'TrainerName',
                           timetable.TimetableID
                           FROM timetable, trainings, halls, employees, trainingtypes, visits, clients, passes 
                           WHERE timetable.TrainingID = trainings.TrainingID 
                           AND timetable.HallID = halls.HallID 
                           AND timetable.TrainerID = employees.EmployeeID
                           AND trainings.TrainingTypeID = trainingtypes.TrainingTypeID
                           AND visits.TimetableID = timetable.TimetableID
                           AND visits.PassID = passes.PassID
                           AND passes.ClientID = clients.ClientID                                  
                           AND timetable.TimetableDate <= CAST(GETDATE() AS DATE)
                           AND (timetable.TimetableDate <= CAST(GETDATE() AS DATE) OR timetable.StartTime <= CAST(GETDATE() AS TIME))
                           AND clients.ClientPhone = @phone
                           ORDER BY timetable.TimetableDate, timetable.StartTime";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@phone", HttpContext.User.Identity.Name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VisitInfo visitInfo = new VisitInfo();
                                visitInfo.VisitID = reader.GetInt32(0).ToString();
                                visitInfo.TimetableDate = reader.GetDateTime(1);
                                visitInfo.StartTime = reader.GetTimeSpan(2);
                                visitInfo.EndTime = reader.GetTimeSpan(3);

                                visitInfo.TrainingName = reader.GetString(4);
                                visitInfo.TrainingTypeName = reader.GetString(5);
                                visitInfo.HallName = reader.GetString(6);
                                visitInfo.TrainerName = reader.GetString(7);
                                visitInfo.TimetableID = reader.GetInt32(8).ToString();

                                visitInfoList.Add(visitInfo);
                            }
                        }
                    }
                }

                if (visitInfoList.Count == 0)
                {
                    successMessage = "У вас пока ещё нет совершенных посещений.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

    }

    //класс для хранения данных о посещениях клиента
    public class VisitInfo
    {
        public string VisitID;

        public string TimetableID;
        public DateTime TimetableDate;

        public TimeSpan StartTime;
        public TimeSpan EndTime;

        public string TrainingName;
        public string TrainingTypeName;

        public string TrainerName;
        public string HallName;
             
    }
}

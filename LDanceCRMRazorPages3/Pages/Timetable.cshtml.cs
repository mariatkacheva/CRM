using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace LDanceCRMRazorPages3.Pages
{
    //����� �������� ������ ���� ����������� ����������������
    [Authorize]

    public class TimetableModel : PageModel
    {
        public string SearchDate;
        public List<TimetableInfo> timetableInfoList = new List<TimetableInfo>();//������ ������� ����������
        public string errorMessage = "", successMessage = "";

        private readonly ILogger<TimetableModel> _logger;
        private readonly IConfiguration _configuration;

        public TimetableModel(IConfiguration configuration, ILogger<TimetableModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void OnGet()
        {
            DateTime date;
            string search = Request.Query["searchDate"];

            if (string.IsNullOrEmpty(search))// ���� �������� searchDate �� ������, ���������� ���������� �� ������� ����
            {
                date = DateTime.Now;
            }
            else
            {

                if (!DateTime.TryParse(search, out date))
                {
                    errorMessage = "���� ������� �������!";
                }
            }

            SearchDate = date.ToString("yyyy-MM-dd"); // ��������� �������� SearchDate

            //��������� ������ ����������� �� ����� ������������
            string cs = _configuration.GetConnectionString("AuthConnectionString");

            //����� ������ ������� �� ���� ����
            LoadTimetable(cs, date);
           
        }

        //�������� ���������� �������
        private void LoadTimetable(string cs, DateTime date)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    string sql = @"SELECT timetable.TimetableID, 
                            trainings.TrainingID, 
                            trainings.TrainingName, 
                            employees.EmployeeID, 
                            employees.EmployeeSurname + ' ' + 
                            employees.EmployeeName + ' ' + 
                            employees.EmployeeMiddleName AS 'TrainerName', 
                            halls.HallID, 
                            halls.HallName, 
                            timetable.TimetableDate, 
                            timetable.StartTime, 
                            timetable.EndTime, 
                            timetable.NumberOfPlaces, 
                            trainingtypes.TrainingTypeID, 
                            trainingtypes.TrainingTypeName 
                    FROM timetable, trainings, halls, employees, trainingtypes 
                    WHERE timetable.TrainingID = trainings.TrainingID 
                            AND timetable.HallID = halls.HallID 
                            AND timetable.TrainerID = employees.EmployeeID
                            AND trainings.TrainingTypeID = trainingtypes.TrainingTypeID
                            AND timetable.TimetableDate = @date
                    ORDER BY timetable.StartTime";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TimetableInfo timetableInfo = new TimetableInfo();
                                timetableInfo.TimetableID = reader.GetInt32(0).ToString();

                                timetableInfo.TrainingID = reader.GetInt32(1).ToString();
                                timetableInfo.TrainingName = reader.GetString(2);

                                timetableInfo.TrainerID = reader.GetInt32(3).ToString();
                                timetableInfo.TrainerName = reader.GetString(4);

                                timetableInfo.HallID = reader.GetInt32(5).ToString();
                                timetableInfo.HallName = reader.GetString(6);

                                timetableInfo.TimetableDate = reader.GetDateTime(7);
                                timetableInfo.StartTime = reader.GetTimeSpan(8);
                                timetableInfo.EndTime = reader.GetTimeSpan(9);
                                timetableInfo.NumberOfPlaces = reader.GetInt32(10);

                                timetableInfo.TrainingTypeID = reader.GetInt32(11).ToString();
                                timetableInfo.TrainingTypeName = reader.GetString(12);

                                //���� ClientID
                                int clientId = -1;

                                using (SqlConnection connection1 = new SqlConnection(cs))
                                {
                                    connection1.Open();
                                    string sql1 = "SELECT ClientID FROM clients WHERE ClientPhone = @phone;";
                                    using (SqlCommand cmd1 = new SqlCommand(sql1, connection1))
                                    {
                                        cmd1.Parameters.AddWithValue("@phone", HttpContext.User.Identity.Name);
                                        object result = cmd1.ExecuteScalar();
                                        if (result != null)
                                        {
                                            clientId = Convert.ToInt32(result);
                                        }
                                    }
                                }

                                //������������� �� ������ ���� ��������?
                                timetableInfo.isBooked = IsBooked(cs, timetableInfo, clientId);


                                //������ �� ��������� �� ��� ����������?
                                timetableInfo.isAvailable = IsAvailible(cs, timetableInfo, clientId);


                                //��������� ������ � ������
                                timetableInfoList.Add(timetableInfo);
                            }
                        }
                    }
                }

                if (timetableInfoList.Count == 0)
                {
                    successMessage = "��� ������� �� �������� ����.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        private bool IsBooked(string cs, TimetableInfo timetableInfo, int clientId)
        {            
            //�������� ���������� ������������ ������� �� ��� ������� 
            int countOfReservations = -1;

            using (SqlConnection connection3 = new SqlConnection(cs))
            {
                connection3.Open();
                string sql3 = "SELECT COUNT(*) AS VisitCount FROM visits INNER JOIN passes ON visits.PassID = passes.PassID WHERE visits.TimetableID = @timetableId AND passes.ClientID = @clientId;";
                using (SqlCommand cmd3 = new SqlCommand(sql3, connection3))
                {
                    cmd3.Parameters.AddWithValue("@clientId", clientId.ToString());
                    cmd3.Parameters.AddWithValue("@timetableId", timetableInfo.TimetableID);
                    countOfReservations = (int)cmd3.ExecuteScalar();
                }
            }

            //���� ���� ����� ������ - ������ ��� ������������ ��� �������
            if (countOfReservations > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsAvailible(string cs, TimetableInfo timetableInfo, int clientId)
        {
            //�������� ������ - ���������� ��������� � ��������� ����������� (���������� �������� ����������), ������. �� ��������
            int numberOfVisits = -1;

            using (SqlConnection connection4 = new SqlConnection(cs))
            {
                connection4.Open();
                string sql4 = @"SELECT TOP 1 passes.NumberOfVisits
                                                    FROM passes
                                                    INNER JOIN passtypes ON passes.PassTypeID = passtypes.PassTypeID
                                                    WHERE passes.ClientID = @clientId
                                                    AND passtypes.TrainingID = @trainingId
                                                    ORDER BY passes.NumberOfVisits DESC;";
                using (SqlCommand cmd4 = new SqlCommand(sql4, connection4))
                {
                    cmd4.Parameters.AddWithValue("@clientId", clientId.ToString());
                    cmd4.Parameters.AddWithValue("@trainingId", timetableInfo.TrainingID);
                    //�������� ������ ������ ��������� - ����� ������� ���-��
                    numberOfVisits = (int?)cmd4.ExecuteScalar() ?? 0; //���� ������� �� �������� ������, �� ����������� 0
                }
            }

            //���� ���� ����� ��������� ���������� � ���������� ���������� ���������  �� ��� > 0 - ������� �������� ��� ������������ ��� ��������
            if (numberOfVisits > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }



    //����� ��� �������� ������ � ������ ����������
    public class TimetableInfo
    {
        public string TimetableID;

        public string TrainingID;
        public string TrainingName;
        
        public string TrainerID;
        public string TrainerName;

        public string HallID;
        public string HallName;

        public DateTime TimetableDate;
        public TimeSpan StartTime;
        public TimeSpan EndTime;
        public int NumberOfPlaces;

        public string TrainingTypeID;
        public string TrainingTypeName;

        //������������� �� �������� ��� �������
        public bool isBooked = false;

        //������ �� ��������� �� ��� �������
        public bool isAvailable = false;

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LDanceCRMRazorPages3.Pages
{
    //чтобы страницу нельзя было просмотреть неавторизованным
    [Authorize]

    public class UserProfileModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "", successMessage = "";

        private readonly ILogger<UserProfileModel> _logger;
        private readonly IConfiguration _configuration;

        public UserProfileModel(IConfiguration configuration, ILogger<UserProfileModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        
        public void OnGet()
        {
            //получение строки подключения из файла конфигурации
            string cs = _configuration.GetConnectionString("AuthConnectionString");

            String phone = HttpContext.User.Identity.Name;

            //получение данных текущего клиента
            LoadProfile(cs, phone);
        }

        
        public void OnPost()
        {                   
            //для получения значений используются имена полей ввода в форме - <input name="ClientName"..
            clientInfo.ClientID = Request.Form["ClientID"];
            clientInfo.ClientSurname = Request.Form["ClientSurname"];
            clientInfo.ClientName = Request.Form["ClientName"];
            clientInfo.ClientMiddleName = Request.Form["ClientMiddleName"];
            clientInfo.ClientBirthDate = Convert.ToDateTime(Request.Form["ClientBirthDate"]);
            clientInfo.ClientPhone = Request.Form["ClientPhone"];

            //получение строки подключения из файла конфигурации
            string cs = _configuration.GetConnectionString("AuthConnectionString");

            //проверка введенных данных
            Checking(cs);

            //редактирование данных текущего клиента
            EditProfile(cs);
          
        }

        private void LoadProfile(string cs, string phone)
        {
            try
            {
                //подключение к бд
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    //получение данных конкретного клиента из таблицы Клиенты
                    string sql = "SELECT * FROM clients WHERE ClientPhone=@phone;";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@phone", phone);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.ClientID = reader.GetInt32(0).ToString();
                                clientInfo.ClientSurname = reader.GetString(1);
                                clientInfo.ClientName = reader.GetString(2);
                                clientInfo.ClientMiddleName = reader.GetString(3);
                                clientInfo.ClientBirthDate = reader.GetDateTime(4);
                                clientInfo.ClientPhone = reader.GetString(5);
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

        private void Checking(string cs)
        {

            //проверка на пустоту
            if ((clientInfo.ClientSurname.Length == 0) || (clientInfo.ClientName.Length == 0) ||
              (clientInfo.ClientMiddleName.Length == 0) || (clientInfo.ClientPhone.Length == 0) || (clientInfo.ClientBirthDate.ToString().Length == 0))
            {
                errorMessage = "Необходимо заполнить все поля!";
                return;
            }

            //проверка даты на возможность преобразования из строки
            try
            {
                clientInfo.ClientBirthDate = Convert.ToDateTime(Request.Form["ClientBirthDate"]);
            }
            catch//если дата не заполнена
            {
                errorMessage = "Необходимо заполнить все поля!";
                return;
            }

            //проверка на ввод пустых строк
            if ((clientInfo.ClientSurname.Length == 0) || (clientInfo.ClientName.Length == 0) ||
              (clientInfo.ClientMiddleName.Length == 0) || (clientInfo.ClientPhone.Length == 0) || (clientInfo.ClientBirthDate.ToString().Length == 0))
            {
                errorMessage = "Необходимо заполнить все поля!";
                return;
            }

            //проверка на дату не позже сегодня
            if (clientInfo.ClientBirthDate >= DateTime.Now)
            {
                errorMessage = "Дата введена неверно!";
                return;
            }

            //проверка на длину номера телефона
            if (clientInfo.ClientPhone.Length != 11)
            {
                errorMessage = "Номер телефона введён неверно!";
                return;
            }

            //проверка на существование другой записи с эти номером телефона
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                string sql = "select * from clients WHERE clients.ClientPhone = @phone AND clients.ClientID != @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@phone", clientInfo.ClientPhone);
                    command.Parameters.AddWithValue("@id", clientInfo.ClientID);

                    DataTable table = new DataTable();
                    table.Clear();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;//выполнить команду
                    adapter.Fill(table);//заполнить полученными командой данными объект table

                    if (table.Rows.Count > 0)
                    {
                        errorMessage = "Клиент с таким номером телефона уже существует!";
                        return;
                    }
                }
            }
        }

        private void EditProfile(string cs)
        {
           
            //ОБНОВЛЕНИЕ ДАННЫХ В БД
            try
            {
                //обновление данных в clients
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    string sql = "UPDATE clients SET clients.ClientSurname = @surname, clients.ClientName = @name, clients.ClientMiddleName = @midname,  clients.ClientPhone = @phone, clients.ClientBirthDate = @date WHERE clients.ClientID = @id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@surname", clientInfo.ClientSurname);
                        command.Parameters.AddWithValue("@name", clientInfo.ClientName);
                        command.Parameters.AddWithValue("@midname", clientInfo.ClientMiddleName);
                        command.Parameters.AddWithValue("@phone", clientInfo.ClientPhone);
                        command.Parameters.AddWithValue("@date", clientInfo.ClientBirthDate);
                        command.Parameters.AddWithValue("@id", clientInfo.ClientID);
                        command.ExecuteNonQuery();//выполнение запроса
                    }
                }

                //обновление данных в AspNetUsers
                using (SqlConnection connection = new SqlConnection(cs))
                {
                    connection.Open();
                    string sql = "UPDATE AspNetUsers SET PhoneNumber = @phone, UserName = @phone, NormalizedUserName = @phone WHERE UserName = @username;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@phone", clientInfo.ClientPhone);
                        command.Parameters.AddWithValue("@username", HttpContext.User.Identity.Name);
                        command.ExecuteNonQuery();//выполнение запроса
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            successMessage = "Запись изменена.";
        }
    }
}

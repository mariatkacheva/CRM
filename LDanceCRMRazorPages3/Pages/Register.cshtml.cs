using LDanceCRMRazorPages3.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Threading.Tasks;


namespace LDanceCRMRazorPages3.Pages
{
    public class RegisterModel : PageModel
    {
        //объект - пользователь (содержит данные для авторизации)
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        //объект - клиент (содержит персональные данные)
        public ClientInfo clientInfo = new ClientInfo();

        private readonly ILogger<RegisterModel> _logger;
        private readonly IConfiguration _configuration;

        public RegisterModel(IConfiguration configuration, ILogger<RegisterModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [BindProperty]

        public Register Model { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {          
            if (ModelState.IsValid)
            {              
                clientInfo.ClientSurname = Model.ClientSurname;
                clientInfo.ClientName = Model.ClientName;
                clientInfo.ClientMiddleName = Model.ClientMiddleName;
                clientInfo.ClientPhone = Model.Phone;

                bool isCorrect = true;//верны ли вводимые персональные данные

                #region ПРОВЕРКИ НА ВВОД ПЕРСОНАЛЬНЫХ ДАННЫХ
                //проверка даты на возможность преобразования из строки
                try
                {
                    clientInfo.ClientBirthDate = Convert.ToDateTime(Model.ClientBirthDate);
                }
                catch//если дата не заполнена
                {
                    ModelState.AddModelError("", "Поле дата рождения является обязательным.");
                    isCorrect = false;
                }

                //проверка на дату не позже сегодня
                if (clientInfo.ClientBirthDate >= DateTime.Now)
                {
                    ModelState.AddModelError("", "Дата рождения задана неверно.");
                    isCorrect = false;
                }
               
                #endregion

                if(isCorrect == true)//если все персональные данные верны
                {
                    //ДОБАВЛЕНИЕ ДАННЫХ ДЛЯ АВТОРИЗАЦИИ
                    var user = new IdentityUser()
                    {
                        UserName = Model.Phone,
                        PhoneNumber = Model.Phone,
                    };

                    var result = await userManager.CreateAsync(user, Model.Password);
                    if (result.Succeeded)//если данные авторизации тоже верны, то данные пользователя добавились в AspNetUsers таблицу
                    {                       
                        await signInManager.SignInAsync(user, false);

                        //ДОБАВЛЕНИЕ ЗАПИСИ О КЛИЕНТЕ В ТАБЛИЦУ CLIENTS

                        //получение строки подключения из файла конфигурации
                        string cs = _configuration.GetConnectionString("AuthConnectionString");

                        try
                        {
                            //подключение к бд
                            using (SqlConnection connection = new SqlConnection(cs))
                            {
                                connection.Open();
                                //добавление нового клиента в таблицу Клиенты
                                string sql = "Insert into clients (ClientSurname, ClientName, ClientMiddleName, ClientBirthDate, ClientPhone) values (@surname, @name, @midname, @date, @phone)";
                                using (SqlCommand command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.AddWithValue("@surname", clientInfo.ClientSurname);
                                    command.Parameters.AddWithValue("@name", clientInfo.ClientName);
                                    command.Parameters.AddWithValue("@midname", clientInfo.ClientMiddleName);
                                    command.Parameters.AddWithValue("@date", clientInfo.ClientBirthDate);
                                    command.Parameters.AddWithValue("@phone", clientInfo.ClientPhone);
                                    command.ExecuteNonQuery();//выполнение запроса
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", ex.Message);
                        }
                      
                        return RedirectToPage("Index");
                        //return RedirectToPage("Index", new { UserName = clientInfo.ClientPhone });
                    }

                    #region СБОР ОШИБОК ВВОДА ДАННЫХ АВТОРИЗАЦИИ (НОМЕР ТЕЛЕФОНА, ПАРОЛЬ, ПОДТВЕРЖД. ПАРОЛЯ)
                    foreach (var error in result.Errors)
                    {
                        //ошибки ввода номера телефона
                        if (error.Code == "DuplicateUserName")
                        {
                            ModelState.AddModelError("", "Этот номер телефона уже используется.");
                        }
                        else if (error.Code == "PhoneNumberTooShort")
                        {
                            ModelState.AddModelError("", "Номер телефона должен содержать 11 цифр.");
                        }
                        else if (error.Code == "PhoneNumberTooLong")
                        {
                            ModelState.AddModelError("", "Этот номер телефона слишком длинный.");
                        }
                        //ошибки ввода паролей                    
                        else if (error.Code == "PasswordTooShort")
                        {
                            ModelState.AddModelError("", "Пароль должен содержать как минимум 6 символов.");
                        }
                        else if (error.Code == "PasswordTooLong")
                        {
                            ModelState.AddModelError("", "Этот пароль слишком длинный.");
                        }
                        else if (error.Code == "PasswordRequiresDigit")
                        {
                            ModelState.AddModelError("", "Пароль должен содержать хотя бы одну цифру.");
                        }
                        else if (error.Code == "PasswordRequiresLower")
                        {
                            ModelState.AddModelError("", "Пароль должен содержать хотя бы одну строчную букву.");
                        }
                        else if (error.Code == "PasswordRequiresUpper")
                        {
                            ModelState.AddModelError("", "Пароль должен содержать хотя бы одну прописную букву.");
                        }
                        else if (error.Code == "PasswordRequiresNonAlphanumeric")
                        {
                            ModelState.AddModelError("", "Пароль должен содержать хотя бы один специальный символ (!@#$%^&).");
                        }
                        //ошибки подтверждения пароля
                        else if (error.Code == "PasswordMismatch")
                        {
                            ModelState.AddModelError("", "Пароли не совпадают.");
                        }
                        //все остальные исключения
                        else
                        {
                            ModelState.AddModelError("", "Произошла ошибка при создании аккаунта.");
                        }
                    }
                    #endregion
                }

            }
            return Page();
        }
    }

    //класс для хранения данных конкретного клиента
    public class ClientInfo
    {
        public String ClientID;
        public String ClientSurname;
        public String ClientName;
        public String ClientMiddleName;
        public DateTime ClientBirthDate;
        public String ClientPhone;
    }
}

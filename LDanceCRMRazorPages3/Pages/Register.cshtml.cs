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
        //������ - ������������ (�������� ������ ��� �����������)
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        //������ - ������ (�������� ������������ ������)
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

                bool isCorrect = true;//����� �� �������� ������������ ������

                #region �������� �� ���� ������������ ������
                //�������� ���� �� ����������� �������������� �� ������
                try
                {
                    clientInfo.ClientBirthDate = Convert.ToDateTime(Model.ClientBirthDate);
                }
                catch//���� ���� �� ���������
                {
                    ModelState.AddModelError("", "���� ���� �������� �������� ������������.");
                    isCorrect = false;
                }

                //�������� �� ���� �� ����� �������
                if (clientInfo.ClientBirthDate >= DateTime.Now)
                {
                    ModelState.AddModelError("", "���� �������� ������ �������.");
                    isCorrect = false;
                }
               
                #endregion

                if(isCorrect == true)//���� ��� ������������ ������ �����
                {
                    //���������� ������ ��� �����������
                    var user = new IdentityUser()
                    {
                        UserName = Model.Phone,
                        PhoneNumber = Model.Phone,
                    };

                    var result = await userManager.CreateAsync(user, Model.Password);
                    if (result.Succeeded)//���� ������ ����������� ���� �����, �� ������ ������������ ���������� � AspNetUsers �������
                    {                       
                        await signInManager.SignInAsync(user, false);

                        //���������� ������ � ������� � ������� CLIENTS

                        //��������� ������ ����������� �� ����� ������������
                        string cs = _configuration.GetConnectionString("AuthConnectionString");

                        try
                        {
                            //����������� � ��
                            using (SqlConnection connection = new SqlConnection(cs))
                            {
                                connection.Open();
                                //���������� ������ ������� � ������� �������
                                string sql = "Insert into clients (ClientSurname, ClientName, ClientMiddleName, ClientBirthDate, ClientPhone) values (@surname, @name, @midname, @date, @phone)";
                                using (SqlCommand command = new SqlCommand(sql, connection))
                                {
                                    command.Parameters.AddWithValue("@surname", clientInfo.ClientSurname);
                                    command.Parameters.AddWithValue("@name", clientInfo.ClientName);
                                    command.Parameters.AddWithValue("@midname", clientInfo.ClientMiddleName);
                                    command.Parameters.AddWithValue("@date", clientInfo.ClientBirthDate);
                                    command.Parameters.AddWithValue("@phone", clientInfo.ClientPhone);
                                    command.ExecuteNonQuery();//���������� �������
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

                    #region ���� ������ ����� ������ ����������� (����� ��������, ������, ���������. ������)
                    foreach (var error in result.Errors)
                    {
                        //������ ����� ������ ��������
                        if (error.Code == "DuplicateUserName")
                        {
                            ModelState.AddModelError("", "���� ����� �������� ��� ������������.");
                        }
                        else if (error.Code == "PhoneNumberTooShort")
                        {
                            ModelState.AddModelError("", "����� �������� ������ ��������� 11 ����.");
                        }
                        else if (error.Code == "PhoneNumberTooLong")
                        {
                            ModelState.AddModelError("", "���� ����� �������� ������� �������.");
                        }
                        //������ ����� �������                    
                        else if (error.Code == "PasswordTooShort")
                        {
                            ModelState.AddModelError("", "������ ������ ��������� ��� ������� 6 ��������.");
                        }
                        else if (error.Code == "PasswordTooLong")
                        {
                            ModelState.AddModelError("", "���� ������ ������� �������.");
                        }
                        else if (error.Code == "PasswordRequiresDigit")
                        {
                            ModelState.AddModelError("", "������ ������ ��������� ���� �� ���� �����.");
                        }
                        else if (error.Code == "PasswordRequiresLower")
                        {
                            ModelState.AddModelError("", "������ ������ ��������� ���� �� ���� �������� �����.");
                        }
                        else if (error.Code == "PasswordRequiresUpper")
                        {
                            ModelState.AddModelError("", "������ ������ ��������� ���� �� ���� ��������� �����.");
                        }
                        else if (error.Code == "PasswordRequiresNonAlphanumeric")
                        {
                            ModelState.AddModelError("", "������ ������ ��������� ���� �� ���� ����������� ������ (!@#$%^&).");
                        }
                        //������ ������������� ������
                        else if (error.Code == "PasswordMismatch")
                        {
                            ModelState.AddModelError("", "������ �� ���������.");
                        }
                        //��� ��������� ����������
                        else
                        {
                            ModelState.AddModelError("", "��������� ������ ��� �������� ��������.");
                        }
                    }
                    #endregion
                }

            }
            return Page();
        }
    }

    //����� ��� �������� ������ ����������� �������
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

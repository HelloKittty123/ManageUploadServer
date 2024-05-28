namespace ManageServer.Models
{
    public class AccountModel : AccountResponseModel
    {
        public string Password { get; set; }
    }

    public class AccountResponseModel : UserModel
    {
        public Guid? Id { get; set; }
    }


    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class UpdatePasswordModel
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
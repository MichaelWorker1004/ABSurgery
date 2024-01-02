namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public class ForgotUsernameCommandDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

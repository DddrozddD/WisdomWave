using System.Runtime.CompilerServices;

namespace ASP_Resume.Models
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPass { get; set; }
       
    }
}

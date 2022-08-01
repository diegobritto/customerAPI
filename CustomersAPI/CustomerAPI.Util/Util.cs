using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Util
{
    public class Util
    {
        
        public static bool IsValidEmail(string pEmail)
        {
            var email = new EmailAddressAttribute();            
            return email.IsValid(pEmail); ;
        }
        
    }
}
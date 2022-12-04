using System.ComponentModel.DataAnnotations;

namespace CAFFEINE.Data
{
    public class UserData
    {
       public string UserName { get; set; }
       public string PhoneNumber { get; set; }
       public bool isAdmin { get; set; }
    }
}

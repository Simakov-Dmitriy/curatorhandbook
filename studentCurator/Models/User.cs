using Microsoft.AspNetCore.Identity;

namespace studentCurator.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Father { get; set; }
        public string Cathedra { get; set; }
        public string PersonalData { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace FloraYFaunaAPI.Enums
{
    public enum UserRol : int
    {
        [Display(Name = "Super Admin")]
        SuperAdmin = 10,
        Admin = 20,
        [Display(Name = "Web Master")]
        WebMaster = 30
    }
}

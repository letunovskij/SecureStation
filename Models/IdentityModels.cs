using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DivisionWebGlobal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // TODO добавить генерацию данных пользователя для Личного Кабинета. 
        // в аккаунт входят данные о DV-HEAD, email владельца, пароль для входа (хеш)
        // отправить письмо владельцу после создания конфигурации DV-HEAD OMEGA
        // записать данные аккаунта в другую БД
        // неучтеные расходы. 
        ////public virtual PersonalCabinetUserInfo PersonalCabinetUserInfo { get; set; } 
        // на данном этапе выделить работу - данные владельца системы автоматизации / систему автоматизации связать с аккаунтом конретного пользователя.
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }
    
    // TODO: класс для хранения данных о пользователе Личного Кабинета
    // добавить реализацию
    ////
    //[Table("cabinet_user_info")]
    //public class PersonalCabinetUserInfo
    //{
    //    [Key]
    //    [Column("cabinetid")]
    //    public int Id { get; set; }

    //    [Column("email")]
    //    [EmailAddress]
    //    [Display(Name = "Почта")]
    //    [Required]
    //    public string Email { get; set; } 

    //    [Column("explicit_password")]
    //    [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Пароль")]
    //    [Required]
    //    public string Password { get; set; }

    //    [Display(Name = "ФИО владельца")]
    //    [StringLength(200, ErrorMessage = "Значение не должно превышать 200 символов")]
    //    public string FullName { get; set; }
    //} 

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
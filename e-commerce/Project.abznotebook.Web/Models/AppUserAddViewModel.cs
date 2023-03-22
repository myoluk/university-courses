using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.abznotebook.Web.Models
{
    public class AppUserAddViewModel
    {
        [Display(Name = "Kullanıcı Adı"), Required(ErrorMessage = "Bu alan gereklidir.")]
        public string UserName { get; set; }
        
        [Display(Name = "Ad"), Required(ErrorMessage = "Bu alan gereklidir.")]
        public string Name { get; set; }

        [Display(Name = "Soyad"), Required(ErrorMessage = "Bu alan gereklidir.")]
        public string Surname { get; set; }

        [Display(Name = "Şifre"), Required(ErrorMessage = "Bu alan gereklidir."), DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor."), Display(Name = "Şifre Tekrar"), Required(ErrorMessage = "Bu alan gereklidir."), DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        
        [EmailAddress(ErrorMessage = "E-Mail adresini doğru formatta giriniz."), Required(ErrorMessage = "Bu alan gereklidir."), Display(Name = "E-Mail")]
        public string Email { get; set; }
    }
}

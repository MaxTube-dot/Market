using System.ComponentModel.DataAnnotations;

namespace Microsoft.eShopWeb.Web.ViewModels.Account;

public class LoginWith2faViewModel
{
    [Required]
    [StringLength(7, ErrorMessage = "Длина {0} должна быть не менее {2} и не более {1} символов.", MinimumLength = 6)]
    [DataType(DataType.Text)]
    [Display(Name = "Код аутентификатора")]
    public string? TwoFactorCode { get; set; }

    [Display(Name = "Запомни эту компьютер")]
    public bool RememberMachine { get; set; }

    public bool RememberMe { get; set; }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.eShopWeb.Web.ViewModels.Manage;

public class EnableAuthenticatorViewModel
{
    [Required]
    [StringLength(7, ErrorMessage = "Длина {0} должна быть не менее {2} и не более {1} символов.", MinimumLength = 6)]
    [DataType(DataType.Text)]
    [Display(Name = "Проверочный код")]
    public string? Code { get; set; }

    [BindNever]
    public string? SharedKey { get; set; }

    [BindNever]
    public string? AuthenticatorUri { get; set; }
}

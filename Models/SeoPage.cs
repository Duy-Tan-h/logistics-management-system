using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebVanChuyenLogicstic.Models;

public class SeoPage
{
    public int Id { get; set; }

    [ValidateNever][StringLength(100)] public string Route { get; set; } = string.Empty;      // "Home/Index" = controller/action
    [ValidateNever][StringLength(100)] public string PageName { get; set; } = string.Empty;   // tên hiển thị cho admin

    [StringLength(70, ErrorMessage = "Meta Title tối đa 70 ký tự.")]
    public string? MetaTitle { get; set; }

    [StringLength(300, ErrorMessage = "Meta Description tối đa 300 ký tự (nên dưới 160).")]
    public string? MetaDescription { get; set; }

    [StringLength(300, ErrorMessage = "Keywords tối đa 300 ký tự.")]
    public string? Keywords { get; set; }
}
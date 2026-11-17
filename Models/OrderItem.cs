using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebVanChuyenLogicstic.Data;

namespace WebVanChuyenLogicstic.Models;

// Một dòng hàng trong đơn (ER: CHI_TIET_HANG_HOA)
public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public int? CargoTypeId { get; set; }
    public CargoType? CargoType { get; set; }

    [StringLength(100)] public string CargoTypeName { get; set; } = string.Empty; // chụp tên lúc đặt, xóa loại hàng vẫn còn lịch sử
    public int Quantity { get; set; } = 1;
    public decimal? WeightKg { get; set; }          // khối lượng của cả dòng
    public bool IsFragile { get; set; }             // hàng đặc biệt / dễ vỡ
    [StringLength(300)] public string? Note { get; set; }
}

// Dữ liệu form (khách và admin dùng chung)
public class OrderItemInput
{
    public const int MaxItems = 20;

    [Required(ErrorMessage = "Vui lòng chọn loại hàng.")]
    public int? CargoTypeId { get; set; }

    [Range(1, 100000, ErrorMessage = "Số lượng không hợp lệ.")]
    public int Quantity { get; set; } = 1;

    [Range(0.1, 1000000, ErrorMessage = "Khối lượng không hợp lệ.")]
    public decimal? WeightKg { get; set; }

    public bool IsFragile { get; set; }

    [StringLength(300, ErrorMessage = "Lưu ý tối đa 300 ký tự.")]
    public string? Note { get; set; }

    public static Dictionary<int, CargoType> Validate(List<OrderItemInput> items, AppDbContext db, ModelStateDictionary ms)
    {
        if (items.Count == 0) ms.AddModelError("Items", "Vui lòng thêm ít nhất một dòng hàng.");
        if (items.Count > MaxItems) ms.AddModelError("Items", $"Mỗi đơn tối đa {MaxItems} dòng hàng.");

        var ids = items.Where(i => i.CargoTypeId != null).Select(i => i.CargoTypeId!.Value).Distinct().ToList();
        var types = db.CargoTypes.Where(c => ids.Contains(c.Id)).ToDictionary(c => c.Id);
        for (var k = 0; k < items.Count; k++)
            if (items[k].CargoTypeId is int id && !types.ContainsKey(id))
                ms.AddModelError($"Items[{k}].CargoTypeId", "Loại hàng không hợp lệ.");
        return types;
    }

    public static List<OrderItem> ToItems(IEnumerable<OrderItemInput> inputs, IDictionary<int, CargoType> types) =>
        inputs.Select(i => new OrderItem
        {
            CargoTypeId = i.CargoTypeId,
            CargoTypeName = types[i.CargoTypeId!.Value].Name,
            Quantity = i.Quantity,
            WeightKg = i.WeightKg,
            IsFragile = i.IsFragile,
            Note = string.IsNullOrWhiteSpace(i.Note) ? null : i.Note.Trim()
        }).ToList();
}
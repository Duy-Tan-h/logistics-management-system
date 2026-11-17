using WebVanChuyenLogicstic.Models;

namespace WebVanChuyenLogicstic.Data;

public class InMemoryContentRepository : IContentRepository
{
    private readonly List<TransportService> _services =
    [
        new() { Id = 1, Name = "Giao hàng nội thành", Summary = "Nhận và giao hàng trong nội thành, phù hợp chứng từ và kiện hàng cần gấp.", Icon = "bi-lightning-charge", DeliveryTime = "1 - 2 ngày", PriceNote = "Báo giá theo chuyến" },
        new() { Id = 2, Name = "Vận chuyển hàng ghép liên tỉnh", Summary = "Vận chuyển hàng hóa các tỉnh với lịch trình ổn định.", Icon = "bi-truck", DeliveryTime = "2 - 5 ngày", PriceNote = "Theo khối lượng/thể tích" },
        //new() { Id = 3, Name = "Vận chuyển Bắc - Nam", Summary = "Kết nối kho hàng hai miền, theo dõi hành trình và hỗ trợ giao nhận tận nơi.", Icon = "bi-map", DeliveryTime = "3 - 5 ngày", PriceNote = "Từ 1.500đ/kg" },
        new() { Id = 4, Name = "Cho thuê xe tải nguyên chuyến", Summary = "Xe tải đa tải trọng từ 500kg đến 15 tấn, có tài xế và hỗ trợ bốc xếp theo yêu cầu.", Icon = "bi-box-seam", DeliveryTime = "Theo lịch đặt", PriceNote = "Theo chuyến/quãng đường", IsExclusive = true },
        //new() { Id = 5, Name = "Kho bãi & phân phối", Summary = "Lưu kho, kiểm đếm, đóng gói và phân phối hàng hóa đến nhiều điểm giao.", Icon = "bi-buildings", DeliveryTime = "Theo hợp đồng", PriceNote = "Theo diện tích sử dụng" },
        //new() { Id = 6, Name = "Giao hàng thu hộ COD", Summary = "Giao tận tay người nhận, đối soát minh bạch và chuyển tiền thu hộ nhanh chóng.", Icon = "bi-cash-coin", DeliveryTime = "1 - 3 ngày", PriceNote = "Phí COD cạnh tranh" }
    ];

    private readonly List<ProcessStep> _steps =
    [
        new() { Number = 1, Title = "Tạo yêu cầu", Description = "Khách hàng cung cấp thông tin người gửi, người nhận và loại hàng hóa.", Icon = "bi-pencil-square" },
        new() { Number = 2, Title = "Tiếp nhận hàng", Description = "Nhân viên đến lấy hàng, kiểm tra, đóng gói và xác nhận vận đơn.", Icon = "bi-box-seam" },
        new() { Number = 3, Title = "Phân loại", Description = "Hàng được phân tuyến tại kho và cập nhật trạng thái trên hệ thống.", Icon = "bi-diagram-3" },
        new() { Number = 4, Title = "Vận chuyển", Description = "Phương tiện phù hợp đưa hàng đến bưu cục hoặc điểm giao gần nhất.", Icon = "bi-truck" },
        new() { Number = 5, Title = "Giao & xác nhận", Description = "Giao đúng người nhận, xác nhận hoàn tất và đối soát COD nếu có.", Icon = "bi-check2-circle" }
    ];

    public IReadOnlyList<TransportService> GetServices() => _services;
    public TransportService? GetServiceById(int id) => _services.FirstOrDefault(x => x.Id == id);
    public IReadOnlyList<ProcessStep> GetProcessSteps() => _steps;
}

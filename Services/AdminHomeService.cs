using Microsoft.EntityFrameworkCore;
using WebVanChuyenLogicstic.Data;
using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Models.Admin;

namespace WebVanChuyenLogicstic.Services;

public class AdminHomeService(AppDbContext db) : IAdminHomeService
{
    // Section nào có mục con hiển thị ra trang chủ
    private static readonly HashSet<string> ItemSectionTypes =
        [HomeSectionTypes.Hero, HomeSectionTypes.WhyChooseUs, HomeSectionTypes.Projects, HomeSectionTypes.Contact];

    public static string LabelOf(string type) => type switch
    {
        HomeSectionTypes.Hero => "Banner đầu trang (Hero)",
        HomeSectionTypes.Tracking => "Ô tra cứu đơn hàng",
        HomeSectionTypes.ServiceIntro => "Giới thiệu dịch vụ",
        HomeSectionTypes.WhyChooseUs => "Vì sao chọn chúng tôi",
        HomeSectionTypes.Projects => "Dự án tiêu biểu",
        HomeSectionTypes.Process => "Quy trình",
        HomeSectionTypes.News => "Tin tức",
        HomeSectionTypes.Contact => "Form liên hệ",
        HomeSectionTypes.Cta => "Kêu gọi hành động (CTA)",
        HomeSectionTypes.Banner => "Banner quảng cáo",
        HomeSectionTypes.Partners => "Đối tác đồng hành",
        _ => type
    };

    // ---------- Section ----------
    public List<HomeSection> GetSections() =>
        db.HomeSections.OrderBy(s => s.DisplayOrder).ThenBy(s => s.Id).ToList(); //lấy danh sách section theo đúng thứ tự hiển thị.

    public HomeSectionEditViewModel? GetSectionForEdit(int id) //nạp dữ liệu vào form sửa.
    {
        var s = db.HomeSections.AsNoTracking().Include(x => x.Items).FirstOrDefault(x => x.Id == id);
        if (s is null) return null;

        var vm = new HomeSectionEditViewModel
        {
            Id = s.Id,
            SectionType = s.SectionType,
            Kicker = s.Kicker,
            Title = s.Title,
            Subtitle = s.Subtitle,
            Body = s.Body,
            ButtonText = s.ButtonText,
            ButtonUrl = s.ButtonUrl,
            SecondaryButtonText = s.SecondaryButtonText,
            SecondaryButtonUrl = s.SecondaryButtonUrl
        };
        FillDisplayInfo(vm, s);
        return vm;
    }

    public void FillDisplayInfo(HomeSectionEditViewModel vm, HomeSection? section = null)
    {
        section ??= db.HomeSections.AsNoTracking().Include(x => x.Items).First(x => x.Id == vm.Id);
        vm.SectionLabel = LabelOf(section.SectionType);
        vm.SupportsItems = ItemSectionTypes.Contains(section.SectionType);
        vm.Items = section.Items.OrderBy(i => i.DisplayOrder).ThenBy(i => i.Id).ToList();
    }

    public bool UpdateSection(HomeSectionEditViewModel model) //lưu nội dung sửa.
    {
        var s = db.HomeSections.Find(model.Id);
        if (s is null) return false;
        s.Kicker = Clean(model.Kicker); s.Title = model.Title.Trim(); s.Subtitle = Clean(model.Subtitle);
        s.Body = Clean(model.Body); s.ButtonText = Clean(model.ButtonText); s.ButtonUrl = Clean(model.ButtonUrl);
        s.SecondaryButtonText = Clean(model.SecondaryButtonText); s.SecondaryButtonUrl = Clean(model.SecondaryButtonUrl);
        db.SaveChanges();
        return true;
    }

    public bool ToggleSection(int id) //ẩn/hiện section
    {
        var s = db.HomeSections.Find(id);
        if (s is null) return false;
        s.IsVisible = !s.IsVisible;
        db.SaveChanges();
        return true;
    }

    public void ReorderSections(List<(int Id, int Order)> items) //lưu thứ tự mới sau khi admin kéo thả.
    {
        var sortedIds = items.OrderBy(x => x.Order).Select(x => x.Id).ToList();
        var sections = db.HomeSections.Where(s => sortedIds.Contains(s.Id)).ToList();
        // Gửi đủ toàn bộ section thì đánh lại 1..n; chỉ gửi một trang thì dùng lại chính các số thứ tự của trang đó
        var slots = sortedIds.Count == db.HomeSections.Count()
            ? Enumerable.Range(1, sortedIds.Count).ToList()
            : sections.Select(s => s.DisplayOrder).OrderBy(x => x).ToList();
        for (var i = 0; i < sortedIds.Count; i++)
        {
            var section = sections.First(s => s.Id == sortedIds[i]);
            section.DisplayOrder = i + 1;
        }
        db.SaveChanges();
    }

    public Dictionary<string, string> GetCreatableTypes() => DuplicableSectionTypes.Options;

    public int CreateSection(HomeSectionCreateViewModel model)
    {
        if (!DuplicableSectionTypes.Options.ContainsKey(model.SectionType))
            throw new InvalidOperationException("Khuôn giao diện không hợp lệ.");

        var nextOrder = (db.HomeSections.Select(s => (int?)s.DisplayOrder).Max() ?? 0) + 1;
        var section = new HomeSection
        {
            SectionType = model.SectionType,
            Title = model.Title.Trim(),
            Subtitle = Clean(model.Subtitle),
            Body = Clean(model.Body),
            ButtonText = Clean(model.ButtonText),
            ButtonUrl = Clean(model.ButtonUrl),
            DisplayOrder = nextOrder,
            IsVisible = true,
            IsSystem = false
        };
        db.HomeSections.Add(section);
        db.SaveChanges();
        return section.Id;
    }

    public bool DeleteSection(int id)
    {
        var s = db.HomeSections.Include(x => x.Items).FirstOrDefault(x => x.Id == id);
        if (s is null || s.IsSystem) return false;
        db.HomeSectionItems.RemoveRange(s.Items);
        db.HomeSections.Remove(s);
        db.SaveChanges();
        return true;
    }

    // Mục con
    public HomeSectionItemEditViewModel? GetNewItem(int sectionId) //chuẩn bị form thêm mục con.
    {
        var s = db.HomeSections.AsNoTracking().FirstOrDefault(x => x.Id == sectionId);
        if (s is null || !ItemSectionTypes.Contains(s.SectionType)) return null;
        return new HomeSectionItemEditViewModel
        {
            SectionId = s.Id,
            SectionType = s.SectionType,
            SectionLabel = LabelOf(s.SectionType)
        };
    }

    public HomeSectionItemEditViewModel? GetItemForEdit(int id) //chuẩn bị form sửa mục con.
    {
        var i = db.HomeSectionItems.AsNoTracking().Include(x => x.Section).FirstOrDefault(x => x.Id == id);
        if (i?.Section is null) return null;
        return new HomeSectionItemEditViewModel
        {
            Id = i.Id,
            SectionId = i.SectionId,
            SectionType = i.Section.SectionType,
            SectionLabel = LabelOf(i.Section.SectionType),
            Title = i.Title,
            Description = i.Description,
            Icon = i.Icon,
            ImageUrl = i.ImageUrl,
            LinkUrl = i.LinkUrl,
            Value = i.Value,
            IsVisible = i.IsVisible
        };
    }

    public void FillItemDisplayInfo(HomeSectionItemEditViewModel vm)
    {
        var s = db.HomeSections.AsNoTracking().First(x => x.Id == vm.SectionId);
        vm.SectionType = s.SectionType;
        vm.SectionLabel = LabelOf(s.SectionType);
    }

    public int? SaveItem(HomeSectionItemEditViewModel model)
    {
        var section = db.HomeSections.AsNoTracking().FirstOrDefault(x => x.Id == model.SectionId);
        if (section is null || !ItemSectionTypes.Contains(section.SectionType)) return null;

        HomeSectionItem item;
        if (model.Id == 0)
        {
            var nextOrder = db.HomeSectionItems.Where(i => i.SectionId == model.SectionId)
                                .Select(i => (int?)i.DisplayOrder).Max() ?? 0;
            item = new HomeSectionItem { SectionId = model.SectionId, DisplayOrder = nextOrder + 1 };
            db.HomeSectionItems.Add(item);
        }
        else
        {
            var found = db.HomeSectionItems.FirstOrDefault(i => i.Id == model.Id && i.SectionId == model.SectionId);
            if (found is null) return null;
            item = found;
        }

        item.Title = model.Title.Trim();
        item.Description = Clean(model.Description);
        item.Icon = Clean(model.Icon);
        item.ImageUrl = Clean(model.ImageUrl);
        item.LinkUrl = Clean(model.LinkUrl);
        item.Value = Clean(model.Value);
        item.IsVisible = model.IsVisible;
        db.SaveChanges();
        return model.SectionId;
    }

    public int? ToggleItem(int id)
    {
        var i = db.HomeSectionItems.Find(id);
        if (i is null) return null;
        i.IsVisible = !i.IsVisible;
        db.SaveChanges();
        return i.SectionId;
    }

    public int? MoveItem(int id, int direction)
    {
        var current = db.HomeSectionItems.AsNoTracking().FirstOrDefault(i => i.Id == id);
        if (current is null) return null;

        var list = db.HomeSectionItems.Where(i => i.SectionId == current.SectionId)
                     .OrderBy(i => i.DisplayOrder).ThenBy(i => i.Id).ToList();
        var idx = list.FindIndex(i => i.Id == id);
        for (var n = 0; n < list.Count; n++) list[n].DisplayOrder = n + 1;

        var target = idx + (direction < 0 ? -1 : 1);
        if (target >= 0 && target < list.Count)
            (list[idx].DisplayOrder, list[target].DisplayOrder) = (list[target].DisplayOrder, list[idx].DisplayOrder);

        db.SaveChanges();
        return current.SectionId;
    }

    public int? DeleteItem(int id)
    {
        var i = db.HomeSectionItems.Find(id);
        if (i is null) return null;
        var sectionId = i.SectionId;
        db.HomeSectionItems.Remove(i);
        db.SaveChanges();

        var rest = db.HomeSectionItems.Where(x => x.SectionId == sectionId)
                     .OrderBy(x => x.DisplayOrder).ThenBy(x => x.Id).ToList();
        for (var n = 0; n < rest.Count; n++) rest[n].DisplayOrder = n + 1;
        db.SaveChanges();
        return sectionId;
    }

    private static string? Clean(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
}
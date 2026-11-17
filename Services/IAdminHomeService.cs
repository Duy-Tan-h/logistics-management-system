using WebVanChuyenLogicstic.Models;
using WebVanChuyenLogicstic.Models.Admin;

namespace WebVanChuyenLogicstic.Services;

public interface IAdminHomeService
{
    // Section
    List<HomeSection> GetSections();
    HomeSectionEditViewModel? GetSectionForEdit(int id);
    void FillDisplayInfo(HomeSectionEditViewModel vm, HomeSection? section = null);
    bool UpdateSection(HomeSectionEditViewModel model);
    bool ToggleSection(int id);
    void ReorderSections(List<(int Id, int Order)> items);
    Dictionary<string, string> GetCreatableTypes();
    int CreateSection(HomeSectionCreateViewModel model);
    bool DeleteSection(int id);

    // Mục con 
    HomeSectionItemEditViewModel? GetNewItem(int sectionId);
    HomeSectionItemEditViewModel? GetItemForEdit(int id);
    void FillItemDisplayInfo(HomeSectionItemEditViewModel vm);
    int? SaveItem(HomeSectionItemEditViewModel model);
    int? ToggleItem(int id);
    int? MoveItem(int id, int direction);
    int? DeleteItem(int id);
}
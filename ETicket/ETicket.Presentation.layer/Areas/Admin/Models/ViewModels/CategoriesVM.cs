using ETicket.Data.Acess.layer.Models;

namespace ETicket.Presentation.layer.Areas.Admin.Models.ViewModels
{
    public class CategoriesVM
    {
        public List<Category> Categories { get; set; }

        public int TotalCategoryCount { get; set; }

        public int CurrentPageIndex { get; set; }
    }
}

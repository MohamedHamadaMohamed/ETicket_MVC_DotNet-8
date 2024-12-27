using System.ComponentModel.DataAnnotations;

namespace ETicket.Data.Acess.layer.Models
{
    public class Category
    {
        public int Id { get; set; }
     
        public string Name { get; set; } = string.Empty;

        public List<Movie> Movies { get; set; } = null!;
    }
}

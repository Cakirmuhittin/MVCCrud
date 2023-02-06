using System.ComponentModel.DataAnnotations;

namespace MVC_060223.Classes
{
    public class Tur
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Ad { get; set; } = null!;
        public List<Film> Filmler { get; set; }= new();

    }
}

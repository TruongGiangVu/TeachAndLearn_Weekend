using System.ComponentModel.DataAnnotations;

namespace CuoiTuan3Api.Models
{
    public class Phim
    {
        public int Id { get; set; }
        public string Name { get; set; } =string.Empty;
        public PhimType Type { get; set; } = PhimType.None;
    }

    public enum PhimType 
    { 
        None = 0,

        [Display(Name = "Hài hước")]
        Commedy = 1,

        [Display(Name = "Hành động")]
        Action = 2,

    }
}

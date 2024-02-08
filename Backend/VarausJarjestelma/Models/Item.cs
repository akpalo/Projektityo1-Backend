using System.ComponentModel.DataAnnotations;

namespace VarausJarjestelma.Models
{
    public class Item
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String? Description { get; set; }
        public virtual User? Owner { get; set; }
    
        public virtual List<Image>? Images { get; set; }
        public long accessCount { get; set; }//sisäinen kenttä - ei lähetetä verkon yli
    }

    public class ItemDTO
    {
        public long Id { get; set; }
        [MinLength(4)]
        [MaxLength(50)]
        public String Name { get; set; }
        public String? Description { get; set; }
        
        public long Owner { get; set; }
        public virtual List<ImageDTO>? Images { get; set; }
    }
}

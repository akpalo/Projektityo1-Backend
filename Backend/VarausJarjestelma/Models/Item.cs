namespace VarausJarjestelma.Models
{
    public class Item
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public User Owner { get; set; }
    }
}

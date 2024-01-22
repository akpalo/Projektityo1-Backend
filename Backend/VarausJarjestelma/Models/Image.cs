namespace VarausJarjestelma.Models
{
    public class Image
    {
        public long Id { get; set; }
        public String Description { get; set; }
        public String Url { get; set; }
        public Item Target { get; set; }
    }
}

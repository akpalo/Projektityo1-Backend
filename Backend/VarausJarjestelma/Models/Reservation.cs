namespace VarausJarjestelma.Models
{
    public class Reservation
    {
        public long Id { get; set; }
        public virtual User? Owner { get; set; }
        public virtual Item? Target { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

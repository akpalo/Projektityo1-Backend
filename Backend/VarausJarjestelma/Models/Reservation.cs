namespace VarausJarjestelma.Models
{
    public class Reservation
    {
        public long Id { get; set; }
        public User Owner { get; set; }
        public Item Target { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

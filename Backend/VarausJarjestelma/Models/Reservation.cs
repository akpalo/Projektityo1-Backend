﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace VarausJarjestelma.Models
{
    public class Reservation
    {
        public long Id { get; set; }
        public virtual User Owner { get; set; }
        public virtual Item Target { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class ReservationDTO
    {
        public long Id { get; set; }
        [Required]
        public long Target { get; set; }
        [Required]
        public long Owner { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}

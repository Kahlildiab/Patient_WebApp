using System.ComponentModel.DataAnnotations;

namespace WebApp_Patient.Models
{
    public class PatientReference
    {
        [Key]
        public int ReferenceId { get; set; }

        public int PatientId { get; set; }

        [StringLength(150)]
        public string ReferenceName { get; set; }

        [StringLength(20)]
        public string Telephone { get; set; }

        public string RelationShip { get; set; }
        [StringLength(300)]
        public string Address { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }

        public int NationalNo { get; set; }
        public virtual Patient Patient { get; set; }

    }

}
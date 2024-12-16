using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SportsClubManagement_SPA_Muster_details.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentID { get; set; }

        [Required]
        public int MemberID { get; set; }

        [Required]
        public int ActivityID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }= DateTime.Now;

        // Navigation properties
        [ForeignKey("MemberID")]
        public virtual Member? Member { get; set; }

        [ForeignKey("ActivityID")]
        public virtual Activity? Activity { get; set; }
    }
}

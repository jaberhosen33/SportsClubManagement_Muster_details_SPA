using System.ComponentModel.DataAnnotations;

namespace SportsClubManagement_SPA_Muster_details.Models.ViewModel
{
    public class MemberVm
    {
        public  MemberVm()
        { 
            this.ActivityList=new List<Activity>();

        }
        public int MemberID { get; set; }
        
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Email { get; set; } = string.Empty;
      
        public DateTime DOB { get; set; }

        public bool? MorningShift { get; set; }
        public string? Picture { get; set; }
        public IFormFile PicturePath { get; set; }

        public IList<Activity> ActivityList { get; set; }


    }
}

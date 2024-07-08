namespace ASM2_KSTH.ViewModels
{
    public class AttendanceStatusViewModel
    {
        public int ? ClassId { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
        public string AttendanceStatus { get; set; }
        public string Reason { get; set; }
        public DateTime? AttendanceDate { get; set; }

        public string Numses { get; set; }
    }
}

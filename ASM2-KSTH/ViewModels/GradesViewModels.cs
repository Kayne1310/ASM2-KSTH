using ASM2_KSTH.Models;
using System.Collections.Generic;

namespace ASM2_KSTH.ViewModels
{
    public class GradesViewModels
    {
        public int SelectedCourseId { get; set; }
        public List<Class> Classes { get; set; }  // Sử dụng List<Class> để lưu trữ danh sách các lớp học
    }
}

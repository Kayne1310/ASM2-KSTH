namespace ASM2_KSTH.Models
{
    public class Room
    {
        public int RoomId { get; set; } // Primary Key
        public string? RoomNumber { get; set; }

        public ICollection<Class>? Classes { get; set; }
    }
}

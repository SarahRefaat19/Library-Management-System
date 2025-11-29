namespace LibrarySystem.BusnissLogic.Dtos.MemberDtos
{
    public class MemberReadDto
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "";
        public decimal FineBalance { get; set; }

    }
}

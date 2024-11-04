namespace Application.Dto
{
    public class UserDto
    {
        public int? Id { get; set; } = 0; //TODO: ajustar no front
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Gender { get; set; }
        public required string Email { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

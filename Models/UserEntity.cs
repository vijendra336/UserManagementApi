namespace UserManagementApi.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class PaginatedUsersResponse
    {
        public List<UserEntity> Users { get; set; }
        public int Total { get; set; }
    }
}

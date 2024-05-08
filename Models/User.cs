namespace ClothesBack.Models
{
    public class User
    {
        public User(int id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

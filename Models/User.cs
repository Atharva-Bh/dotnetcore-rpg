namespace dotnetcore_rpg.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public byte[] PasswordHash { get; set; }  = Array.Empty<byte>();
        public List<Character>? Characters { get; set; } 
    }
}
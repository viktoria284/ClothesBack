using ClothesBack.Models;

namespace ClothesBack.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}

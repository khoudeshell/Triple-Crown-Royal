using System;
namespace HorseLeague.Controllers
{
    public interface IUserSecurity
    {
        Guid UserId { get; }
        bool IsAuthenticated { get; }
    }
}
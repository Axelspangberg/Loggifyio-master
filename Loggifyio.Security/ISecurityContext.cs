using Loggifyio.Data.Model;

namespace Loggifyio.Security
{
    public interface ISecurityContext
    {
        User User { get; }
        bool IsAdministrator { get; }
    }
}
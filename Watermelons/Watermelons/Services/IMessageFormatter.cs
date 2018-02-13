using Watermelons.EntityFramework;

namespace Watermelons.Services
{
    public interface IMessageFormatter
    {
        string FormatMessage(CompetitionEntry entry);
    }
}

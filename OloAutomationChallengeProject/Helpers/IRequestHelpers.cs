using System.Net.Http;
using System.Threading.Tasks;

namespace OloAutomationChallengeProject.Helpers
{
    public interface IRequestHelpers
    {
        string SetupUrl(string baseUrl, string endpoint);
    }
}
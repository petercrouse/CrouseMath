using System.Threading.Tasks;
using CrouseMath.Application.Common.Models;

namespace CrouseMath.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
using System.Threading.Tasks;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Application.Common.Models;

namespace CrouseMath.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}
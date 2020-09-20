using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Application.Common.Models;
using MediatR;

namespace CrouseMath.Application.Students.Commands.CreateStudent
{
    public class StudentCreated : INotification
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        
        public class StudentCreatedHandler : INotificationHandler<StudentCreated>
        {
            private readonly INotificationService _notification;

            public StudentCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(StudentCreated notification, CancellationToken cancellationToken)
            {
                var message = new Message()
                {
                    From = "CrouseMath",
                    To = notification.Email,
                    Subject = "Account created",
                    Body = $"Hello {notification.FirstName} {notification.LastName},\n" +
                           $" Welcome to CrouseMath."
                };

                await _notification.SendAsync(message);
            }
        }
    }
}
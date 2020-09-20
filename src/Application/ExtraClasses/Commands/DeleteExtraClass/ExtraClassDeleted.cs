using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Application.Common.Models;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.ExtraClasses.Commands.DeleteExtraClass
{
    public class ExtraClassDeleted : INotification
    {
        public IEnumerable<Booking> Bookings { get; set; }   
        
        public class ExtraClassDeletedHandler : INotificationHandler<ExtraClassDeleted>
        {
            private readonly INotificationService _notificationService;

            public ExtraClassDeletedHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public async Task Handle(ExtraClassDeleted notification, CancellationToken cancellationToken)
            {
                var bookings = notification.Bookings.ToList();

                var message = new Message()
                {
                    From = "CrouseMath",
                    BCC = CreateBCCRecipients(bookings),
                    Subject = "Account created",
                    Body = $"Hello,\n" +
                           $" We regret to inform you that the class {bookings.FirstOrDefault().ExtraClass.Name} has been cancelled\n"
                };

                await _notificationService.SendAsync(message);
            }

            private string CreateBCCRecipients(List<Booking> bookings)
            {
                string bccRecipients = "";

                bookings.ForEach(x => bccRecipients += $"{x.Student.Email};");

                return bccRecipients;
            }
        }
    }
}
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepositories
{
    public interface INotificationRepository
    {
        System.Threading.Tasks.Task AddNotificationAsync(Notification notification);
        Task<IEnumerable<Notification>> GetNotificationsForUserAsync(int userId);
        System.Threading.Tasks.Task MarkAsReadAsync(int notificationId);
    }
}

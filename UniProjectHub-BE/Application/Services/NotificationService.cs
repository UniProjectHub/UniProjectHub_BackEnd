using Application.InterfaceRepositories;
using Application.InterfaceServies;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async System.Threading.Tasks.Task NotifyProjectDeadlineAsync(int userId, string projectName)
        {
            var notification = new Notification
            {
                UserId = userId.ToString(), // Ensure UserId type matches with Notification model
                Title = "Project Deadline Alert",
                Description = $"The project '{projectName}' is approaching its deadline.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Type = 1, // Use appropriate value or enum
                SourceType = 1 // Use appropriate value or enum
            };

            await _notificationRepository.AddNotificationAsync(notification);
        }

        public async System.Threading.Tasks.Task NotifyNewTaskAsync(int userId, string taskName)
        {
            var notification = new Notification
            {
                UserId = userId.ToString(), // Ensure UserId type matches with Notification model
                Title = "New Task Assigned",
                Description = $"A new task '{taskName}' has been assigned to you.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Type = 2, // Use appropriate value or enum
                SourceType = 2 // Use appropriate value or enum
            };

            await _notificationRepository.AddNotificationAsync(notification);
        }

        public async System.Threading.Tasks.Task NotifyMessageAsync(int userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId.ToString(), // Ensure UserId type matches with Notification model
                Title = "New Message",
                Description = message,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Type = 3, // Use appropriate value or enum
                SourceType = 3 // Use appropriate value or enum
            };

            await _notificationRepository.AddNotificationAsync(notification);
        }

        System.Threading.Tasks.Task INotificationService.NotifyProjectDeadlineAsync(int userId, string projectName)
        {
            throw new NotImplementedException();
        }

        System.Threading.Tasks.Task INotificationService.NotifyNewTaskAsync(int userId, string taskName)
        {
            throw new NotImplementedException();
        }

        System.Threading.Tasks.Task INotificationService.NotifyMessageAsync(int userId, string message)
        {
            throw new NotImplementedException();
        }
    }
}
    
        
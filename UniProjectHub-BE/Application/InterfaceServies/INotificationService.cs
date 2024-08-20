using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServies
{
    public interface INotificationService
    {
        Task NotifyProjectDeadlineAsync(int userId, string projectName);
        Task NotifyNewTaskAsync(int userId, string taskName);
        Task NotifyMessageAsync(int userId, string message);
    }
}
using ReturneeManager.Application.Interfaces.Services;
using System;

namespace ReturneeManager.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
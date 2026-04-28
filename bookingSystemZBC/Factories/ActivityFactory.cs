using bookingSystemZBC.Domain.Entities.Activities;
using bookingSystemZBC.Domain.Enums;
using System.Diagnostics;
using Activity = bookingSystemZBC.Domain.Entities.Activities.Activity;

namespace bookingSystemZBC.Factories
{
    public static class ActivityFactory
    {
        public static Activity Create (string name, ActivityType type, int maxParticipants, double price, string style = "", string level = "")
        {
            return type switch
            {
                ActivityType.Yoga => new YogaActivity(name, type, maxParticipants, price, style, level),
                ActivityType.Swimming => new SwimmingActivity(name, type, maxParticipants, price, style, level),
                ActivityType.Climbing => new ClimbingActivity(name, type, maxParticipants, price, style, level),
                _ => throw new ArgumentException($"Unsupported activity type: {type}")
            };
        }
    }
}

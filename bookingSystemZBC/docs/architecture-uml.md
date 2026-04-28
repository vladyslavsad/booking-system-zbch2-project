# UML Architecture

Full UML diagram for the current classes, interfaces, and enums in the project.

```mermaid
classDiagram
direction LR

namespace bookingSystemZBC.Models {
    class ActivityType {
        <<enumeration>>
        Swimming
        Climbing
        Yoga
    }

    class Locations {
        +string Id
        +string Name
        +string Description
        +bool IsAvailable
    }
}

namespace bookingSystemZBC.Model {
    class Member {
        +string Id
        +string Name
        +string Email
        +int Age
        +Member(string id, string name, string email, int age)
        +void UpdateEmail(string newEmail)
        +void UpdateName(string newName)
    }

    class Booking {
        +string BookingId
        +Member Member
        +Activity Activity
        +DateTime Date
        +string ToString()
    }
}

namespace bookingSystemZBC.Model.Activities {
    class Activity {
        <<abstract>>
        +string Name
        +string Id
        +int MaxParticipants
        +double Price
        +string GetDescription()
    }

    class ClimbingActivity {
        +string Name
        +ClimbingActivity(int maxParticipants, double price)
        +string GetDescription()
    }

    class SwimmingActivity {
        +string Name
        +SwimmingActivity(int maxParticipants, double price)
        +string GetDescription()
    }

    class YogaActivity {
        +string Name
        +YogaActivity(int maxParticipants, double price)
        +string GetDescription()
    }
}

namespace bookingSystemZBC.Models.Activity {
    class ActivitySession {
        +string Id
        +Activity Activity
        +Locations Location
        +DateTime StartTime
        +DateTime EndTime
        +ActivitySession(Activity activity, Locations location, DateTime start, DateTime end)
    }
}

namespace bookingSystemZBC.Interfaces {
    class IBookable {
        <<interface>>
        +Booking BookMember(Member member, Activity activity)
        +bool CanelBooking(Member member, Activity activity, DateTime bookingDate)
    }

    class IActivityService {
        <<interface>>
        +Activity CreateActivity(ActivityType type, int maxParticipants, double price)
        +void DeleteActivity(string activityId)
        +void AddActivity(Activity activity)
        +List~Activity~ GetAll()
        +List~Activity~ GetAvailableActivities()
    }

    class IActivitySchedulingService {
        <<interface>>
        +ActivitySession CreateActivitySession(Activity activity, List~Locations~ locations, DateTime start, DateTime end)
        +void DeleteActivitySession(string sessionId)
        +bool ValidateSessionTime(Activity activity, DateTime start, DateTime end)
        +List~ActivitySession~ GetSessionsForActivity(string activityId)
        +List~ActivitySession~ GetAllSessions()
    }
}

namespace bookingSystemZBC.Factories {
    class ActivityFactory {
        +Activity CreateActivity(ActivityType type, int maxParticipants, double price)$
    }
}

namespace bookingSystemZBC.Services {
    class ActivityService {
        +void AddActivity(Activity activity)
        +Activity CreateActivity(ActivityType type, int maxParticipants, double price)
        +List~Activity~ GetAll()
        +List~Activity~ GetAvailableActivities()
    }

    class ActivitySchedulingService

    class BookingService {
        +List~Booking~ Bookings$
        +Booking BookMember(Member member, Activity activity)
        +bool CanelBooking(Member member, Activity activity, DateTime bookingDate)
    }

    class Logger {
        +void Log(string message)$
    }
}

namespace bookingSystemZBC.Exeptions {
    class BookingExeption {
        +BookingExeption()
        +BookingExeption(string message)
        +BookingExeption(string message, Exception innerException)
    }
}

namespace bookingSystemZBC.Utils {
    class PrintList {
        +void Print~T~(IEnumerable~T~ list)$
    }
}

Activity <|-- ClimbingActivity
Activity <|-- SwimmingActivity
Activity <|-- YogaActivity

Booking --> Member : contains
Booking --> Activity : contains
ActivitySession --> Activity : scheduled for
ActivitySession --> Locations : uses

BookingService ..|> IBookable
ActivityService ..|> IActivityService

ActivityFactory ..> ActivityType : switches on
ActivityFactory ..> Activity : creates
ActivityFactory ..> ClimbingActivity : creates
ActivityFactory ..> SwimmingActivity : creates
ActivityFactory ..> YogaActivity : creates

ActivityService ..> ActivityFactory : uses
ActivityService ..> ActivityType : uses
BookingService --> Booking : manages
BookingService ..> Member : books
BookingService ..> Activity : books

IActivitySchedulingService ..> ActivitySession : returns
IActivitySchedulingService ..> Activity : uses
IActivitySchedulingService ..> Locations : uses

BookingExeption --|> Exception
BookingExeption ..> Logger : logs via
PrintList ..> IEnumerable : prints
```

## How To Read The Architecture

- `Activity` is the base abstraction for all activities.
- `ClimbingActivity`, `SwimmingActivity`, and `YogaActivity` are concrete implementations of `Activity`.
- `ActivityFactory` creates the correct activity based on `ActivityType`.
- `ActivityService` is the service layer for activity operations.
- `BookingService` handles bookings and stores a list of `Booking`.
- `Booking` connects `Member` and `Activity`.
- `ActivitySession` describes an activity held at a specific `Location` and time.
- `IActivityService`, `IBookable`, and `IActivitySchedulingService` are service-layer contracts.

## Notes About The Current Code

- `ActivityService` declares `IActivityService` but currently does not implement `DeleteActivity`.
- `ActivitySchedulingService` is still empty even though `IActivitySchedulingService` already exists.
- In `IActivitySchedulingService.CreateActivitySession(...)`, the parameter is `List<Locations>`, while `ActivitySession` uses a single `Locations`.
- In `Activity.Id`, every property access creates a new `Guid`, so the identifier is not stable for one object instance.

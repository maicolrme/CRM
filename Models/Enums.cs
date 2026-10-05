namespace CrmApp.Models;

public enum CustomerStatus
{
    Lead,
    Prospect,
    Customer,
    Inactive
}

public enum ActivityType
{
    Call,
    Meeting,
    Email,
    Note,
    Other
}

public enum CrmTaskStatus
{
    Pending,
    InProgress,
    Completed
}

# Ias Audit POC
---
### 1. To configure audit sinks and areate an instance of AuditManager
#
#
```csharp
   IAuditManager auditManager = new AuditManagerBuilder()
        .WriteToUri(options =>
        {
           options.Uri = "myuri";
        })
        .WriteToFile(options =>
        {
            options.FilePath = "Audit.txt";
        })
        .Build();

```

### 1.  Manually do a User Action audit 
```csarp
   var userId = 677623;
  //Simple user action Auditing
   await _auditManager.SaveAuditEvent(new AuditEvent
    {
        ApplicationName = "Weather Forecast API",
        EventName = "Weather API Access",
        TimeStamp = DateTime.Now,
        UserId = userId,
        EntityId = 23,
        EventType = AuditEventType.UserAction,
        AuditText = $"User-{userId} accessed Weather API"
    });
```
#### 2. Create a audit scope, and specify an object to track. Audit event will automatically dispatch once the code block goes out of scope. Auditing can also be done manually

```csharp
 Branch myBranch = new Branch
    {
        BranchId = 888737,
        Code = "74066666",
        Status = true,
        BranchName = "My corner wireless store",
        BranchAddress = new Address
        {
            StreetName = "Biscayne Blvd",
            StreetNumber = "100",
            City = "Miami",
            State = "FL",
            Zip = "33181"
        }
    };

await using AuditScope scope = auditManager.CreateScope(options =>
{
    options.EntityId= 19982;
    options.EventName = "Branch Update Flow";
    options.TargetEntity =  myBranch ;// Entity to track
    options.UserId = 1122;
});

// Mutate state
myBranch.Status = false;
myBranch.BranchAddress.City = "North Miami";
```
### Audit event output
```javascript
{
    "id": "e2c7108f-8f1e-42df-90e9-9c84d08e09c1",
    "EntityId": 19982,
    "AuditKey": "Branch19982",
    "ApplicationName": null,
    "UserId": 1122,
    "EventName": "Branch Update Flow",
    "EventType": 2,
    "AuditText": null,
    "TimeStamp": "2020-05-18T16:32:32.5013975-04:00",
    "Target": {
        "Name": "Branch",
        "InitialState": {
            "BranchId": 888737,
            "Code": "74066666",
            "BranchName": "My corner wireless store",
            "BranchAddress": {
                "StreetName": "Biscayne Blvd",
                "StreetNumber": "100",
                "City": "Miami",
                "State": "FL",
                "Zip": "33181"
            },
            "Status": true
        },
        "FinalState": {
            "BranchId": 888737,
            "Code": "74066666",
            "BranchName": "My corner wireless store",
            "BranchAddress": {
                "StreetName": "Biscayne Blvd",
                "StreetNumber": "100",
                "City": "North Miami",
                "State": "FL",
                "Zip": "33181"
            },
            "Status": false
        }
    }
}
```





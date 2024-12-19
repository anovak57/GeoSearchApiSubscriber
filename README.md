# GeoSearchApiSubscriber

GeoSearchApi Subscriber is a simple console application used for testing real time updates coming from https://github.com/anovak57/GeoSearch

After running the application - every request to fetch locations by the GeoSearch Api should notify this client.

---

## Prerequisites

- .NET 8.0 SDK
- Up and running GeoSearch Api 
- Visual Studio or Rider IDE (optional)

## Installation

1. Clone the repository:
   
   ```bash
   git clone https://github.com/anovak57/GeoSearchApiSubscriber.git
   cd GeoSearch
   
2. Update appsettings.Development.json with your environment-specific configuration:
   
   ```bash
   {
    "SignalR": {
      "HubUrl": "<GeoSearchApi host>/searchHub",
      "ApiKey": "<UserApiKey>"
    }
   }
   
  3. Build and run the application:
     ```bash
      dotnet build
      dotnet run

## Api keys

Username | API Key

- `member0` |	`0888ed55-167f-4ec8-95bd-f74f7f66088c`

- `member1`	| `da490e8e-de97-4f07-9335-83d9ce7f98ff`

- `member2` |	`29955f19-9bec-435a-83db-9bf188e03ada`

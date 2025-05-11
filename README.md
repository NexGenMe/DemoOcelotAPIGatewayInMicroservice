
# 🧩 .NET 8 Microservices with Ocelot API Gateway

This repository demonstrates a real-world microservices architecture built with **.NET 8** using **Ocelot API Gateway**. It includes advanced API management features like **JWT Authentication**, **Load Balancing**, **Caching**, **Rate Limiting**, and **Request Aggregation** to handle communication between services efficiently.

> 🎥 Inspired by the Net Cod Hub channel tutorial — “Building an Efficient API Gateway in .NET using Ocelot”.

---

## 📦 Tech Stack

- [.NET 8](https://dotnet.microsoft.com/)
- [Ocelot API Gateway](https://ocelot.readthedocs.io/)
- Microservices Architecture
- JWT Authentication
- In-Memory Caching
- Custom Middleware
- Load Balancing
- Rate Limiting
- Request Aggregation

---

## 🚀 Features

- 🔁 **Central API Gateway** – Routes and manages requests across microservices.
- 🔒 **JWT Secured Endpoints** – Authenticated access for secure operations.
- ⚡ **File-Based Caching** – Improved response time and reduced backend hits.
- 📊 **Load Balancing** – "Least Connection" strategy to distribute requests.
- ⏱️ **Rate Limiting** – Prevents service overuse with request throttling.
- 🔗 **Aggregated Requests** – Combines responses from multiple services into one.
- 🛠️ **Extensible Configuration** – Easy modifications via `ocelot.json`.

---

## 🛠️ Microservices Included

- **Auth Service** – Handles authentication via email and password.
- **User Service** – Returns user-related data.
- **Weather Forecast Service** – Delivers weather forecast data.
- **API Gateway** – Manages routing, caching, throttling, and security.

---

## 📁 Project Structure

```
/src
  /ApiGateway
    - ocelot.json
    - Program.cs
  /UserService
  /WeatherForecastService
  /AuthService
```

---

## ⚙️ Ocelot Configuration (`ocelot.json`)

```json
{
  "GlobalConfiguration": {
    "BaseUrl": "https://localhost:7000"
  },
  "Routes": [
    {
      "UpstreamPathTemplate": "/gateway/api/user",
      "UpstreamHttpMethod": [ "Get" ],
      "DownstreamPathTemplate": "/api/user",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        { "Host": "localhost", "Port": 7003 },
        { "Host": "localhost", "Port": 7004 },
        { "Host": "localhost", "Port": 7005 }
      ],
      "FileCacheOptions": {
        "TtlSeconds": 60,
        "Region": "defaultUser",
        "Header": "OC-Caching-Control",
        "EnableContentHashing": false
      },
      "RateLimitOptions": {
        "ClientWhitelist": [],
        "EnableRateLimiting": true,
        "Period": "60s",
        "PeriodTimespan": 6,
        "Limit": 2
      },
      "Key": "userService",
      "LoadBalancerOptions": {
        "Type": "LeastConnection"
      }
    },
    {
      "UpstreamPathTemplate": "/gateway/api/weatherforecast",
      "UpstreamHttpMethod": [ "Get" ],
      "DownstreamPathTemplate": "/api/WeatherForecast",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        { "Host": "localhost", "Port": 7002 }
      ],
      "FileCacheOptions": {
        "TtlSeconds": 60,
        "Region": "defaultWeatherforecast",
        "Header": "OC-Caching-Control",
        "EnableContentHashing": false
      },
      "RateLimitOptions": {
        "ClientWhitelist": [],
        "EnableRateLimiting": true,
        "Period": "60s",
        "PeriodTimespan": 6,
        "Limit": 2
      },
      "Key": "weatherforecastService"
    },
    {
      "UpstreamPathTemplate": "/gateway/api/auth/{email}/{password}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ],
      "DownstreamPathTemplate": "/api/auth/{email}/{password}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        { "Host": "localhost", "Port": 7001 }
      ]
    }
  ],
  "Aggregates": [
    {
      "RouteKeys": [
        "userService",
        "weatherforecastService"
      ],
      "UpstreamPathTemplate": "/gateway/api/aggregate"
    }
  ]
}
```

---

## 🚦 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- Visual Studio or VS Code
- Postman or Swagger

### Run the Solution

1. Clone the repository.
2. Open terminal in each service directory.
3. Run the services:
   ```bash
   dotnet run
   ```
4. Run the API Gateway:
   ```bash
   cd ApiGateway
   dotnet run
   ```

### Sample Endpoints

- `GET /gateway/api/user`
- `GET /gateway/api/weatherforecast`
- `GET /gateway/api/aggregate`
- `GET /gateway/api/auth/{email}/{password}`

---

## 🙌 Contributions

Pull requests are welcome! Please open an issue first to discuss what you would like to change.

---

## 📝 License

This project is licensed under the MIT License.

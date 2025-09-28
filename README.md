# 🏟️ Stadium Management System

A comprehensive **microservices-based stadium traffic management system** built with **.NET 6** that monitors and manages crowd flow at stadium gates using real-time sensor data processing.

## 📋 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Installation & Setup](#installation--setup)
- [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Testing](#testing)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)

## 🎯 Overview

The Stadium Management System is designed to provide real-time monitoring and management of crowd traffic at stadium gates. The system processes sensor data from multiple gates, stores it in a database, and provides RESTful APIs for data retrieval and analysis.

### Key Capabilities:
- **Real-time sensor data processing** via message queues
- **Multi-gate traffic monitoring** with entry/exit tracking
- **Data filtering and analytics** by gate, type, and time range
- **RESTful API** for data access and management
- **Background service processing** for continuous data handling
- **Simulator** for testing and demonstration purposes

## 🏗️ Architecture

The system follows a **clean architecture pattern** with clear separation of concerns across multiple microservices:

```
┌─────────────────┐    ┌──────────────────┐    ┌─────────────────┐
│  Sensor         │    │    RabbitMQ      │    │  Sensor Data    │
│  Simulator      │──▶│    Message       │───▶│  Service        │
│                 │    │    Queue         │    │  (Background)   │
└─────────────────┘    └──────────────────┘    └─────────────────┘
                                                         │
                                                         ▼
┌─────────────────┐    ┌──────────────────┐    ┌─────────────────┐
│  Web Client     │◀──│  Web API         │◀───│  PostgreSQL     │
│  (Swagger UI)   │    │  (Controllers)   │    │  Database       │
└─────────────────┘    └──────────────────┘    └─────────────────┘
```

### Data Flow:
1. **Sensor Simulator** generates mock sensor data and publishes to RabbitMQ
2. **RabbitMQ** queues messages for reliable delivery
3. **Sensor Data Service** consumes messages and processes them
4. **PostgreSQL** stores processed sensor data
5. **Web API** serves data via REST endpoints
6. **Clients** access data through HTTP APIs

## ✨ Features

### 🔄 Real-time Data Processing
- **Message Queue Integration**: Uses RabbitMQ for reliable message delivery
- **Background Processing**: Continuous data processing with hosted services
- **Event-driven Architecture**: Asynchronous processing of sensor events

### 📊 Data Management
- **Multi-gate Support**: Monitor multiple stadium gates simultaneously
- **Entry/Exit Tracking**: Distinguish between entry and exit events
- **Time-based Filtering**: Filter data by date/time ranges
- **People Counting**: Track number of people passing through gates

### 🌐 API Features
- **RESTful Endpoints**: Standard HTTP methods for data operations
- **Query Parameters**: Flexible filtering by gate, type, and time
- **Swagger Documentation**: Interactive API documentation
- **Error Handling**: Comprehensive error handling and logging

### 🧪 Testing & Simulation
- **Sensor Simulator**: Mock sensor data generation for testing
- **Unit Tests**: Comprehensive test coverage with NUnit and Moq
- **Integration Testing**: End-to-end testing capabilities

### 📈 Monitoring & Analytics
- **Data Visualization**: Swagger UI for API exploration
- **Logging**: Structured logging throughout the application
- **Performance Monitoring**: Built-in performance counters

## 🛠️ Technology Stack

### Backend Technologies:
- **.NET 6**: Modern, cross-platform framework
- **ASP.NET Core Web API**: RESTful API framework
- **Entity Framework Core**: ORM for database operations
- **PostgreSQL**: Reliable, open-source database
- **RabbitMQ**: Message broker for async communication

### Development Tools:
- **NUnit**: Unit testing framework
- **Moq**: Mocking framework for tests
- **Swagger/OpenAPI**: API documentation
- **Git**: Version control

### Architecture Patterns:
- **Clean Architecture**: Separation of concerns
- **Repository Pattern**: Data access abstraction
- **Dependency Injection**: Loose coupling and testability
- **Background Services**: Long-running tasks

## 📁 Project Structure

```
StadiumManagementSystem/
├── StadiumTrafficManager/                    # Main Web API
│   ├── Controllers/                          # API Controllers
│   │   └── StadiumManagerController.cs       # Main API controller
│   ├── Program.cs                           # Application entry point
│   ├── appsettings.json                     # Configuration
│   └── Properties/
│       └── launchSettings.json              # Launch configuration
├── StadiumTrafficManager.Common/             # Shared contracts
│   └── Contracts/
│       ├── SensorData.cs                    # Sensor data model
│       └── SensorResult.cs                  # API response model
├── StadiumTrafficManager.Services/           # Business logic layer
│   ├── Interface/
│   │   └── IStadiumManagerService.cs        # Service interface
│   └── Implementation/
│       └── StadiumManagerService.cs         # Service implementation
├── StadiumTrafficManager.Repository/         # Data access layer
│   ├── Interface/
│   │   └── IStadiumManagerRepository.cs     # Repository interface
│   ├── Persistance/
│   │   ├── Context/
│   │   │   └── StadiumManagerContext.cs     # EF DbContext
│   │   └── Repository/
│   │       └── StadiumManagerRepository.cs  # Repository implementation
│   └── Migrations/                          # Database migrations
├── StadiumTrafficManager.SensorDataService/ # Background service
│   ├── Program.cs                           # Service entry point
│   └── SensorDataServiceWorker.cs          # Message consumer
├── Sensor Simulator/                        # Mock sensor data generator
│   └── Program.cs                           # Simulator logic
└── StadiumTrafficManager.Tests/             # Unit tests
    ├── StadiumTrafficManagerServiceTest.cs  # Service tests
    └── Repository/                          # Mock implementations
```

## 📋 Prerequisites

Before running the application, ensure you have the following installed:

### Required Software:
- **.NET 6 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/6.0)
- **PostgreSQL 12+** - [Download here](https://www.postgresql.org/download/)
- **RabbitMQ Server** - [Download here](https://www.rabbitmq.com/download.html)
- **Git** (for version control)

### Verify Installation:
```bash
dotnet --version    # Should show 6.x.x
psql --version      # PostgreSQL version
rabbitmqctl status  # RabbitMQ status
```

## 🚀 Installation & Setup

### 1. Clone the Repository
```bash
git clone <repository-url>
cd StadiumManagementSystem
```

### 2. Database Setup (PostgreSQL)

#### Install PostgreSQL:
- Download and install PostgreSQL from the official website
- During installation, remember the password for the `postgres` user

#### Create Database:
```sql
-- Connect to PostgreSQL
psql -U postgres

-- Create the database
CREATE DATABASE EBI_incremental;

-- Verify database creation
\l

-- Exit psql
\q
```

#### Update Connection String:
Edit `StadiumTrafficManager/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EBI_incremental;Port=5432;Username=postgres;Password=YOUR_PASSWORD;"
  }
}
```

### 3. RabbitMQ Setup

#### Install RabbitMQ:
- Download and install RabbitMQ Server
- Ensure Erlang is installed (required for RabbitMQ)

#### Start RabbitMQ Service:
```bash
# Windows
rabbitmq-service start

# Linux
sudo systemctl start rabbitmq-server

# macOS
brew services start rabbitmq
```

#### Enable Management Plugin (Optional):
```bash
rabbitmq-plugins enable rabbitmq_management
```
- Access management UI at: `http://localhost:15672`
- Default credentials: `guest/guest`

#### Verify Installation:
```bash
rabbitmqctl status
```

### 4. Restore Dependencies
```bash
# Navigate to the project root
cd StadiumManagementSystem

# Restore all project dependencies
dotnet restore
```

### 5. Database Migrations
```bash
# Navigate to the main API project
cd StadiumTrafficManager

# Apply database migrations
dotnet ef database update

# If migrations don't exist, create them first
dotnet ef migrations add InitialMigration
dotnet ef database update
```

## 🏃‍♂️ Running the Application

The application consists of three main components that need to run simultaneously:

### Step 1: Start the Main Web API
```bash
# Terminal 1 - Navigate to main API project
cd StadiumTrafficManager
dotnet run
```

**API Endpoints:**
- **HTTPS:** `https://localhost:7189`
- **HTTP:** `http://localhost:5251`
- **Swagger UI:** `https://localhost:7189/swagger`

### Step 2: Start the Sensor Data Service
```bash
# Terminal 2 - Navigate to sensor data service
cd StadiumTrafficManager.SensorDataService
dotnet run
```

This service will:
- Connect to RabbitMQ
- Listen for sensor data messages
- Process and store data in PostgreSQL

### Step 3: Start the Sensor Simulator
```bash
# Terminal 3 - Navigate to sensor simulator
cd "Sensor Simulator"
dotnet run
```

This simulator will:
- Generate mock sensor data every 4 seconds
- Send data to RabbitMQ queue
- Simulate gate entry/exit events

## 📚 API Documentation

### Base URL
```
https://localhost:7189
```

### Endpoints

#### 1. Get All Sensor Data
```http
GET /StadiumManager/GetAllSensorData
```
**Description:** Retrieves all sensor data from the database

**Response:**
```json
[
  {
    "id": "guid",
    "gate": "Gate B",
    "type": "exit",
    "timestamp": "2024-01-15T10:30:00Z",
    "noOfPeople": 115
  }
]
```

#### 2. Add Sensor Data
```http
POST /StadiumManager?gate={gate}&noOfPeople={count}&type={type}
```
**Parameters:**
- `gate` (string): Gate identifier
- `noOfPeople` (int): Number of people
- `type` (string): Event type (entry/exit)

**Example:**
```bash
curl -X POST "https://localhost:7189/StadiumManager?gate=Gate%20A&noOfPeople=50&type=entry" -k
```

#### 3. Get Filtered Sensor Results
```http
GET /StadiumManager/GetSensorResults?gate={gate}&type={type}&startTime={start}&endTime={end}
```
**Parameters:**
- `gate` (string, optional): Filter by gate name
- `type` (string, optional): Filter by event type
- `startTime` (datetime, optional): Start time filter
- `endTime` (datetime, optional): End time filter

**Example:**
```bash
curl -X GET "https://localhost:7189/StadiumManager/GetSensorResults?gate=Gate%20B&type=exit" -k
```

### Data Models

#### SensorData
```json
{
  "id": "string (guid)",
  "gate": "string",
  "type": "string",
  "timestamp": "datetime",
  "noOfPeople": "integer"
}
```

#### SensorResult
```json
{
  "gate": "string",
  "type": "string",
  "noOfPeople": "integer"
}
```

## 🧪 Testing

### Running Unit Tests
```bash
# Navigate to test project
cd StadiumTrafficManager.Tests

# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal
```

### Manual Testing

#### 1. Test API Endpoints
- Open Swagger UI at `https://localhost:7189/swagger`
- Use the interactive interface to test endpoints
- Verify data is being stored and retrieved correctly

#### 2. Test Data Flow
1. Start all three applications
2. Monitor console outputs for message processing
3. Check database for new sensor data entries
4. Verify API responses contain the data

#### 3. Test Filtering
- Add multiple sensor data entries with different gates/types
- Test filtering by gate name
- Test filtering by event type
- Test time-based filtering

### Integration Testing
```bash
# Test the complete data flow
# 1. Ensure all services are running
# 2. Monitor RabbitMQ for message flow
# 3. Check database for data persistence
# 4. Verify API responses
```

## 🐛 Troubleshooting

### Common Issues and Solutions

#### 1. Database Connection Errors
**Error:** `Connection refused` or `Authentication failed`

**Solutions:**
- Verify PostgreSQL service is running
- Check connection string in `appsettings.json`
- Ensure database `EBI_incremental` exists
- Verify username/password are correct

#### 2. RabbitMQ Connection Issues
**Error:** `Connection refused` or `Channel error`

**Solutions:**
- Ensure RabbitMQ service is running
- Check if port 5672 is accessible
- Verify RabbitMQ management plugin is enabled
- Check firewall settings

#### 3. Port Conflicts
**Error:** `Address already in use`

**Solutions:**
- Check if ports 7189, 5251 are already in use
- Modify `launchSettings.json` to use different ports
- Kill processes using the ports

#### 4. Migration Issues
**Error:** `Migration not found` or `Database update failed`

**Solutions:**
```bash
# Create new migration
dotnet ef migrations add InitialMigration

# Update database
dotnet ef database update

# Reset migrations (if needed)
dotnet ef database drop
dotnet ef database update
```

#### 5. Dependency Issues
**Error:** `Package not found` or `Restore failed`

**Solutions:**
```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore packages
dotnet restore --force

# Update packages
dotnet add package PackageName --version LatestVersion
```

### Performance Issues

#### High Memory Usage
- Monitor background service memory consumption
- Check for memory leaks in message processing
- Consider implementing message batching

#### Slow Database Queries
- Add database indexes for frequently queried columns
- Optimize Entity Framework queries
- Consider implementing caching

### Logging and Debugging

#### Enable Detailed Logging
Update `appsettings.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```

#### Monitor Application Logs
- Check console outputs for error messages
- Monitor database logs for connection issues
- Review RabbitMQ logs for message processing

## 📊 Monitoring

### Application Health
- **API Health:** Check `https://localhost:7189/swagger`
- **Database:** Monitor PostgreSQL logs and performance
- **Message Queue:** Use RabbitMQ Management UI at `http://localhost:15672`

### Key Metrics to Monitor
- Message processing rate
- Database connection pool usage
- API response times
- Error rates and types

## 🤝 Contributing

### Development Setup
1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Ensure all tests pass
6. Submit a pull request

### Coding Standards
- Follow C# naming conventions
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Write unit tests for new features
- Update README for significant changes

### Testing Requirements
- All new features must have unit tests
- Integration tests for API endpoints
- Manual testing of complete workflows

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📞 Support

For support and questions:
- Create an issue in the repository
- Check the troubleshooting section
- Review the API documentation
- Contact the development team

---

**Happy Stadium Management! 🏟️**

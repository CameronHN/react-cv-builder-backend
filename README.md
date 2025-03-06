# CV Builder backend

## Description
This project serves as the backend for a CV builder application. It is built using .NET 8 and Entity Framework Core. The application allows users to create, update, and delete CVs. The application also allows users to create, update, and delete sections within a CV. The application is built using a RESTful API.

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/download/details.aspx?id=104781&lc=1033&msockid=052aae58b66767830a30bb28b7f96609)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)

## Project Structure

- **Domain**: Contains the entities.
- **Application**: Contains the application services, interfaces, and data transfer objects (DTOs).
- **Infrastructure**: Contains the Entity Framework Core migrations, database configurations, and repositories (generic and custom methods).
- **api (Presentation)**: Contains the API controllers.
- **Tests**: Contains the unit and integration tests.

## Installation

1. **Clone the repository**

    `git clone https://github.com/CameronHN/react-cv-builder-backend.git`

2. **Navigate to the project folder**

    `cd react-cv-builder-backend`


3. **Install .NET 8 SDK**

    Download and install the .NET 8 SDK from the official website

4. **Set up the database**

    - Ensure SQL Server is installed and running.
    - Update the connection string in `appsettings.json` to point to your SQL Server instance.

5. **Apply Migrations**

    Open a terminal in the project directory and run:

    `dotnet ef database update`

6. **Build the project**

    Open the project in Visual Studio and firstly build the application.

    `dotnet build`

7. **Test the project**

    `dotnet test`

8. **Run the project**

    `dotnet run`
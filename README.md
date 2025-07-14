
##  Tech Stack

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- Visual Studio 2022/2025
- Git

---

##  Getting Started

###  Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Visual Studio](https://visualstudio.microsoft.com/)

---

###  Running the API Locally

1. **Clone the repo:**

```bash
git clone https://github.com/codingfreshie/DGRC.git
cd DGRC
```

2. **Update connection string:**

In `appsettings.json`, configure your SQL Server connection:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=DATABASE_NAME_;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

3. **Apply EF migrations:**

```bash
dotnet ef migrations add InitialCreate

```
4.**Update the Database Structure**
```bash
dotnet ef database update
```

4. **Run the project:**

```bash
dotnet run
```

5. **Test in Swagger:**

Navigate to:  
`http://localhost:PORT/swagger`

---

##  Project Structure

```
├── Controllers/
│   ├── RegisterController.cs
│   └── LoginController.cs
├── DTOs/
│   ├── RegisterRequest.cs
│   └── LoginRequest.cs
├── Models/
│   └── User.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Migrations/
├── appsettings.json
├── Program.cs
└── .gitignore
```

---

##  Adding a New Controller

1. **Create the DTO if needed**  
   Add a new file in `DTOs/`, e.g., `MyFeatureRequest.cs`

2. **Create the controller:**  
   Add a new class in `Controllers/`, e.g., `MyFeatureController.cs`

```csharp
[ApiController]
[Route("api/[controller]")]
public class MyFeatureController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public IActionResult Test() => Ok("It works!");
}
```

3. **Rebuild the solution**  
   Your new controller will auto-appear in Swagger!
```bash
dotnet clean
dotnet build

```

---

##  Tips

- Use `[HttpGet]`, `[HttpPost]`, etc. to define endpoints.
- To add database interaction, inject `ApplicationDbContext`.
- Don’t forget to register new services if needed in `Program.cs`.



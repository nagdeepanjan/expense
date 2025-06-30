# Expense Management System

A modern, user-friendly expense tracking application built with ASP.NET Core 8.0 and MySQL.

## Features

### 📊 Expense Management
- **Create** new expenses with description, amount, category, and date
- **View** all expenses with filtering and search capabilities
- **Edit** existing expenses with full validation
- **Delete** expenses with confirmation dialogs
- **Filter** by category, date range, and search terms
- **Visual Analytics** with interactive pie charts

### 💰 Financial Tracking
- Real-time expense totals and summaries
- Category-based expense breakdown
- Date range filtering for specific periods
- Currency formatting with proper decimal handling

### 🎨 User Experience
- Responsive Bootstrap 5 design
- Modern, clean interface with icons
- Real-time form validation
- Success/error message notifications
- Predefined expense categories for quick selection

### 🔒 Security Features
- CSRF protection on all forms
- Input validation and sanitization
- Security headers (HSTS, X-Frame-Options, etc.)
- SQL injection protection through Entity Framework

## Technology Stack

- **Backend**: ASP.NET Core 8.0 MVC
- **Database**: MySQL 8.0
- **ORM**: Entity Framework Core
- **Frontend**: Bootstrap 5, Chart.js, Font Awesome
- **Deployment**: Docker, Azure Container Apps

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- MySQL 8.0 or higher
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd Expense
   ```

2. **Set up the database connection**
   ```bash
   # Add user secrets for connection string
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:ExpenseYoutubeDB" "server=localhost;database=ExpenseDB;user=your_user;password=your_password"
   ```

3. **Install dependencies**
   ```bash
   dotnet restore
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the application**
   Open your browser and navigate to `https://localhost:5001`

## Docker Deployment

### Build and run with Docker

```bash
# Build the Docker image
docker build -t expense-app .

# Run the container
docker run -p 8080:8080 expense-app
```

### Docker Compose (with MySQL)

```yaml
version: '3.8'
services:
  app:
    build: .
    ports:
      - "8080:8080"
    environment:
      - ConnectionStrings__ExpenseYoutubeDB=server=mysql;database=ExpenseDB;user=root;password=password
    depends_on:
      - mysql
  
  mysql:
    image: mysql:8.0
    environment:
      - MYSQL_ROOT_PASSWORD=password
      - MYSQL_DATABASE=ExpenseDB
    volumes:
      - mysql_data:/var/lib/mysql

volumes:
  mysql_data:
```

## Usage Guide

### Adding Expenses

1. Click "Add New Expense" button
2. Fill in the expense details:
   - **Description**: What the expense was for
   - **Amount**: Cost in dollars (e.g., 25.99)
   - **Category**: Select from predefined or enter custom
   - **Date**: When the expense occurred
3. Click "Add Expense" to save

### Managing Expenses

- **View All**: Main page shows all expenses with totals
- **Filter**: Use the filter form to narrow down results
- **Edit**: Click the edit icon next to any expense
- **Delete**: Click the delete icon and confirm removal

### Analytics

The dashboard displays:
- Total expenses for the current filter
- Interactive pie chart showing spending by category
- Responsive charts that update based on filters

## API Endpoints

The application exposes the following endpoints:

- `GET /Expense` - List all expenses with optional filtering
- `GET /Expense/Create` - Show create form
- `POST /Expense/Create` - Create new expense
- `GET /Expense/Edit/{id}` - Show edit form
- `POST /Expense/Edit/{id}` - Update expense
- `GET /Expense/Delete/{id}` - Show delete confirmation
- `POST /Expense/Delete/{id}` - Delete expense
- `GET /Expense/GetChart` - Get chart data (JSON)

## Configuration

### Environment Variables

- `ConnectionStrings__ExpenseYoutubeDB`: Database connection string
- `ASPNETCORE_ENVIRONMENT`: Environment (Development/Production)

### Security Configuration

The application includes several security features:
- HTTPS enforcement
- Security headers (HSTS, CSP, X-Frame-Options)
- CSRF protection
- Input validation

## Development

### Project Structure

```
Expense/
├── Controllers/          # MVC Controllers
├── Data/                # Database context and services
│   ├── Service/         # Business logic services
│   └── ExpenseDbContext.cs
├── Models/              # Data models
├── Views/               # Razor views
│   ├── Expense/         # Expense-related views
│   └── Shared/          # Shared layouts
├── wwwroot/             # Static files
├── Migrations/          # EF Core migrations
└── Program.cs           # Application startup
```

### Adding New Features

1. Create/modify models in `Models/`
2. Update the database context if needed
3. Add business logic to services in `Data/Service/`
4. Create controller actions in `Controllers/`
5. Add corresponding views in `Views/`

### Database Migrations

```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For support and questions, please open an issue in the repository or contact the development team.
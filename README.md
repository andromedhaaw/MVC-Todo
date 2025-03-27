

# MVC-Todo Application

This is a simple To-Do application built using **ASP.NET Core MVC** and **SQLite** as the database. It provides a clean and straightforward structure for managing tasks, supporting CRUD operations, and offering user authentication.

## Features

- **MVC Architecture**: The application follows the MVC (Model-View-Controller) pattern, ensuring separation of concerns and maintainable code.
- **SQLite Database**: Lightweight and easy-to-use SQLite database for storing tasks.
- **User Authentication**: Built-in user authentication using ASP.NET Core Identity.
- **CRUD Operations**: Create, Read, Update, and Delete tasks in a simple, intuitive interface.
- **Fluent API Configuration**: Uses Fluent API for advanced database configurations.

## Prerequisites

Before you begin, make sure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet)
- [SQLite](https://www.sqlite.org/download.html) (for local database)
- A text editor like [Visual Studio Code](https://code.visualstudio.com/) (with the C# extension) or [Visual Studio](https://visualstudio.microsoft.com/)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/andromedhaaw/MVC-Todo.git
```

### 2. Install Dependencies

Navigate to the project directory and restore the dependencies:

```bash
cd MVC-Todo
dotnet restore
```

### 3. Set Up the Database

To set up the SQLite database and run migrations:

```bash
dotnet ef database update
```

This will create the necessary database files, including `app.db`.

### 4. Run the Application

To start the application:

```bash
dotnet run
```

The application should now be running at [http://localhost:5000](http://localhost:5000).

### 5. Accessing the Application

Once the app is running, you can access the following features:

- **Register**: Sign up as a new user.
- **Login**: Sign in to manage your tasks.
- **Todo List**: View, add, edit, and delete tasks.
  
## File Structure

Here’s a brief overview of the important directories:

- **Areas/Identity/Pages**: Contains pages for managing user authentication (login, registration, etc.).
- **Controllers**: Includes logic for handling HTTP requests and serving views.
- **Data**: Contains database context and migrations.
- **Models**: Defines the structure of data entities.
- **Services**: Contains business logic, including any helpers or services used in the application.
- **Views**: Contains Razor views (UI templates).
- **Validator**: Contains validation logic (e.g., form validation).

## Configuration

The project uses two configuration files:

- **appsettings.json**: Default application settings.
- **appsettings.Development.json**: Development-specific settings (such as connection strings).

### Fluent API Configuration

The application uses Fluent API to configure the database models and relationships between entities. This approach provides more control over the database schema.



## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---


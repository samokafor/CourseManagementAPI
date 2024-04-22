# Course Management Web Application

This is a basic web application for managing courses, developed as part of a task. The application provides user authentication using JWT, role-based authorization, and functionality for managing courses, including viewing, adding, editing, and deleting courses.

## Features

### User Authentication
- Users can register for an account with a username, email, and password either as administrators or as regular users.
- Registered users can log in to access the application.
- JWT (JSON Web Tokens) are used for user authentication.

### User Authorization
- Role-based authorization restricts access to certain functionalities based on user roles.
- Two roles are defined: Administrator and Regular User.
- Only administrators can perform administrative tasks such as adding, editing, and deleting courses.

### Entity Post or Update Functionality
- Mechanism provided to create, edit, and delete entities from the system, but only the admin can carry out these functions.

## Backend Implementation

The backend of this application is developed using .NET Web API, utilizing Ef Core for database interactions, implementing the Repository pattern, and using MSSQL for data storage.

## Usage

To run the application, follow these steps:

1. Clone the repository to your local machine.
2. Navigate to the backend directory and set up the .NET Web API project.
3. Update the connection string in the appsettings.json with your connection string.
4. Update the database by running `dotnet ef database update`.
5. Ensure you have a compatible database (e.g., MSSQL) set up and configured.
6. Run the application, and the backend server should start listening on the specified port.

## Developer

- [Samuel N Okafor]
- [Samuel.Okafor@firstbankgroup.com]

## License

This project is licensed under the [Apache Licence](LICENSE).

# Course Management Web Application

This is a basic web application for managing courses, developed as part of a task. The application provides user authentication using JWT, role-based authorization, and functionality for managing courses, including viewing, adding, editing, and deleting courses.

## Features

### User Authentication
- Users can register for an account with a username, email, and password.
- Registered users can log in to access the application.
- JWT (JSON Web Tokens) are used for user authentication.

### User Authorization
- Role-based authorization restricts access to certain functionalities based on user roles.
- Two roles are defined: Administrator and Regular User.
- Only administrators can perform administrative tasks such as adding, editing, and deleting courses.

### Course Management Page
- Users can view a list of courses with basic details like course name, description, instructor, and start date.
- Functionality is provided for adding, editing, and deleting courses.

### Course Details Page
- Clicking on a course takes the user to a details page where they can view more information about the course.
- All details of the selected course are displayed, including course name, description, instructor, start date, end date, etc.

### Add/Edit Course Page
- Users can add a new course or edit an existing course using a form.
- Input fields are validated to ensure that required fields are filled, and dates are in the correct format.

### Delete Course Functionality
- Mechanism provided to delete a course from the system, typically through a button or link on the course management page.

## Backend Implementation

The backend of this application is developed using .NET Web API, utilizing Ef Core for database interactions, implementing the Repository pattern, and using MSSQL for data storage.

## Usage

To run the application, follow these steps:

1. Clone the repository to your local machine.
2. Navigate to the backend directory and set up the .NET Web API project.
3. Ensure you have a compatible database (e.g., MSSQL) set up and configured.
4. Run the application, and the backend server should start listening on the specified port.
5. Set up the frontend React.js app if required, using the provided dummy data to support the UI.

## Developer

- [Samuel N Okafor]
- [Samuel.Okafor@firstbankgroup.com]

## License

This project is licensed under the [Apache Licence](LICENSE).

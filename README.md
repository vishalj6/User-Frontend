# Role-Based User Management System

This is a Role-Based User Management System built with ASP.NET Core. It provides functionalities such as admin login, user login, password reset, user registration with OTP verification, user editing, user deletion, and JWT token-based authentication. 

## Features

- **Admin Login:** Admin can log in to manage users.
- **User Login:** Registered users can log in to access their accounts.
- **Forgot Password:** Users can reset their password if they forget it.
- **Create User (Sign Up):** New users can sign up with OTP verification.
- **Edit User:** Users can update their profile information.
- **Delete User:** Users can delete their account.
- **Delete All Users:** Admin can delete all user accounts.
- **JWT Token:** Used for authentication, creating a cookie, and decrypting received encrypted data.

## Demo

### Admin Login
![Admin Login](https://github.com/vishalj6/User-Frontend/assets/99495355/26dd9b63-5d3c-46d0-b0c3-86a2082860a6)

### User Login
![User Login](https://github.com/vishalj6/User-Frontend/assets/99495355/4a518028-d7cf-4384-bee7-4457961d99cd)

### Forgot Password
![Forgot Password](https://github.com/vishalj6/User-Frontend/assets/99495355/81a11895-2362-4921-be74-c23fc5829e5c)

### Create User (Sign Up)
![Create User](https://github.com/vishalj6/User-Frontend/assets/99495355/7f361a2a-8256-4780-a125-e420e808dea2)

### OTP Verification
![OTP Verification](https://github.com/vishalj6/User-Frontend/assets/99495355/52b4eb7c-df97-4a9c-9288-add80e182742)
![OTP Verification](https://github.com/vishalj6/User-Frontend/assets/99495355/9d9dc67b-cd7d-45f2-9fbb-fbff0c542601)

### Edit User
![Edit User](https://github.com/vishalj6/User-Frontend/assets/99495355/d0fcb437-2454-4499-be61-a820470e97ad)

### Delete User
![Delete User](https://github.com/vishalj6/User-Frontend/assets/99495355/70ae1753-fdfe-4cf1-b142-60f35a23da03)

### Admin Dashboard
![Admin Dashboard](https://github.com/vishalj6/User-Frontend/assets/99495355/220bea70-4bef-4878-8802-94dca895ad05)

## Installation

1. **Clone the repository:**
    ```bash
    git clone https://github.com/vishalj6/your-repo-name.git
    cd your-repo-name
    ```

2. **Set up the database:**
    - Update the connection string in `appsettings.json` to point to your database.
    - Apply migrations to create the database schema:
    ```bash
    dotnet ef database update
    ```

3. **Run the application:**
    ```bash
    dotnet run
    ```

4. **Navigate to:**
    ```
    http://localhost:5000
    ```

## Usage

1. **Admin Login:**
    - Navigate to `/admin-login`
    - Enter admin credentials

2. **User Login:**
    - Navigate to `/login`
    - Enter user credentials

3. **Forgot Password:**
    - Navigate to `/forgot-password`
    - Enter registered email address

4. **Create User (Sign Up):**
    - Navigate to `/sign-up`
    - Fill in the required details and complete OTP verification

5. **Edit User:**
    - Navigate to `/edit-user`
    - Update user information

6. **Delete User:**
    - Navigate to `/delete-user`
    - Confirm account deletion

7. **Admin Dashboard:**
    - Navigate to `/admin-dashboard`
    - Manage users (create, edit, delete, delete all)

## Technologies Used

- **ASP.NET Core**
- **Entity Framework Core**
- **JWT Token Authentication**
- **SQL Server**

## Contributing

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Commit your changes.
4. Push to your branch.
5. Create a new Pull Request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For any questions or suggestions, please reach out at [jadejavishal6@gmail.com](mailto:jadejavishal6@gmail.com).

---

*This README was generated with ❤️ by [Vishal Jadeja](https://github.com/vishalj6).*

# Sukalibur

**Sukalibur** is a dynamic and robust trip marketplace backend, crafted with cutting-edge **ASP.NET Core 8** and powered by the **HotChocolate GraphQL** library. Designed to simplify and enhance the travel experience, Sukalibur connects travelers and trip providers through a seamless API, ensuring efficient and smooth interactions.

## Features

- **HotChocolate GraphQL**: Built using the [HotChocolate GraphQL library](https://chillicream.com/docs/hotchocolate/v13), providing a powerful and flexible API layer with support for advanced features like schema stitching, filtering, and more.
- **ASP.NET Core 8**: Built on the latest and greatest version of ASP.NET Core, ensuring high performance, security, and scalability.
- **Entity Framework Core**: Utilizes Entity Framework Core for efficient data access and management, making it easy to interact with the database using object-oriented code.
- **MySQL**: Integrated with MySQL for reliable and scalable data storage, tailored to handle a wide range of trip marketplace data.
- **Modular Code Structure**: Designed with a clean, modular architecture, making the codebase easy to extend, maintain, and navigate.
- **Best Practice Oriented**: Follows industry best practices for security, performance, and code quality, ensuring a stable and maintainable codebase.

## Getting Started

To get started with Sukalibur, clone this repository and follow the steps below:

```bash
git clone https://github.com/irvanherz/sukalibur.git
cd sukalibur
dotnet build
dotnet run
```

## Setup for Debugging

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/irvanherz/sukalibur.git
   cd sukalibur
   ```

2. **Configure Secrets**:
   - Use the [ASP.NET Core Secret Manager tool](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0) to securely store sensitive information such as connection strings and API secrets.
   - The `secrets.json` file should follow this format:

   ```json
   {
     "JwtConfig": {
       "Issuer": "Sukalibur",
       "Audience": "Sukalibur",
       "Secret": "Sukalibur_xxxxx"
     },
     "ConnectionStrings": {
       "DefaultConnection": "server=localhost;port=3306;database=sukalibur;user=root;AllowZeroDateTime=True"
     }
   }
   ```

   - For detailed instructions on setting up secrets, refer to the [official Microsoft documentation](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0).

3. **Create and Apply Entity Framework Migrations**:
   - Ensure that Entity Framework Core CLI tools are installed:

   ```bash
   dotnet tool install --global dotnet-ef
   ```

   - Create a new migration:

   ```bash
   dotnet ef migrations add InitialCreate
   ```

   - Apply the migration to the MySQL database:

   ```bash
   dotnet ef database update
   ```

4. **Run the Application**:
   - Use the following command to run the application in development mode:

   ```bash
   dotnet run --environment Development
   ```

## API Documentation

Sukalibur uses the HotChocolate GraphQL library, allowing you to explore the API with tools like GraphiQL or Postman. The GraphQL playground is available at `/graphql`.

## Contributing

We welcome contributions! Whether you find a bug, have a feature request, or want to help with the code, feel free to submit an issue or pull request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

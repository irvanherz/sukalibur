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
   - The `secrets.json` file should follow this format:

   ```json
   {
   "JwtAuthOptions": {
      "Issuer": "Sukalibur",
      "Audience": "Sukalibur",
      "Secret": "????",
      "AccessTokenTtl": 900,
      "RefreshTokenTtl": 864000
   },
   "ConnectionStrings": {
      "DefaultConnection": "host=localhost;port=5432;database=sukalibur;username=postgres;password=???"
   },
   "BunnyStorageOptions": {
      "BaseUrl": "https://sukalibur-storage-dev.b-cdn.net",
      "Zone": "sukalibur-storage-dev",
      "MainRegion": "sg",
      "AccessKey": "???"
   },
   "MidtransOptions": {
      "MerchantId": "???",
      "ClientKey": "???",
      "ServerKey": "???"
   },
   "CommonOptions": {
      "EmbeddingServiceBaseUrl": "http://localhost:8000",
      "WebAppBaseUrl": "http://localhost:5163",
      "NoreplyEmailAddress":  "noreply@ivn.my.id"
   },
   "AWSProfileName": "ivn"
   }
   ```
   - Setup application default credentials for Firebase. Add below to `/etc/profile`, then relogin:
   ```
   export GOOGLE_APPLICATION_CREDENTIALS="/home/ivn/.secrets/sukalibur-dev-firebase-adminsdk-fbsvc-4a5ef2c32d.json"
   ```
   - For detailed instructions on setting up secrets, refer to the [official Microsoft documentation](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0).

3. **Create and Apply Entity Framework Migrations**:
   - Ensure that Entity Framework Core CLI tools are installed:

   ```bash
   dotnet tool install dotnet-ef
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

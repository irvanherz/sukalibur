# Sukalibur

**Sukalibur** is a dynamic and robust trip marketplace backend, built with **ASP.NET Core 8** and powered by the **HotChocolate GraphQL** library. It aims to simplify and enhance the travel experience by connecting travelers and trip providers through a seamless API, ensuring efficient and smooth interactions.

## Features

- **HotChocolate GraphQL**: Built with the [HotChocolate GraphQL library](https://chillicream.com/docs/hotchocolate/v13), providing a powerful, flexible API layer with advanced features like schema stitching, filtering, and more.
- **ASP.NET Core 9**: Leverages the latest version of ASP.NET Core for high performance, security, and scalability.
- **Entity Framework Core**: Utilizes Entity Framework Core for efficient data access and management, enabling easy interaction with the database using object-oriented code.
- **PostgreSQL**: Integrated with PostgreSQL for reliable, scalable data storage, optimized for handling a wide range of trip marketplace data.
- **Modular Code Structure**: Designed with a clean, modular architecture for easy extension, maintenance, and navigation.
- **Best Practices**: Follows industry best practices for security, performance, and code quality, ensuring a stable and maintainable codebase.

## Prerequisities

In order to go to getting started, you must prepare some of thing.

- Active AWS CLI Credential with SES.
- Firebase project
- BunnyCDN Storage
- Midtrans developer account
- PostgreSQL 
  Ensure your PostgreSQL setup includes:
  - **pgvector**: [pgvector](https://github.com/pgvector/pgvector)
  - **postgis**: [postgis](https://postgis.net/)

## Getting Started

To get started with Sukalibur, follow the steps below:

1. **Clone Repository**
   ```bash
   git clone https://github.com/irvanherz/sukalibur.git
   ```

2. **Configure Secrets**:
   The `secrets.json` file should follow this format:

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
       "NoreplyEmailAddress": "noreply@ivn.my.id"
     },
     "AWSProfileName": "ivn"
   }
   ```

   For GNU/Linux systems, the `secrets.json` file should be located at `/home/ivn/.microsoft/usersecrets/65a20657-d3f5-403e-9a81-3e11f6e4edbc/secrets.json`. 

   **Firebase Setup**:
   - Add Firebase application credentials to `/etc/profile` and relogin:

     ```bash
     export GOOGLE_APPLICATION_CREDENTIALS="/home/ivn/.secrets/sukalibur-dev-firebase-adminsdk-fbsvc-4a5ef2c32d.json"
     ```

   For detailed instructions, refer to the [official Microsoft documentation](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0).

3. **Create and Apply Entity Framework Migrations**:
   Ensure Entity Framework Core CLI tools are installed:

   ```bash
   dotnet tool install dotnet-ef
   ```

   Create a new migration:

   ```bash
   dotnet ef migrations add InitialCreate
   ```

   Apply the migration to the MySQL database:

   ```bash
   dotnet ef database update
   ```

4. **Run the Application**:
   Start the application in development mode:

   ```bash
   dotnet run --environment Development
   ```

## API Documentation

Sukalibur uses the HotChocolate GraphQL library, allowing you to explore the API with tools like GraphiQL or Postman. Access the GraphQL playground at `/graphql`.

## Contributing

We welcome contributions! If you find a bug, have a feature request, or want to help with the code, feel free to submit an issue or pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.

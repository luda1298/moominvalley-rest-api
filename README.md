# Moomin Valley REST API
Educational ASP.NET Core REST API with client application. It was created as part of an API development course exercise.
The application manages a collection of Moomin characters and provides basic CRUD operations using an in-memory data store.

## What the Application Does

The API offers a RESTful interface for managing Moomin characters in Moomin Valley.  
The API provides endpoints to:

- List all Moomins
- Search Moomins by name 
- Add a new Moomin
- Retrieve a single Moomin by its number
- Delete a Moomin by its number

## API Documentation (Swagger)

Swagger / OpenAPI is included in the project and can be used to explore and test the API.
Once the application is running, open the Swagger UI in your browser to see:

- All available endpoints
- Request/response schemas
- Example payloads
- Response codes

## Behavior and Responses

The API follows common REST conventions and uses appropriate HTTP status codes, including:

- `201 Created` when a new Moomin is added (includes a `Location` header to the created resource)
- `409 Conflict` if a Moomin with the same number already exists
- `404 Not Found` when a requested Moomin does not exist
- `400 Bad Request` for missing or invalid input
- `204 No Content` when deletion succeeds

## Technologies Used

- C#
- ASP.NET Core Web API
- Swagger / OpenAPI

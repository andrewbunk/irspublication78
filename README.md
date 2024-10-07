IRS Tax-Exempt Organizations Application
========================================

Overview
--------

This application is designed to help users access information about IRS tax-exempt organizations, including details about their giving limitations. Built using ASP.NET Core 8 for the back-end and React with Material-UI for the front-end, this application provides a modern and responsive user interface for efficient navigation and data retrieval.

Features
--------

-   **Search for Tax-Exempt Organizations**: Easily search for organizations by name or criteria.
-   **View Giving Limitations**: Access information about the giving limitations associated with each organization.
-   **User-Friendly Interface**: The front-end utilizes Material-UI to ensure a responsive and visually appealing experience.

Prerequisites
-------------

Before you begin, ensure you have the following software installed on your machine:

-   [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
-   [Node.js (LTS version)](https://nodejs.org/)
-   [npm (Node Package Manager)](https://www.npmjs.com/get-npm)
-   [Visual Studio 2022 or later](https://visualstudio.microsoft.com/) (with ASP.NET and web development workload) or [Visual Studio Code](https://code.visualstudio.com/)
-   [SQL Server] A SQL server with a database of irspublication78 created.

Getting Started
---------------

Follow these steps to set up and run the application on your local machine.

### 1\. Clone the Repository

bash



`git clone https://github.com/andrewbunk/irspublication78.git
cd irspublication78`

### 2\. Set Up the Back-End (ASP.NET Core)

Navigate to the back-end directory and restore the dependencies:

bash



`cd IRSPublication78.Server
dotnet restore`

#### 3\. Configure the Database Connection String

The connection string for SQL Server is stored in the user secrets for the back-end project. To set it up:

1.  **Initialize User Secrets**: Run the following command in the back-end project directory:

    bash

    

    `dotnet user-secrets init`

2.  **Set the Connection String**: Use the following command to store your SQL Server connection string under the key `IrsPublication78`:

    bash

    

    `dotnet user-secrets set "ConnectionStrings:IrsPublication78" "Your_Connection_String_Here"`

    Replace `Your_Connection_String_Here` with your actual SQL Server connection string.

3.  **Accessing the Connection String**: Ensure your `appsettings.json` file contains the following to access the connection string:

    json

    

    `{
      "ConnectionStrings": {
        "IrsPublication78": ""
      }
    }`

4.  **Apply Migrations** (if using Entity Framework): Run the following command to apply any migrations:

    bash

    

    `dotnet ef database update`

### 4\. Run the Back-End

You can run the ASP.NET Core back-end using the following command:

bash



`dotnet run`

By default, the API will be available at `https://localhost:7158`.

### 5\. Set Up the Front-End (React)

Open a new terminal and navigate to the front-end directory:

bash



`cd irspublication78.client`

Install the necessary packages, including Material-UI:

bash



`npm install`

### 6\. Run the Front-End

Start the React application:

bash



`npm start`

The React application will be available at `https://localhost:5173`.

Using the Application
---------------------

1.  **Access the Application**: Open your web browser and navigate to `https://localhost:5173`.
2.  **Search for Organizations**: Use the search functionality to find IRS tax-exempt organizations by name or criteria.
3.  **View Details**: Click on an organization to view detailed information, including any relevant giving limitations.

Debugging with Visual Studio
----------------------------

To run the application in debug mode using Visual Studio:

1.  **Open the Solution**: Open the solution file (`.sln`) located in the root of your project.

2.  **Set the StartUp Project**: Right-click on the back-end project in Solution Explorer and select "Set as StartUp Project."

3.  **Start Debugging**: Press `F5` or click the green play button in the toolbar. This will start the back-end API in debug mode.

4.  **Run the Front-End**: Open a terminal, navigate to the front-end directory, and run:

    bash

    

    `npm start`

    Alternatively, you can configure Visual Studio to launch the front-end as well by adding a second project to the launch settings.

Additional Scripts
------------------

### Building the Front-End for Production

To create a production build of the React app, run:

bash



`npm run build`

This will create an optimized build of the front-end in the `build` directory.

Common Commands
---------------

-   **Back-End**: `dotnet run` (starts the ASP.NET Core API)
-   **Front-End**: `npm start` (starts the React application)

Troubleshooting
---------------

-   Ensure that the back-end API is running before accessing the front-end application.
-   Check for any error messages in the terminal for both the back-end and front-end and address them accordingly.

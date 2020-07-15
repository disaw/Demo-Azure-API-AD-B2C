![Design](https://github.com/disaw/Demo-Azure-API-AD-B2C/blob/master/Documents/Design.JPG?raw=true)

## How to Run:

1. Set 'Functions' and 'WebApp' as two starting projects.

![Starting Projects](https://github.com/disaw/Demo-Azure-API-AD-B2C/blob/master/Documents/MultipleStartup.png?raw=true)


2. Restore nuget packages 

![Solution Structure](https://github.com/disaw/Demo-Azure-API-AD-B2C/blob/master/Documents/Projects.png?raw=true)


3. Run the solution

![Web UI](https://github.com/disaw/Demo-Azure-API-AD-B2C/blob/master/Documents/WebUI.JPG?raw=true)


## Authentication:

Authentication is done using the latest Azure Active Directory B2C Authentication service.

List of products are pre populated for demonstration purposes.
When you run the application you can list the products without logging in.
In order to perform Create, Update or Delete oprations, users have to register and login.
Registration can be done using a valid email address where the confirmation code will be sent.
Benefits of Azure AD B2C Authentication service is that, it is fully cloud based and can be extended to use with existing providers like Facebook.


## How to Run Test:

Right Click 'ProductTests' project and click 'Run Tests'

Tests are written in xUnit and Moq.

![Test](https://github.com/disaw/Demo-Azure-API-AD-B2C/blob/master/Documents/Tests.png?raw=true)


## Azure Hosted Live Demo:

https://demo-products-webapp-disaw.azurewebsites.net/


## Sample API Endpoints (local):

GET http://localhost:7777/api/products/

GET http://localhost:7777/api/products/2

POST http://localhost:7777/api/products/

PUT http://localhost:7777/api/products/2

DELETE http://localhost:7777/api/products/1

GET http://localhost:7777/api/products/description/Dell

A Complete Postman API Request Collection is attached with the solution.



## Troubleshoot:

Make sure the "ApiUrl": "http://localhost:7777/api/products/" is correctly set in WebApp -> appsettings.json



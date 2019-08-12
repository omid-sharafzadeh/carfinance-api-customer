# CarFinance-API-Customer
A RESTful API for customer CRUD operations.

## To run locally
### Clone the repo from GitHub
-- ***git clone*** git@github.com:omid-sharafzadeh/carfinance-api-customer.git

### Build the docker image
-- Install docker for your OS (more info on: https://www.docker.com/get-started)

-- Open command-line of your choice and change directory to the root directory
(**[PathToRepository]/carfinance-api-customer**)

-- Run **'docker build . -t car-finance'** and wait a minute or two untill an image is built

-- To check for the list of images you can run **'docker images'**. You should be able to see the name of your image under the Repository column.

-- Now all you need to do is run **'docker-compose up -d'** to run an instance of your API along with a mongodb server

-- To stop the services/containers you can run **'docker-compose down'**.

### To Test the whole thing :)

-- To test the API you can use Postman or Swagger which is alredy installed. To use Swagger, navigate to **'http:localhost:5050/swagger'** and you will be abble to hit the endpointd easily.

-- Use the POST endpoint to create a customer and then use the GET endpoint(s) to retrieve them.

-- In order to access the MongoDB through a GUI install **MongoCompass** which is free and easy to use. Here is the link to download: https://www.mongodb.com/download-center/compass?jmp=hero

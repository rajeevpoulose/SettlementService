# Settlement Service

API localhost URL -: http://localhost:5095/api/v1/Booking
## Input:
  Version : 1
  
  Sample request body:
  
    {
    "bookingTime": "13:12",
    "name": "jo"
    }
   
## Authentication 
Used new Auth0 platform sandbox for authorization. Please comment    [Authorize] attribute if any issue with token generation 
Use below to generate AccessToken.

`curl --request POST \
  --url https://dev-qsu6lbp3l3i1z1hv.au.auth0.com/oauth/token \
  --header 'content-type: application/json' \
  --data '{"client_id":"SzxsRcE0N0JK1SZVT4eC5JJ1FPKGacLq","client_secret":"4zLYkoRPSgUR6lPTue4cuVzxl44O-vvjFFynl5BuzqPV63EIJv3Q9lq119tEDphP","audience":"https://settlementservice.com/","grant_type":"client_credentials"}'`
 
 ## Integration testing
 
Please use [Postman Collection](https://github.com/rajeevpoulose/SettlementService/blob/master/SettlementService/Integration%20Testing/SettlementService.postman_collection.json) to test APIs 
 1. Use Token Request to generate Access token.
 2. Use Bearer Token Authentication to run Post API

## API versioning 
Two types of API versioning supported 

### URL versioning
 Use version in api url 
 
> Ex: api/***v1***/Booking) 
 
 ### custom headers versioning 
 
 Add custom header in the request 
 
> Ex. ***x-api-version=1*** 

## Unit testing 

xUnit and Moq used for unit testing. 
Test project SettlementService.Test contains unit test for both controller and service layers

## Data store

EntityFrameworkCore.InMemory is used to store data in-memory. Stored data will be lost on application restart. 


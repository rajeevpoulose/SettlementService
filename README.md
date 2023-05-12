# Settlement Service

API localhost URL -: http://localhost:5095/api/v1/Booking

   Sample request
   
   `curl --location --request POST 'http://localhost:5095/api/v1/Booking' \
--header 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IkM3dDVqZlpPOEtnSUhLSmtpSzdSdyJ9.eyJpc3MiOiJodHRwczovL2Rldi1xc3U2bGJwM2wzaTF6MWh2LmF1LmF1dGgwLmNvbS8iLCJzdWIiOiJTenhzUmNFME4wSksxU1pWVDRlQzVKSjFGUEtHYWNMcUBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly9zZXR0bGVtZW50c2VydmljZS5jb20vIiwiaWF0IjoxNjgzODA5OTA1LCJleHAiOjE2ODM4OTYzMDUsImF6cCI6IlN6eHNSY0UwTjBKSzFTWlZUNGVDNUpKMUZQS0dhY0xxIiwic2NvcGUiOiJ3cml0ZS5ib29raW5ncyIsImd0eSI6ImNsaWVudC1jcmVkZW50aWFscyJ9.oxWD98fVEEIG_K6Pos1FiVWJR0RY0-UAby0mZlBs9afUkcfIsj2U2JNbug-7TUP37ShYOUWRSjErlmMeKthD3nFS7_fQTu0whi5kyMnuVVnTeOMNDv8T9PICx4x6z0ake02JSxi6jGyviB1zxvtIw-PlCfvx0-fwU6e-y39WwTRaObkGcPsb6C5y3czvgd8_Ik3Ys1Mba_RMNSVWmvFiNQll2_lBAkcaVBNxXiiHW1oVViG2_QO9FNrN2Df86DHs0aXNDDvPNSDbnwJYpgBf2HT7WCi9F41oZjErCl06rJHoSgEB_wMqFcSVYGVk1V3tXsvyTLcEkM914l4SnX0isg' \
--header 'Content-Type: application/json' \
--data-raw '{
  "bookingTime": "13:12",
  "name": "jo"
}'`
## Input:
  Version : 1
  
  Sample request body:
  
    {
    "bookingTime": "13:12",
    "name": "jo"
    }

## Authentication 
Used new Auth0 platform sandbox for authorization. Please comment [Authorize] attribute to bypass authentication.

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


Feature: APIFramework_Examples
Example tests against the public available API endpoints

@APIFramework_Tests_Star_Wars @Working
Scenario: API_Framework_VerifyStatus200
	Given api url https://swapi.dev/api
	And path people
	And path 1
	When method GET
	Then assert api response status is equal to 200

@APIFramework_Tests_Star_Wars @Working
Scenario: API_Framework_VerifyStatus404
	Given api url https://swapi.dev/api
	And path planets
	And path 123
	When method GET
	Then assert api response status is equal to 404

@APIFramework_Tests_Star_Wars @Working
Scenario: API_Framework_VerifyLukeInformationWithVariables
	# Test Variables
	Given var %name% as Luke Skywalker type string
	And var %id% as 1 type int
	And var %homePlanet% as Tatooine type string
	# GET - Star Wars People API
	Given api url https://swapi.dev/api
	And path people
	And path %id%
	When method GET
	Then assert json response name is equal to %name%
	And assert json response gender is equal to male
	# SET planets api url from previous response
	Given var %planetUrl% is equal to response.homeworld type string
	# GET - using %planetUrl% variable
	Given api url %planetUrl%
	When method GET
	Then assert api response status is equal to 200
	Then assert json response name is equal to %homePlanet%

@APIFramework_BearerToken_Documentation @NotWorking
# NOTE: Test is not operational and is used as documentation only. you will need to replace with your Authorization Token and form content from twitter
Scenario: Bearer_Token_Documentation
	Given api url https://api.twitter.com
	And header Authorization = Basic ABCDEFGHI
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value client_credentials
	# Additional form fields would be need to be added
	When method POST
	Then assert api response status is equal to 200
	# SET %bearer% variable to access_token response
	# example: {"token_type":"bearer","access_token":"AAAA%2FAAA%3DAAAAAAAA"}
	Given var %bearer% as response.access_token type string
	# SET the Authorization header to Bearer with the %bearer% variable
	Given api url https://api.twitter.com
	And header Authorization = Bearer %bearer%
	When method GET
	Then assert api response status is equal to 200

@APIFramework_Tests_Swagger_PetShop @Working
Scenario: Swagger_Petshop_CRUD
	# NOTE: The petshop API is throttled by SmartBear. It may take a couple of runs to complete successfully if the endpoint is blocking connections
	#
	#Delete
	Given api url https://petstore.swagger.io/v2/pet
	And path 1061
	And header Content-Type = application/json
	When method DELETE
	#Create
	Given api url https://petstore.swagger.io/v2/pet
	And request json { 'id': 1061, 'category': {'id': 0,  'name': 'string' }, 'name': 'test pet', 'photoUrls': [ 'string'], 'tags': [   {    'id': 0,    'name': 'string'   } ], 'status': 'available'}
	And header Content-Type = application/json
	When method POST
	Then assert json response id is equal to 1061
	Given var %id% is equal to response.id type string
	Then print var %id% debug
	#Read
	Given api url https://petstore.swagger.io/v2/pet
	And path %id%
	And header Content-Type = application/json
	When method GET
	Then assert api response status is equal to 200
	#	Example assert against a variable/token
	And assert json response id is equal to %id%
	#Update
	Given api url https://petstore.swagger.io/v2/pet
	#	Example reading a json model file with a variable
	And request read json file .\\Data\\JsonData\\ApiData\\Swagger\\JsonRequest\\PetShopTestData.json
	And header Content-Type = application/json
	When method PUT
	Then assert api response status is equal to 200
	And assert json response tags[0].id is equal to 1061
	And assert json response name is equal to doggie update
	#	Example of asserting response against a json response file
	And assert json response matches json file .\\Data\\JsonData\\ApiData\\Swagger\\JsonResponse\\SwaggerPetstore_POST_pet_response.json
	#	Example of asserting response against a json schema file for contract testing
	And assert json response matches schema file .\\Data\\JsonData\\ApiData\\Swagger\\JsonSchemas\\SwaggerPetstore_POST_pet_schema.json
	#Delete
	Given api url https://petstore.swagger.io/v2/pet
	And path 1061
	And header Content-Type = application/json
	When method DELETE
	Then assert api response status is equal to 200

@APIFramework_Tests_Swagger_PetShop @Working
Scenario: Swagger_Petshop_CRUD_Failing_Contract_Example
	# NOTE: The petshop API is throttled by SmartBear. It may take a couple of runs to complete successfully if the endpoint is blocking connections
	#
	#Delete
	Given api url https://petstore.swagger.io/v2/pet
	And path 1061
	And header Content-Type = application/json
	When method DELETE
	#Create
	Given api url https://petstore.swagger.io/v2/pet
	And request json { 'id': 1061, 'category': {'id': 0,  'name': 'string' }, 'name': 'test pet', 'photoUrls': [ 'string'], 'tags': [   {    'id': 0,    'name': 'string'   } ], 'status': 'available'}
	And header Content-Type = application/json
	When method POST
	Then assert json response id is equal to 1061
	Given var %id% is equal to response.id type string
	Then print var %id% debug
	#Read
	Given api url https://petstore.swagger.io/v2/pet
	And path %id%
	And header Content-Type = application/json
	When method GET
	Then assert api response status is equal to 200
	#	Example assert against a variable/token
	And assert json response id is equal to %id%
	#Update
	Given api url https://petstore.swagger.io/v2/pet
	#	Example reading a json model file with a variable
	And request read json file .\\Data\\JsonData\\ApiData\\Swagger\\JsonRequest\\PetShopTestData.json
	And header Content-Type = application/json
	When method PUT
	Then assert api response status is equal to 200
	And assert json response tags[0].id is equal to 1061
	And assert json response name is equal to doggie update
	#	Example of asserting response against a json response file
	And assert json response matches json file .\\Data\\JsonData\\ApiData\\Swagger\\JsonResponse\\SwaggerPetstore_POST_pet_response.json
	#	Example of asserting response against a json schema file for contract testing
	And assert json response matches schema file .\\Data\\JsonData\\ApiData\\Swagger\\JsonSchemas\\SwaggerPetstore_POST_pet_schema_fail.json
	#	Should see reference to a required status2 property that is not in the response


@APIFramework_Tests_Swagger_PetShop @NotWorking
Scenario: Date_Examples
	Given var %timestamp% as now type date
	# UTC alternative
	#Given var %timestamp% as utc type date
	And format var %timestamp% as string MM-dd-yyyy
	And var %id% as 3fa85f64-5717-4562-b3fc-2c963f66afa6 type string
	And request json { "Id": "%id%", "eventType": "string", "timestamp": "%timestamp%", "event": "string", "eventData": [ { "name": "string", "value": "string"} ], "location": { "Id": "%id%", "timestamp": "%timestamp%", "latitude": 0, "longitude": 0, "accuracy": 0, "altitude": 0, "altitudeAccuracy": 0, "direction": 0, "speed": 0, "satellite": 0, "csq": 0, "address": "string", "fix": 0, "locationMode": "string" } }
	Then print var %timestamp% debug
	And print var %id% debug


@APIFramework_Tests_SQLServer_Example @NotWorking
Scenario: SQL_Request_Example
	Given var %key% as 218 type int
	And request sqlserver select top 1 *  from table with (nolock) where key = %key%
	And sqlserver connection string Data Source=SQL Server Connection string
	When method sqlserver
	Then assert json response sqlresult[0].key is equal to %key%
	Then print response debug
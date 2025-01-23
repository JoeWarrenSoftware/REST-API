# REST-API
Training module for developing REST APIs

## RE-presentational State Transfer (REST)
( An architectural style for building distributed hypermedia systems )
 Alternatives include GraphQL and gRPC

 ## 6 Constraints of REST:
 ### 1. Uniform interface (clearly defined interface between client and server)
 - Identification of resources (data model. e.g. movie, customers)
 - Manipulation of resources through representations (clients can use the provided representations of the model to alter the states on the server)
 - Self-description messages (each resource should provide all the information it needs for the message to be processed)
 - Hypermedia as the engine of application state (clients only need initial URL to get all further data required through future server responses)

 ### 2. Stateless
 The client-sent message needs to provide all the information the server needs to process the request. Any state-related data needs to be kept on the client as server requests may be distributed among load balancers.

 ### 3. Cacheable
 The server should communicate to the client whether a response is cacheable and for how long it will be if so. Clients must explitly state whether they want to bypass this caching in their requests using HTTP headers.

 ### 4. Client-Server
 Clients and Servers can change as much as they want independently so long as they both conform to the contract set between them for how API communicarequests/responses will be exchanged.

 ### 5. Layered System
 The client isn't allowed to know if it's communication directly to the root server, or some load balancer proxy.

 ### 6. Code on demand (optional)
 The server is allowed to send raw executable code (typically Javascript) for the client to run directly. Considered obsolete or high-risk and typically ignored by modern APIs.

 ## Resource Naming and Routing

 The routing should describe the resources (or models) you're accesssing. For a collection the name should be plural and end with an 's' to describe a many-object definition. i.e. "Games" not "Game"

 - GET /games
 - GET /games/1
 - GET /games/1/ratings
 - GET /ratings/me
 - DELETE /games/1/ratings

 ## HTTP Verbs
 These describe the action happening to the resource

 POST - Create new
 GET - Retrieve existing
 PUT - Complete Update existing
 PATCH - Partial update (not used much anymore)
 DELETE - Remove existing

 ## HTTP Response Codes

 POST
 - Single resource (/items/1): N/A
 - Collection resource (/items): 201 (Created)

 GET
 - Single resource (/items/1): 200 (OK), 404 (Not Found)
 - Collection resource (/items): 200 (Success)

 PUT
 - Single resource (/items/1): 200 (OK), 204 (No Content), 404 (Not Found)
 - Collection resource (/items): 405 (Method not allowed)

 DELETE
 - Single resource (/items/1): 200, 404 (Not Found)
 - Collection resource (/items): 405 (Method not allowed)

 ## HTTP Response Body
 (Normally JSON, but can be XML if client requests that)

 JSON Example:
 {
    "Name": "Mark Smith"
 }

 ## Item Idempotency

An idempotent HTTP method is a method that can be invoked many times without different outcomes. It should not matter if the method has been called only once, or ten times over. The result should always be the same.

Safe methods are those that will not change the state on the backend.

 |HTTP Method| Idempotent |Safe|
| :---:   | :---: | :---: |
| Get |✅|✅|
| Head |✅|✅|
| Put |✅|❌|
| Delete |✅|❌|
| Post |❌|❌|
| Patch |❌|❌|

## HATEOAS (Hypermedia as the engine of Application state)

This is the principle that response bodies to requests can contain data to make further requests about the responded resource. In the below example that is the response to a GET on a department. Employees route is provided in case client wants to look into the department further. Typically not required where APIs have good documentation as it has a performance hit on the API to provide this.

{
    "departmentId": 7,
    "departmentName": "Engineering",
    "locationId": 120,
    "managerId": 55,
    "links":[
        {
            "href: "7/employees",
            "rel": "employees",
            "type": "GET
        }
    ]
}

## Errors and Faults
HTTP Response 4XX Errors = Client has provided invalid data
HTTP Response 5XX Faults = Client had valid request, but Server has an issue/bug


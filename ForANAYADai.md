# GroceryApi
api for my desktop application

Username & Password for login:

username : aakash

password: dahal

OR

username:admin

password: password

I have created the Web api . 
Here I have used my existing database and I used Database first EF for getting models and context class.

I then created two api controllers with r/w action with EF to create sales and product controllers.

Since, there was no key in my login table,scaffolding was not possible , I added empty api controller and added post method for login/authorization.

Login controller validates the login and generates the JWT token.

and I added some code in Program.cs to add authentication and add authorize header in Swagger UI.

After generating token and authorizing in swagger Api , you can do operation in Sales and Product Schemas.

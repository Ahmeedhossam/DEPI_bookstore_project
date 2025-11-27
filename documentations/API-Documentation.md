API Documentation
GET /books
•	Returns list of books.
•	Query:
•	search (optional): filter by title or author.
•	Status:
•	200 OK
GET /books/{id}
•	Returns book details by id.
•	Status:
•	200 OK
•	404 Not Found
POST /cart/add
•	Adds a book to cart.
•	Body:
•	{ "bookId": number, "quantity": number }
•	Status:
•	200 OK
•	400 Bad Request
•	404 Not Found
•	409 Conflict
Extra endpoints available in the project
GET /account/register
•	Returns register page.
POST /account/register
•	Creates a new user.
•	Body:
•	{ "firstName": string, "lastName": string, "email": string, "phoneNumber": string, "address": string?, "password": string, "confirmPassword": string }
•	Status:
•	302 Redirect (to /account/login on success)
•	400 Bad Request (validation errors)
GET /account/login
•	Returns login page.
POST /account/login
•	Logs in a user.
•	Body:
•	{ "email": string, "password": string, "rememberMe": bool }
•	Status:
•	302 Redirect (to /home/index on success or returnUrl)
•	400 Bad Request (invalid credentials)
•	423 Locked (locked out)
POST /account/logout
•	Logs out current user.
•	Status:
•	302 Redirect (to /home/index)
GET /account/accessdenied
•	Returns access denied page.
GET /book
•	Returns books list page (HTML) with search support.
•	Query:
•	search (optional)
GET /book/details/{id}
•	Returns book details page (HTML).
GET /book/create
•	Returns create book page (HTML).
POST /book/create
•	Creates a new book.
•	Body:
•	{ "title": string, "author": string, "price": decimal, "description": string, "publishedDate": string (date), "copiesAvailable": int, "coverImageUrl": string (url) }
•	Status:
•	302 Redirect (to /book)
GET /book/edit/{id}
•	Returns edit book page (HTML).
POST /book/edit/{id}
•	Updates an existing book.
•	Status:
•	302 Redirect (to /book)
GET /book/delete/{id}
•	Returns delete confirmation page (HTML).
POST /book/delete/{id}
•	Deletes a book.
•	Status:
•	302 Redirect (to /book)
GET /home/index
•	Returns home page (HTML).
GET /home/privacy
•	Returns privacy page (HTML).
Notes
•	Books search: matches title or author.
•	Forms use AntiForgery and validation.
•	Authentication required for actions after login; public browsing is allowed.

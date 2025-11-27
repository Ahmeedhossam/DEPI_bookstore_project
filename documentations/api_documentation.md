# API Documentation

## GET /books
- Returns list of books.
- Query:
  - search (optional): filter by title or author.
- Status:
  - 200 OK

## GET /books/{id}
- Returns book details by id.
- Status:
  - 200 OK
  - 404 Not Found

## POST /cart/add
- Adds a book to cart.
- Body:
  { "bookId": number, "quantity": number }
- Status:
  - 200 OK
  - 400 Bad Request
  - 404 Not Found
  - 409 Conflict

## GET /account/register
- Returns register page.

## POST /account/register
- Creates a new user.
- Body:
  { "firstName": string, "lastName": string, "email": string, "phoneNumber": string, "address": string?, "password": string, "confirmPassword": string }
- Status:
  - 302 Redirect (to /account/login on success)
  - 400 Bad Request

## GET /account/login
- Returns login page.

## POST /account/login
- Logs in a user.
- Body:
  { "email": string, "password": string, "rememberMe": bool }
- Status:
  - 302 Redirect
  - 400 Bad Request
  - 423 Locked

## POST /account/logout
- Logs out current user.
- Status:
  - 302 Redirect

## GET /account/accessdenied
- Returns access denied page.

## GET /book
- Returns books list page (HTML) with search support.
- Query:
  - search (optional)

## GET /book/details/{id}
- Returns book details page.

## GET /book/create
- Returns create book page.

## POST /book/create
- Creates a new book.
- Body:
  { "title": string, "author": string, "price": decimal, "description": string, "publishedDate": string, "copiesAvailable": int, "coverImageUrl": string }
- Status:
  - 302 Redirect

## GET /book/edit/{id}
- Returns edit book page.

## POST /book/edit/{id}
- Updates an existing book.
- Status:
  - 302 Redirect

## GET /book/delete/{id}
- Returns delete confirmation page.

## POST /book/delete/{id}
- Deletes a book.
- Status:
  - 302 Redirect

## GET /home/index
- Returns home page.

## GET /home/privacy
- Returns privacy page.

## Notes
- Books search matches title or author.
- Forms use AntiForgery and validation.
- Authentication required for actions after login; public browsing is allowed.

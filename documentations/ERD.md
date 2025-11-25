# ERD Description

Entities:
- **User(UserId, Name, Email, PasswordHash)**
- **Book(BookId, Title, Author, Price, Category)**
- **Order(OrderId, UserId, TotalAmount, Date)**
- **OrderItem(OrderItemId, OrderId, BookId, Quantity)**

Relationships:
- User 1..* Orders
- Order 1..* OrderItems
- Book 1..* OrderItems
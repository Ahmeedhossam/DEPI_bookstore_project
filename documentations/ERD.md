# ERD Description

Entities:

Relationships:

# ERD Description (Mermaid)

```mermaid
erDiagram
	USER {
		int UserId PK
		string Name
		string Email
		string PasswordHash
	}
	BOOK {
		int BookId PK
		string Title
		string Author
		float Price
		string Category
	}
	"ORDER" {
		int OrderId PK
		int UserId FK
		float TotalAmount
		date Date
	}
	ORDER_ITEM {
		int OrderItemId PK
		int OrderId FK
		int BookId FK
		int Quantity
	}

	USER ||--o{ "ORDER" : places
	"ORDER" ||--|{ ORDER_ITEM : contains
	BOOK ||--o{ ORDER_ITEM : "included_in"
```

Notes:
- Table names are uppercased to avoid reserved-keyword collisions (e.g., `order`).
- Render this file in a Markdown viewer that supports Mermaid to see the diagram.
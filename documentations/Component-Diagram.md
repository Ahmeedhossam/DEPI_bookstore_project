# Component Diagram

```mermaid
graph TB
    subgraph "Online Book Store System"
        UI[UI Layer - Views]
        Controller[Controller Layer]
        Service[Service Layer]
        Repository[Repository Layer]
        DB[(Database)]
    end
    
    UI --> Controller
    Controller --> Service
    Service --> Repository
    Repository --> DB
```

## Components:
- **UI Layer (Views)** - User interface and presentation layer
- **Controller Layer** - Handles HTTP requests and responses
- **Service Layer** - Business logic and operations
- **Repository Layer** - Data access abstraction
- **Database** - Data storage and persistence

**Dependencies:** UI → Controller → Service → Repository → DB


# Deployment Diagram

```mermaid
graph TB
    subgraph ClientTier ["Client Tier"]
        Browser["Client Browser<br/>(User Interface)"]
    end
    
    subgraph WebTier ["Web Tier"]
        WebServer["Web Server - IIS<br/>(Static content & routing)"]
    end
    
    subgraph AppTier ["Application Tier"]
        App["ASP.NET MVC Application<br/>(Business logic & processing)"]
    end
    
    subgraph DBTier ["Database Tier"]
        SQLServer[("SQL Server Database<br/>(Data persistence)")]
    end
    
    Browser -.->|HTTPS| WebServer
    WebServer --> App
    App --> SQLServer
```

## Architecture Components:
- **Client Browser** - User interface
- **Web Server (IIS)** - Handles HTTP requests and static content
- **Application (ASP.NET MVC)** - Business logic and processing
- **SQL Server Database** - Data storage and persistence

**Connections:** All communications via HTTPS for security



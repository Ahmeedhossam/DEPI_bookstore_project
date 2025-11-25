# State Diagram - Order States

```mermaid
stateDiagram-v2
    [*] --> Created
    Created --> PendingPayment : User confirms order
    PendingPayment --> Paid : Payment successful
    PendingPayment --> Cancelled : Payment failed/cancelled
    Paid --> Shipped : Order dispatched
    Shipped --> Delivered : Order received
    Delivered --> [*]
    Cancelled --> [*]
    
    Created : Order initially created
    PendingPayment : Awaiting payment confirmation
    Paid : Payment confirmed, ready to ship
    Shipped : Order in transit
    Delivered : Order completed successfully
```

## Order States Flow:
- **Created** → **Pending Payment** → **Paid** → **Shipped** → **Delivered**
- Alternative path: **Pending Payment** → **Cancelled** (if payment fails)
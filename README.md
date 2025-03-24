[![build](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml/badge.svg)](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml)

A beautiful journey to writing **wise code that works**
- **`The source code structure`** should be as clear as **`a book’s table of contents`**, making it easy to understand the domain and system.
- **`Test code`** should act as **`a manual`** for understanding business rules.

<br/>


## Application Architecture

### Internal Architecture (Hexagonal Architecture)
![hexagonal architecture](./01-architecture/part1-overview/ch04-internal-architecture/.images/Architecture.Internal.Hexagonal.png)

### External Architecture
> TODO

<br/>

## Hands-on Labs
I restructured '[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/?ref=dometrain-github&promo=getting-started-domain-driven-design)' based on the design principles and practices I defined.

### Goal
- Understand code structuring for sustainable software development.
- Learn tactical design that express domain knowledge as code.

### Table of Contents
- Part 1. VOC
  - [x] Chapter 01. [Domain Exploration](./03-tutorial/ddd/ch01-domain-exploration/)
  - [x] Chapter 02. [Domain Structuring](./03-tutorial/ddd/ch02-domain-structuring/)
  - [x] Chapter 03. [Usecase Exploration](./03-tutorial/ddd/ch03-usecase-exploration/)
  - [ ] Chapter 04. Usecase Pipeline
  - [ ] Chapter 05. Cucumber
- Part 2. Monolithic
  - [ ] Chapter 06. WebApi
  - [ ] Chapter 07. OpenTelemetry
  - [ ] Chapter 08. PostgreSQL
  - [ ] Chapter 09. Cache
  - [ ] Chapter 10. Containerization
- Part 3. Microservices
  - [ ] Chapter 11. Aspire
  - [ ] Chapter 12. RabbitMQ
  - [ ] Chapter 13. Resilience
  - [ ] Chapter 14. Reverse Proxy
  - [ ] Chapter 15. Chaos Engineering
- Part 4. Operations
  - [ ] Chapter 16. Feature Flag Management
  - [ ] Chapter 17. Observability System
  - [ ] Chapter 18. Infrastructure as Code

### Solution Design Principles

1. Separation
   - **`Concern`**: `Business Concerns` vs `Technical Concerns`
   - **`Goal`**: `Main Goals` vs `Accompanying Goals` (It refers to a goal that is naturally carried out or plays a supporting role in the process of achieving the main goal. 부수 목표: 주가 되는 것에 붙어 따르는 것)
1. Direction
   - **`Top`**: The more important thing from a technical concern(Accompanying Goal).
   - **`Down`**: The more important thing from a business concern(Main Goal).

<br/>

| Direction | Separation of `Concerns`        | Separation of `Goals`                                           |
| ---       | ---                             | ---                                                             |
| `Top`     | Technical Concerns (_Infinite_) | Accompanying Goals (_Infinite_ -**_Abstractions_**-> _Finite_)  |
| `Down`    | Business Concerns (_Finite_)    | Main Goals (_Finite_)                                           |

- To intuitively understand the main goals of a layer, accompanying goals are placed inside the Abstractions folder, leaving only the main goals at the top level.
- This helps clearly distinguish between the main and accompanying goals, making them easier to understand.

```shell
{T}
├─Src
│  ├─{T}                          // Host               > Top: Technical Concerns (Accompanying Goal)
│  ├─{T}.Adapters.Infrastructure  // Adapter Layer      >  │
│  ├─{T}.Adapters.Persistence     // Adapter Layer      >  │
│  ├─{T}.Application              // Application Layer  >  ↓
│  └─{T}.Domain                   // Domain Layer       > Down: Business Concerns (Main Goal)
│     │
│     ├─Abstractions                                    > Top: Technical Concerns (Accompanying Goal)
│     │                                                 >  ↓
│     └─AggregateRoots                                  > Down: Business Concerns (Main Goal)
│
└─Tests
   ├─{T}.Tests.Integration        // Integration Test   > Top: Technical Concerns (Accompanying Goal)
   ├─{T}.Tests.Performance        // Performance Test   >  ↓
   └─{T}.Tests.Unit               // Unit Test          > Down: Business Concerns (Main Goal)
```

![](./03-tutorial/ddd/.images/SolutionDesignExample.png)

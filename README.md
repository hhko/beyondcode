[![build](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml/badge.svg)](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml)

A beautiful journey to writing wise code
- **`The source code structure`** should be as clear as **`a table of contents in a book`** to help understand the system.
- **`Test code`** should serve as **`a manual`** for understanding business rules.

<br/>

## Application Architecture

### Internal Architecture(Hexagonal Architecture)
![](./01-architecture/part1-overview/ch04-internal-architecture/.images/Architecture.Internal.png)

### External Architecture
> TODO

<br/>

## Domain-Driven Design Tutorial
I have restructured it based on **the design principles I defined**, using '[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/?ref=dometrain-github&promo=getting-started-domain-driven-design)' as a foundation.

### Goal
- Understand code structuring for sustainable software development.
- Learn tactical design that express domain knowledge as code.

### Table of Contents
- Part 1. VOC
  - [x] Chapter 01. [Domain Exploration](./03-tutorial/ddd/ch01-domain-exploration/)
  - [x] Chapter 02. [Domain Structuring](./03-tutorial/ddd/ch02-domain-structuring/)
  - [x] Chapter 03. [Usecase Exploration](./03-tutorial/ddd/ch03-usecase-exploration/)
  - [ ] Chapter 04. Usecase Pipeline
- Part 2. Monolithic
  - [ ] Chapter 05. WebApi
  - [ ] Chapter 06. OpenTelemetry
  - [ ] Chapter 07. PostgreSQL
  - [ ] Chapter 08. Cache
  - [ ] Chapter 09. Containerization
- Part 3. Microservices
  - [ ] Chapter 10. RabbitMQ
  - [ ] Chapter 11. Reverse Proxy
  - [ ] Chapter 12. Resilience
  - [ ] Chapter 13. Chaos Engineering
- Part 4. Deployment
  - [ ] Chapter 14. Feature Flag Management
  - [ ] Chapter 15. Infrastructure as Code

### Solution Design Principles

1. **Separation**
   - **Concern**: `Domain Concern` vs `Technical Concern`
   - **Goal**: `Main Goal` vs `Accompanying Goal`(It refers to a goal that is naturally carried out or plays a supporting role in the process of achieving the main goal. 부수 목표: 주가 되는 것에 붙어 따르는 것)
1. **Direction**
   - **Top**: The more important thing from a technical aspect(Accompanying Goal)
   - **Down**: The more important thing from a business aspect(Main Goal)

<br/>

> | `Direction` | Separation of `Concerns`    | Separation of `Goals`                     |
> | ---         | ---                         | ---                                             |
> | Top         | Technical Concern(Infinite) | Accompanying Goal(Infinite -Abstractions-> Finite) |
> | Down        | Domain Concern(Finite)      | Main Goal(Finite)                         |
>
> - To intuitively understand the main and  goals of a layer, accompanying goals are placed inside the Abstractions folder, leaving only the main goals at the top level.
> - This helps clearly distinguish between the main and accompanying goals, making them easier to understand.

```shell
{T}
├─Src
│  ├─{T}                          // Host               > Top: The more important thing from a technical aspect(Accompanying Goal)
│  ├─{T}.Adapters.Infrastructure  // Adapter Layer      > │
│  ├─{T}.Adapters.Persistence     // Adapter Layer      > │
│  ├─{T}.Application              // Application Layer  > ↓
│  └─{T}.Domain                   // Domain Layer       > Down: he more important thing from a business aspect(Main Goal)
│     │
│     ├─Abstractions                                    > Top: The more important thing from a technical aspect(Accompanying Goal)
│     │                                                 > ↓
│     └─AggregateRoots                                  > Down: he more important thing from a business aspect(Main Goal)
│
└─Tests
   ├─{T}.Tests.Integration        // Integration Test   > Top: The more important thing from a technical aspect(Accompanying Goal)
   ├─{T}.Tests.Performance        // Performance Test   > ↓
   └─{T}.Tests.Unit               // Unit Test          > Down: he more important thing from a business aspect(Main Goal)
```

![](./03-tutorial/ddd/.images/SolutionDesignExample.png)
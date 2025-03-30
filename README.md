[![build](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml/badge.svg)](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml)

[![Build VitePress](https://github.com/hhko/beyondcode/actions/workflows/build-vitepress.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-vitepress.yml)
[![Build C# Template](https://github.com/hhko/beyondcode/actions/workflows/build-template.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-template.yml)
[![Build C# Gym](https://github.com/hhko/beyondcode/actions/workflows/build-gym.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-gym.yml)

> Make It Work, Make It Right, Make It Fast

A beautiful journey to writing **wise code that works**
- **`The source code structure`** should be as clear as **`a book’s table of contents`**, making it easy to understand the domain and system.
- **`Test code`** should act as **`a manual`** for understanding business rules.

<br/>

## Application Architecture

### Architecture Tech. Map
![](./.images/ArchitectureTechMap.png)

### Internal Architecture (Hexagonal Architecture)
![hexagonal architecture](./01-architecture/part1-overview/ch03-internal-architecture/.images/Architecture.Internal.Hexagonal.png)

### External Architecture
> TODO

<br/>

## Hands-on Labs
I restructured '[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/?ref=dometrain-github&promo=getting-started-domain-driven-design)' based on the design principles and practices I defined.

### Goal
- Understand code structuring for sustainable software development.
- Learn tactical design that express domain knowledge as code.

### Table of Contents
- Part 1. Domain
  - [ ] Chapter 01. [Domain Glossary](./02-tutorial/ddd/ch01-domain-glossary/index.md)
  - [x] Chapter 02. [Domain Exploration](./02-tutorial/ddd/ch02-domain-exploration/index.md)
  - [ ] Chapter 03. Domain Structuring
  - [ ] Chapter 04. Domain Test
- Part 2. Use Case
  - [ ] Chapter 05. Use Case Exploration
  - [ ] Chapter 06. Use Case Pipeline
  - [ ] Chapter 07. Use Case Test(Cucumber)
- Part 2. Monolithic
  - [ ] Chapter 08. WebApi
  - [ ] Chapter 09. OpenTelemetry
  - [ ] Chapter 10. PostgreSQL
  - [ ] Chapter 11. Cache
  - [ ] Chapter 12. Containerization
- Part 3. Microservices
  - [ ] Chapter 13. Aspire
  - [ ] Chapter 14. RabbitMQ
  - [ ] Chapter 15. Resilience
  - [ ] Chapter 16. Reverse Proxy
  - [ ] Chapter 17. Chaos Engineering
- Part 4. Operations
  - [ ] Chapter 18. OpenFeature(Feature Flag Management)
  - [ ] Chapter 19. OpenSearch(Observability System)
  - [ ] Chapter 20. Ansible(Infrastructure as Code)
  - [ ] Chapter 21. Backstage(Building developer portals)

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

![](./02-tutorial/ddd/.images/SolutionDesignExample.png)

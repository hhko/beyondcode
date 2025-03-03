[![build](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml/badge.svg)](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml)

> A Beautiful Journey to Writing Wise Code
> - **`The structure`** of the source code should be as clear as **`a table of contents in a book`** to help understand the system.
> - **`Test code`** should serve as **`a manual`** for understanding business rules.

<br/>

# Application Architecture

## Internal Architecture
![](./01-architecture/part1-overview/ch04-internal-architecture/.images/Architecture.Internal.png)

## External Architecture
> TODO

<br/>

# Tutorial
## Domain-Driven Design Basic

> This has been restructured based on "[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/?ref=dometrain-github&promo=getting-started-domain-driven-design)".

### Goal
- Understand code structuring for sustainable software development.
- Learn design patterns that express domain knowledge as code.

### Table of Contents
- Part 1. Business Concern
  - [ ] Chapter 01. Domain Exploration
  - [ ] Chapter 02. Deeper Domain Exploration
  - [ ] Chapter 03. Use Case(DTO)
  - [ ] Chapter 04. Factory(Error)
- Part 2. Host Technical Concern
  - [ ] Chapter 05. Host(Option)
  - [ ] Chapter 06. Container(Service Discovery)
  - [ ] Chapter 07. OpenTelemetry
  - [ ] Chapter 08. Resilience
- Part 3. Input/Output Technical Concern
  - [ ] Chapter 09. WebApi
  - [ ] Chapter 10. PostgreSQL
  - [ ] Chapter 11. RabbitMQ
  - [ ] Chapter 12. Reverse Proxy

### Solution Design Principles

1. **Separation**
   - **Concern**: `Business Concern` vs `Technical Concern`
   - **Goal**: `Main Goal` vs `Sub-Goal`(something supplementary to the main goal, 부수 목표: 주가 되는 것에 붙어 따르는 것)
1. **Direction**
   - **Up**: The more important thing from a technical aspect(Sub-Goal)
   - **Down**: The more important thing from a business aspect(Main Goal)

<br/>

| `Direction`  | `Separation` of Concerns | `Separation` of Goals                         |
| --- | --- | --- |
| Up    | Technical Concern(Infinite)   | Sub-Goal(Infinite -Abstractions-> Finite)   |
| Down  | Business Concern(Finite)      | Main Goal(Finite)                           |

- To transform the infinite nature of sub-goals into a finite structure, an `Abstractions` top-level folder is introduced, with sub-goals placed in sub-folders beneath it.
- This ensures a clear separation between sub-goals and the main goal, making all folders, except for the `Abstractions` folder at the top, more intuitively understood as part of the main goal."

```
{T}
├─Src
│  ├─{T}                          // Host               > Up: The more important thing from a technical aspect(Sub-Goal)
│  ├─{T}.Adapters.Infrastructure  // Adapter Layer      > │
│  ├─{T}.Adapters.Persistence     // Adapter Layer      > │
│  ├─{T}.Application              // Application Layer  > ↓
│  └─{T}.Domain                   // Domain Layer       > Down: he more important thing from a business aspect(Main Goal)
│     │
│     ├─Abstractions                                    > Up: The more important thing from a technical aspect(Sub-Goal)
│     │                                                 > ↓
│     └─AggregateRoots                                  > Down: he more important thing from a business aspect(Main Goal)
│
└─Tests
   ├─{T}..Tests.Integration       // Integration Test   > Up: The more important thing from a technical aspect(Sub-Goal)
   ├─{T}..Tests.Performance       // Performance Test   > ↓
   └─{T}..Tests.Unit              // Unit Test          > Down: he more important thing from a business aspect(Main Goal)
```
- {T}: Service

![](./03-tutorial/ddd-basic/.images/SolutionDesignExample.png)

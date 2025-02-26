# Domain-Driven Design Basic

> This has been restructured based on "[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/?ref=dometrain-github&promo=getting-started-domain-driven-design)".

## Goal
- Understand code organization for sustainable software development.
- Learn design patterns that express domain knowledge in code.

## Table of Contents
- Part 1. Business Concern
  - [ ] Chapter 1. Domain Exploration
  - [ ] Chapter 2. Deeper Domain Exploration
  - [ ] Chapter 3. Use Case
- Part 2. Technical Concern
  - [ ] Chapter 4. Infrastructure(WebApi)
  - [ ] Chapter 5. Persistence(Db)
  - [ ] Chapter 6. Service

## Solution Design Principles

| `Direction`  | Separation of Concerns | Separation of Goals                         |
| --- | --- | --- |
| Up    | Technical Concern(Infinite)   | Sub-Goal(Infinite -Abstractions-> Finite)   |
| Down  | Business Concern(Finite)      | Main Goal(Finite)                           |

- **Separation**
  - **Concern**: `Business Concern` vs `Technical Concern`
  - **Goal**: `Main Goal` vs `Sub-Goal`(something supplementary to the main goal, 부수 목표: 주가 되는 것에 붙어 따르는 것)
- **Direction**
  - **Up**: The more important thing from a technical aspect(sub-goal: something supplementary to the main goal)
  - **Down**: The more important thing from a business aspect(main goal)

```
{T3}
├─Src
│  ├─{T1}.{T2}.{T3}                           // Host               > Up: The more important thing from a technical aspect(sub-goal)
│  ├─{T1}.{T2}.{T3}.Adapters.Infrastructure   // Adapter Layer      > │
│  ├─{T1}.{T2}.{T3}.Adapters.Persistence      // Adapter Layer      > │
│  ├─{T1}.{T2}.{T3}.Application               // Application Layer  > ↓
│  └─{T1}.{T2}.{T3}.Domain                    // Domain Layer       > Down: he more important thing from a business aspect(main goal)
│     ├─Abstractions                          //                    > Up: The more important thing from a technical aspect(sub-goal)
│     │                                                             > ↓
│     └─AggregateRoots                        //                    > Down: he more important thing from a business aspect(main goal)
│
└─Tests
   ├─{T1}.{T2}.{T3}.Tests.Integration        // Integration Test    > Up: The more important thing from a technical aspect(sub-goal)
   ├─{T1}.{T2}.{T3}.Tests.Performance        // Performance Test    > ↓
   └─{T1}.{T2}.{T3}.Tests.Unit               // Unit Test           > Down: he more important thing from a business aspect(main goal)
```
- {T1}: Corporation
- {T2}: Project
- {T3}: Service

![](./.images/SolutionDesignExample.png)

---
outline: deep
---

# 아키텍처

> wise code that works = `VOC` |> `DDD` |> `Architecture`  
> 동작하는 지혜로운 코드를 만들기 위한 아름다운 여정

- **소스 코드 구조는** 책의 목차처럼 명확하게 도메인과 시스템을 쉽게 이해할 수 있어야 합니다.
- **테스트 코드는** 비즈니스 규칙을 이해하기 위한 매뉴얼 역할을 해야 합니다.

## Application 아키텍처

### 아키텍처 기술 맵
![](./../../.images/ArchitectureTechMap.png)

### Internal 아키텍처 (Hexagonal Architecture)
![hexagonal architecture](./ch03-internal-architecture/.images/Architecture.Internal.Hexagonal.png)

### External 아키텍처
> TODO

## 분리
### 관심사의 분리
- 비즈니스 관심사
  - 비즈니스 흐름 관심사: 애플리케이션 레이어(Application Layer)
  - 비즈니스 단위 관심사: 도메인 레이어(Domain Layer)
- 기술 관심사
  - 인프라 관심사: 기술 레이어(Adapter Layer: Infrastructure)
  - 영속성 관심사: 기술 레이어(Adapter Layer: Persistence)
  - 화면 관심사: 기술 레이어(Adapter Layer: Presentation)

```
          [ 비즈니스 흐름 관심사 ]
              ↓             ↓
[ 기술 관심사 관심사 ]   [ 비즈니스 단위 관심사 ]
```
- 비즈니스 흐름 관심사(Application Layer)가 전체 관심사를 주관합니다.

### 목표의 분리
- 주요 목표
- 부수 목표(Abstractions): 주요 목표를 달성하는 과정에서 자연스럽게 함께 수행되거나 보조 역할을 하는 목표를 의미합니다.
  - 레이어의 주요 목표와 부수 목표를 직관적으로 이해할 수 있도록, 부수 목표는 Abstractions 폴더 안에 배치하고 최상위에는 주요 목표만 남도록 구성합니다.
  - 이를 통해 주요 목표와 부수 목표를 쉽게 구분하고 명확하게 이해할 수 있습니다.
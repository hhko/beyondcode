---
outline: deep
---

# Internal 아키텍처 개요

## 목표
- Internal 아키텍처의 레이어와 그 역할을 이해합니다.
- 아키텍처 트릴레마(캡슐화, 순수성, 성능) 관점에서 각 설계 요소의 필요성과 트레이드 오프를 확인합니다.

## 주요 키워드
- Internal 아키텍처 & External 아키텍처
- 관심사의 분리
- 레이어
- 순수 & 불순 함수

## 아키텍처 정의
![](./../../01-architecture/part1-overview/ch01-architecture/.images/Architecture.png)
- [소프트웨어 아키텍처의 중요성](https://www.youtube.com/watch?v=4E1BHTvhB7Y)

## 아키텍처 분류
![](./../../01-architecture/part1-overview/ch01-architecture/.images/Architecture.Category.png)
![](./../../01-architecture/part1-overview/ch01-architecture/.images/Architecture.Microservices.png)

- **External** 아키텍처: 프로세스 외부, **서비스 배치**
- **Internal** 아키텍처: 프로세스 내부, **레이어 배치**

## 아키텍처 원칙
![](./../../01-architecture/part1-overview/ch01-architecture/.images/Architecture.Principles.png)

- 관심사의 분리(Separation of concerns): 기술과 비즈니스를 분리한다.
  - 결정을 내리는 코드: 비즈니스(**순수 함수**: 숨은 입출력이 없는 함수)
  - 해당 결정에 따라 작용하는 코드: 기술(**불순 함수**: 숨은 입출력이 있는 함수)
- 관심사는 레이어로 관리합니다.
  - 비즈니스 레이어
    - 비즈니스 단위: Domain
    - 비즈니스 흐름: Application
  - 기술 레이어: Adapter






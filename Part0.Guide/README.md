# 개발 가이드

# Ch 14. 솔루션 레이어 의존성 주입


## Ch 11.1 테스트
- TODO 코드 커버리지
- TODO Fake 데이터
- TODO AutoFixture
- TODO Snapshot
- TODO ...

## Ch 11.2 컨테이너 테스트
- TODO PostgreSQL
- TODO RabbitMQ, ...

## Ch 11.3 통합 테스트
- TODO WebApi
- TODO RabbitMQ
- TODO FileSystem
- TODO 반복 작업
- TODO ...

## Ch 11.4 성능 테스트
- TODO ...

<br/>


# Ch 11. 솔루션 빌드
- TODO 코드 커버리지
- TODO 프로젝트 의존성 다이어그램
- TODO 코드 정적 분석?
- TODO EFCore 다이어그램
- TODO 빌드 조건?
  - 브랜치
  - PR
  - 대상 파일

<br/>

# Ch 12. 솔루션 배포
- TODO GitHub Release
- TODO GitHub Docker Image
- TODO 버전
- TODO Dockerfile 자동 생성
- TODO 이름 규칙
- TODO docker compose 솔루션 생성
- TODO Health Check
- TODO 진단 도구
- TODO 버전
  - 어셈블리
  - 도커 이미지

---

## 1. 레이어 구성
- [x] 레이어 정의
- [x] AssemblyReference.cs 파일  
- [x] Directory.Build.props 파일
- [x] 테스트 레이어 정의
- [x] 레이어 의존성 테스트
- [x] 레이어 다이어그램

## 2. 레이어 의존성 주입(옵션 패턴)
- [x] 옵션 패턴
- [x] 레이어 의존성 주입
- [x] 관찰 가능성 콘솔 로그
- [x] 콘솔 통합 테스트

## 3. 관찰 가능성
- [ ] 구조적 로그 Microsoft
- [ ] 구조적 로그 Microsoft, Error
- [ ] 구조적 로그 Microsoft, Exception
- [ ] 구조적 로그 Serilog
- [ ] 구조적 로그 Serilog, Error
- [ ] 구조적 로그 Serilog, Exception
- [ ] 관찰 가능성 추적
- [ ] 관찰 가능성 지표
- [ ] 관찰 가능성 로그 Aspire
- [ ] 관찰 가능성 로그 Grafana
- [ ] 관찰 가능성 로그 OpenSearch

## 4. 컨테이너
- [ ] 컨테이너 구성
- [ ] 컨테이너 HealthCheck


```
## 4. 연산 타입
- [ ] Error 타입
- [ ] IResult/IResult<T> 타입
- [ ] ValidationResult/ValidationResult<T> 타입
- [ ] Validation 로직
- [ ] Error 코드
- [ ] Exception 구조적 로그

## 5. Known 입출력
- [ ] CQRS 메시지 Meditor 패턴
- [ ] Command Decorator 패턴
- [ ] Query Decorator 패턴

## 6. Unknown 입출력
- [ ] IAdapter 인터페이스
- [ ] IAdapter Decorator 패턴

## 7. 데이터베이스
- [ ] DTO
- [ ] Repository 패턴
- [ ] Unit of Work 패턴
- [ ] ORM(Command Repository)
- [ ] SQL(Query Repository)

## 8. CI/CD
- [ ] 빌드 자동화
- [ ] 코드 품질 지표
- [ ] 의존성 다이어그램
- [ ] 배포 자동화

## 9. 호스트
- [ ] WebApi
- [ ] RabbitMQ
- [ ] 반복
- [ ] gRPC
- [ ] FileSystem
- [ ] FTP
- [ ] Time
- [ ] 캐시

## 10. 장애
- [ ] Retry
- [ ] 서킷 브레이커

## 11. 테스트
- [ ] 아키텍처 테스트
- [ ] 단위 테스트
- [ ] 통합 테스트
- [ ] 성능 테스트
- [ ] Fake 데이터
- [ ] Moq

## 12. 도메인 타입
- [ ] Entity
- [ ] ValueObject
- [ ] Enum
- [ ] Domain Service
- [ ] Aggregate Root
- [ ] Domain Event
- [ ] Factory 패턴
---
- [ ] SSG
---
- [ ] Specification 패턴
- [ ] Saga 패턴
- [ ] Outbox 패턴
- [ ] RabbitMQ
---
- [ ] 도메인 모델 패턴과 트랜잭션 스크립트 패턴
---
- [ ] Api Gateway?
- [ ] Aspire
- [ ] Dapr
```
# 함수형 리팩토링

## 개요
- 도메인 로직을 보다 **선언적이며 명확하고 검증 가능한 방식으로** 구현할 수 있습니다

<br/>

## 지침
- 함수 조합 (함수형: LINQ Query)
  ```
  from ... in ...
  ```
  - 유효성 검사 메서드 구분
    - 규칙: Ensure{조건}
    - 예제: EnsureAdminNotCreated
  - 명시적 이벤트 메서드 구분
    - 규칙: Raise{이벤트}Event
    - 예제: RaiseAdminProfileCreatedEvent
- 성공과 실패
  ```cs
  Fin<T>
  ```
- 도메인 에러 타입
  - 규칙: {레이어}Errors.{Aggregate Root 클래스}Errors.{에러 조건}.cs
  - 예제: DomainErrors.UserErrors.TrainerAlreadyCreated.cs
- 도메인 이벤트 타입
  - 규칙: {레이어}Events.{Aggregate Root 클래스}Events.{이벤트}Event.cs
  - 예제: DomainEvents.UserEvents.AdminProfileCreatedEvent.cs
- Map, Bind -> LINQ Query

<br/>

## 리팩토링
### 리팩토링 전
```cs
public ErrorOr<Guid> CreateAdminProfile()
{
  if (AdminId is not null)
  {
    return Error.Conflict(description: "User already has an admin profile");
  }

  AdminId = Guid.NewGuid();
  _domainEvents.Add(new AdminProfileCreatedEvent(Id, AdminId.Value));

  return AdminId.Value;
}
```

### 리팩토링 후 (함수형)
```cs
public Fin<Guid> CreateAdminProfile()
{
  return from _1 in EnsureAdminNotCreated(AdminId)
         let newAdminId = NewAdminId()
         from _2 in ApplyAdminProfileCreation(newAdminId)
         from _3 in RaiseAdminProfileCreatedEvent(newAdminId)
         select newAdminId;

  Fin<Unit> EnsureAdminNotCreated(Option<Guid> adminId) =>
    adminId.IsSome
      ? UserErrors.AdminAlreadyCreated(Id, (Guid)adminId)
      : unit;

  Guid NewAdminId() =>
    Guid.NewGuid();

  Fin<Unit> ApplyAdminProfileCreation(Guid newAdminId)
  {
    AdminId = newAdminId;
    return unit;
  }

  Fin<Unit> RaiseAdminProfileCreatedEvent(Guid newAdminId)
  {
    _domainEvents.Add(new UserEvents.AdminProfileCreatedEvent(Id, newAdminId));
    return unit;
  }
}

// 도메인 에러
//  - 규칙: {레이어}Errors.{Aggregate Root 클래스}Errors.{에러 조건}.cs
//  - 예제: DomainErrors.UserErrors.TrainerAlreadyCreated.cs
public static partial class DomainErrors
{
  public static partial class UserErrors
  {
    public static Error TrainerAlreadyCreated(Guid userId, Guid trainerId) =>
      ErrorCodeFactory.Create(
        $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(TrainerAlreadyCreated)}",
        $"User '{userId}' already has a trainer profile '{trainerId}'");
  }
}

// 도메인 이벤트
//  - 규칙: {레이어}Events.{Aggregate Root 클래스}Events.{이벤트}Event.cs
//  - 예제: DomainEvents.UserEvents.AdminProfileCreatedEvent.cs
public static partial class DomainEvents
{
    public static partial class UserEvents
    {
        public record AdminProfileCreatedEvent(
            Guid UserId,
            Guid AdminId)
            : IDomainEvent;
    }
}
```

## 리팩토링 방향
### 로컬 함수
- 로직을 함수 조합으로 표현합니다.

### Map vs. Bind
- `Map`: 순수 함수
  ```
  T -> R
  ```
- `Bind`: 불순 함수(부수 효과가 있는 함수)
  ```
  T -> Fin<R>
  ```
  - 멤버 변수 값 변경
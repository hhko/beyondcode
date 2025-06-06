
- CQRS 클래스 이름
  - 규칙: {유스케이스 이름}Command
  - 예제: CreateAdminProfileCommand
- CQRS 입력 메시지 이름
- CQRS 출력 메시지 이름
- DTO
  - 규칙: To{유스케이스 이름}Response
  - 예제: ToCreateAdminProfileResponse


## 리팩토링
### 리팩토링 전
```cs
public async Task<ErrorOr<Guid>> Handle(CreateAdminProfileCommand command, CancellationToken cancellationToken)
{
  var user = await _usersRepository.GetByIdAsync(command.UserId);
  if (user is null)
  {
    return Error.NotFound(description: "User not found");
  }

  var createAdminProfileResult = user.CreateAdminProfile();
  await _usersRepository.UpdateAsync(user);

  return createAdminProfileResult;
}
```

### 리팩토링 후 (함수형)
```cs
public static class CreateAdminProfileCommand
{
  // 입력 메시지
  public sealed record Request(Guid UserId)
    : ICommand<Response>;

  // 출력 메시지
  public sealed record Response(Option<Guid> AdminId)
    : IResponse;

  // 입력 메시지 유효성 검사
  internal sealed class Validator
    : AbstractValidator<Request>
  {
    public Validator()
    {
      RuleFor(_ => _.UserId)
        .NotEmpty();
    }
  }

  // 유스케이스
  internal sealed class Usecase(IUsersRepository usersRepository)
    : ICommandUsecase<Request, Response>
  {
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<Fin<Response>> Handle(Request request, CancellationToken cancellationToken)
    {
      var usecase = from user in _usersRepository.GetByIdAsync(request.UserId)
                    from newAdminId in user.CreateAdminProfile()
                    from _ in _usersRepository.UpdateAsync(user)
                    select newAdminId;

      return await usecase
        .Run()
        .RunAsync()
        .ToCreateAdminProfileResponse();
    }
  }
}

// DTO
public static class UserMapping
{
  public static async ValueTask<Fin<CreateAdminProfileCommand.Response>> ToCreateAdminProfileResponse(this ValueTask<Fin<Guid>> task)
  {
    var result = await task;
    return result.Match(
      Succ: adminId => new CreateAdminProfileCommand.Response(adminId),
      Fail: Fin<CreateAdminProfileCommand.Response>.Fail);
  }
}
```
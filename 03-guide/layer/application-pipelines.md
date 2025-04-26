# 애플리케이션 레이어 파이프라인

## 개요
메시지 핸들러 구현 시 부수적으로 필요한
  - 입력 메시지 유효성 검사,
  - 메시지 캐싱,
  - 메시지 트랜잭션 처리,
  - 메시지 로깅,
  - 메시지 예외 처리
등의 반복적인 작업을 MediatR의 파이프라인 기능을 통해 처리합니다.  
이를 통해 메시지 핸들러에서는 유스케이스 본질에만 더 집중하여 구현할 수 있는 환경을 구성해야 합니다.

## 파이프라인
- [x] 입력 메시지 유효성 검사
- [ ] 메시지 캐시
- [ ] 메시지 트랜잭션
- [ ] 메시지 로그
- [ ] 메시지 예외 처리

```
internal static class MediatRRegistration
{
    internal static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            // 메시지 핸들러 등록
            configuration.RegisterServicesFromAssemblies(AssemblyReference.Assembly);

            // 입력 메시지 유효성 검사 추가가
            configuration.AddOpenBehavior(typeof(FluentValidationPipeline<,>));
        });

        return services;
    }
}
```
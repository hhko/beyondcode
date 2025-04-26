# 애플리케이션 레이어 파이프라인

## 개요
- **메시지 핸들러는 유스케이스의 본질적인 로직을 구현하는 데 집중해야 합니다.**
- 그러나 실무에서는 유효성 검사, 트랜잭션 관리, 로깅, 예외 처리 등 부수적이고 반복적인 기술 작업도 필수적으로 요구됩니다.
- 기술적인 반복 작업을 별도로 분리하여, 핸들러 코드의 가독성 및 유지보수성을 향상시킵니다.
- MediatR의 파이프라인 기능을 이용해 다음과 같은 부수적 작업을 유스케이스 구현에서 분리 시킵니다(파이프라인).
  - 입력 메시지 유효성 검사
  - 메시지 캐싱
  - 메시지 트랜잭션 처리
  - 메시지 로깅
  - 메시지 예외 처리
- 모든 기술적 처리는 파이프라인에서 자동으로 수행되며, 메시지 핸들러는 유스케이스의 본질적인 구현에만 집중할 수 있는록 합니다

## 파이프라인
- [x] 입력 메시지 유효성 검사
- [ ] 메시지 캐시
- [ ] 메시지 트랜잭션
- [ ] 메시지 로그
- [ ] 메시지 예외 처리
- ...

```
internal static class MediatRRegistration
{
    internal static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            // 메시지 핸들러 등록
            configuration.RegisterServicesFromAssemblies(AssemblyReference.Assembly);

            // 입력 메시지 유효성 검사 추가
            configuration.AddOpenBehavior(typeof(FluentValidationPipeline<,>));
        });

        return services;
    }
}
```
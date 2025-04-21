using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DddGym.Framework.Options;

public static class FluentValidationOptionsExtensions
{
    // TOptions 유효성 검사 등록
    public static OptionsBuilder<TOptions> AddConfigureOptions<TOptions, TValidator>(
        this IServiceCollection services,
        string configurationSectionName)
            where TOptions : class
            where TValidator : class, IValidator<TOptions>
    {
        // IValidator 인터페이스 구현체 등록
        services.AddScoped<IValidator<TOptions>, TValidator>();

        return services.AddOptions<TOptions>()              // TOptions과
            .BindConfiguration(configurationSectionName)    //  - 옵션 데이터 연결: appsettings.json을 configurationSectionName 이름으로 연결
            .ValidateFluentValidation()                     //  - 옵션 유효성 연결: IValidateOptions<TOptions>과 연결
            .ValidateOnStart();                             //  - 옵션 유효성 검사: 프로그램 시작 시 유효성 감사
    }
}
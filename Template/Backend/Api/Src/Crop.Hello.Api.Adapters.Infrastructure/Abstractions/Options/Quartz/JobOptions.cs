﻿namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.Quartz;

public sealed class JobOptions
{
    // 1. 초(0-59)
    // 2. 분(0-59)
    // 3. 시간(0-23)
    // 4. 일(1-31)
    // 5. 월(1-12 또는 JAN-DEC)
    // 6. 요일(1-7 또는 SUN-SAT, 1은 일요일)
    // ---
    // 7. 연도(선택 사항)

    public string CronSchedule { get; init; } = default!;

    // 매주 월요일 오전 10시에 실행
    // 0 10 ? * MON             : 매주 월요일 오전 10시에 실행

    // 매일 정각에 실행
    // 0 0 * * ? *              : 매일 00:00시에 실행

    // 매시간 실행
    // 0 0 * * * ?              : 매시간 정각에 실행

    // 매주 월, 수, 금 오후 3시에 실행
    // 0 15 ? * MON,WED,FRI     : 매주 월요일, 수요일, 금요일 오후 3시에 실행

    // 매월 1일 오전 6시에 실행
    // 0 6 1 * ? *

    // 매일 오후 5시 30분에 실행
    // 30 17 * * ? *

    // 매분 실행
    // 0 * * * * ?

    // 특정 날짜마다 실행 (예: 2024년 12월 24일)
    // 0 0 24 12 ? 2024

    // 매 15분마다 실행
    // 0 0/15 * * * ?

    // 매주 토요일 오후 10시에 실행
    // 0 22 ? * SAT



    // 매일 특정 시간에 실행
    //  0 9 * * ? *                  : 매일 오전 9시에 작업

    //  0 10 ? * MON                : 매주 월요일 오전 10시에 실행
    //   - 0초

    // 0 0 * * ? *     매일 정각에 실행

    // 0/5 * * * * ?                : 5초 단위, ? 요일 무시
    // 0 0/5 * * * ?                : 5분 단위, ? 요일 무시

    // 0 30 10-13 ? * WED,FRI       : 0초, 30분매주 Wednesday과 Friday의 10:30, 11:30, 12:30, 13:30
}

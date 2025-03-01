# Application 레이어

## Application 레이어 템플릿
```shell
└─ {Solution}.{Service}.Application
   ├─ Abstractions                                      // 부수 목표
   │  ├─ Registrations                                  // - 의존성 등록
   │  ├─ Pipelines                                      // - MediatR 파이프라인
   │  └─ ...
   │
   ├─ Usecases                                          // 주 목표
   │  ├── {Usecase}                                     // - 유스케이스
   │  │   ├─ Commands                                   // - Command 유스케이스
   │  │   │  └─ {CommandName}                           //   - Command 이름
   │  │   │      ├─ {CommandName}Command.cs             //     - Command Input: DTO
   │  │   │      ├─ {CommandName}CommandTelemetry.cs    //     - Command Telemetry: 메시지 로그, 추적, 지표
   │  │   │      ├─ {CommandName}CommandUsecase.cs      //     - Command Usecase: 메시지 처리
   │  │   │      ├─ {CommandName}CommandValidator.cs    //     - Command Validator: 메시지 유효성 검사
   │  │   │      └─ {CommandName}Response.cs            //     - Command Output: DTO
   │  │   │
   │  │   ├─ Events                                     // - Event 유스케이스
   │  │   │
   │  │   └─ Queries                                    // - Query 유스케이스
   │  │      └─ {QueryName}                             //   - Query 이름
   │  │          ├─ {QueryName}Query.cs                 //     - Query Input: DTO
   │  │          ├─ {QueryName}QueryTelemetry.cs        //     - Query Telemetry: 메시지 로그, 추적, 지표
   │  │          ├─ {QueryName}QueryUsecase.cs          //     - Query Usecase: 메시지 처리
   │  │          ├─ {QueryName}QueryValidator.cs        //     - Query Validator: 메시지 유효성 검사
   │  │          └─ {QueryName}Response.cs              //     - Query Output: DTO
   │  └─ ...
   │
   └─ AssemblyReference.cs
```

## Application 단위 테스트
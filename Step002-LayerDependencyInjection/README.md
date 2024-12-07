# 레이어 의존성 주입(관찰 가능성 옵션)

## 목표
- [x] 레이어 의존성 주입/관찰 가능성 옵션
- [ ] 관찰 가능성 콘솔 로그
- [ ] 통합 테스트, 옵션

## TODO
- [ ] .

## 폴더 구성
![](./.images/DI.Structure.png)

- `Abstractions` 폴더
  - 레이어의 주요 목표와 직접 관련이 없는 모든 코드는 Abstractions 폴더에서 관리합니다.
    - 의존성 등록: Registrations
    - 옵션: Options

## 옵션 패턴
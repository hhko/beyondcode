---
outline: deep
---

# 솔루션 파일

## 형상관리
- .gitignore
- .gitattributes

## .NET 설정
- global.json
- nuget.config
- Directory.Packages.props
- Directory.Build.props

## 코딩 스타일
- .editorconfig
- - 타입
  - `var` 키워드를 사용하지 않는다.
  - `Target-typed new` 키워드를 사용한다.
- 클래스 접근 제어
  ```cs
  internal sealed class Foo
  ```

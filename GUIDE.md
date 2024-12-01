## 의존성 구성
### 프로젝트
1. AssemblyReference 파일

### Unit 테스트 프로젝트
1. xunit.runner.json 파일(모든 테스트 프로젝트)
1. Directory.Build.props 테스트 전용 파일
1. Abstractions/Constants/Constants.Constants.cs 파일
1. ArchitectureTests/ArchitectureBaseTest.cs 파일
1. ArchitectureTests/LayerDependencyTests.cs 파일

---

## CQRS
### 프로젝트
1. Result, ValidationResult, Error
1. ICommand, IQuery

### Unit 테스트 프로젝트
1. Results 테스트
1. ArchitectureTests/NamingConventionsCQRSTests

---



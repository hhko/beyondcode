# 솔루션 설정 .gitignore
> Git이 추적하지 않도록 제외할 파일 목록을 지정하는 역할을 합니다.

```shell
# 명령어 확인
dotnet new list | findstr git

# .gitignore 파일 생성
dotnet new gitignore
```

```
*.received.*
```
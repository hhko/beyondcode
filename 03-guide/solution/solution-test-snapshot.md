# 솔루션 Snapshot 테스트

## 개요

<br/>

## 형상관리 설정
### .gitignore
```
*.received.*
*.received/
```

### .gitattributes
```
*.verified.txt text eol=lf working-tree-encoding=UTF-8
*.verified.xml text eol=lf working-tree-encoding=UTF-8
*.verified.json text eol=lf working-tree-encoding=UTF-8
```

### .editorconfig
```
# Verify settings
[*.{received,verified}.{json,txt,xml}]
charset = "utf-8-bom"
end_of_line = lf
indent_size = unset
indent_style = unset
insert_final_newline = false
tab_width = unset
trim_trailing_whitespace = false
```

<br/>

## Snapshot 설정
### 비교 파일 위치
```cs
static class VerifyInitializer
{
    // https://github.com/VerifyTests/Verify/blob/main/docs/naming.md#derivepathinfo
    [ModuleInitializer]
    public static void Init()
    {
        Verifier.DerivePathInfo(
            (sourceFile, projectDirectory, type, method) => new(
                directory: Path.Combine(projectDirectory, "Snapshots"),
                typeName: type.Name,
                methodName: method.Name));
    }
}
```

<br/>

## Snapshot 관리
### verify.tool 도구
```shell
dotnet tool install -g verify.tool

# 모든 .received 파일을 하나 하나 확인합니다.
dotnet verify review        # Review pending snapshots

# 모든 .received 파일을 .verified 파일로 변경합니다.
dotnet verify accept -y     # Accept all pending snapshots

# 모든 .received 파일을 삭제합니다.
dotnet verify reject -y     # Reject all pending snapshots
```
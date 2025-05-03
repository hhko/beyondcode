# 솔루션 Snapshot 테스트

## 개요
- Snapshot 테스트는 테스트 대상 객체의 출력을 스냅샷 파일에 저장하고, 테스트 실행 시 해당 결과와 비교하여 변경 여부를 감지하는 테스트 기법입니다.
- [Verify 패키지](https://github.com/VerifyTests/Verify)를 사용하면 직관적이고 강력한 스냅샷 테스트를 손쉽게 작성할 수 있으며, 특히 텍스트, JSON, XML 등 다양한 형식의 결과를 안정적으로 검증할 수 있습니다.

<br/>

## 지침
- `Snapshot` 테스트는 `Source Generator` 출력, `Web API` 응답 값, 복잡한 객체 직렬화 결과처럼 구조화된 출력이 크거나 반복적으로 검증해야 하는 경우에 효과적입니다.
- 이처럼 검증 대상의 구조가 자주 변경되거나 사람이 수작업으로 기대값을 작성하기 어려운 경우에 적용하면 유용합니다."

<br/>

## 형상관리 설정
- Snapshot 테스트에서 생성되는 `*.received.*`, `*.verified.*` 파일들은 Git 관리 정책에 따라 포함/제외가 필요합니다.
  - `*.received.*`: 제외, 테스트 결과 파일
  - `*.verified.*`: 포함, 테스트 검증 파일

### `.gitignore`
- `*.received.*` 파일은 변경 여부 확인용 임시 파일이며 Git에 커밋되면 안 됩니다.

```
*.received.*
*.received/
```

### `.gitattributes`

- `*.verified.*` 파일은 테스트 기준이 되는 파일이므로 Git에 포함되며, 줄바꿈과 인코딩 일관성을 보장해야 합니다.

```
*.verified.txt  text eol=lf working-tree-encoding=UTF-8
*.verified.xml  text eol=lf working-tree-encoding=UTF-8
*.verified.json text eol=lf working-tree-encoding=UTF-8
```

- `*.verified.txt text`, `*.verified.xml text`, `*.verified.json text`: Git에게 해당 파일은 텍스트 파일임을 명시합니다.
- `eol=lf`: 저장소에는 항상 LF (Line Feed) 줄바꿈 문자로 저장되도록 지정합니다.
- `working-tree-encoding=UTF-8`: 로컬 작업 디렉터리(Working Tree)에서 파일을 읽고 쓸 때 사용할 문자 인코딩을 UTF-8로 지정합니다.

### `.editorconfig`
- Verify 테스트 파일들의 인코딩, 개행 등은 아래와 같이 명시적으로 정의합니다.
- 공백 하나로도 Snapshot diff가 발생할 수 있기 때문에 불필요한 자동 정리를 막습니다(`unset`, `false`).

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

## Snapshot 저장 위치 설정

- 기본적으로 Verify는 테스트 소스 파일과 동일한 위치에 스냅샷 파일을 생성합니다.
  - 그러나 관리 편의를 위해 별도의 `Snapshots/` 폴더에 저장하도록 설정할 수 있습니다.
  - `Snapshots/` 폴더는 Git에 커밋되며, `*.verified.*` 파일이 저장됩니다.
- 환경변수 CI가 true일 때 비교 도구 표시를 비활성화합니다

```cs
// VerifyInitializer.cs
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

        // 환경변수 CI가 true일 때 비교 도구 표시를 비활성화합니다
        if (Environment.GetEnvironmentVariable("CI") == "true")
        {
            DiffRunner.Disabled = true;
        }
    }
}
```

<br/>

## 사용 예제

### 기본 Snapshot 테스트

```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class SnapshotTests
{
    [Fact]
    public Task VerifyPerson()
    {
        var person = new Person { Name = "Alice", Age = 30 };
        return Verify(person);
    }
}
```

```
{
  Name: Alice,
  Age: 30
}
```

### JSON 직렬화 형식 지정

```csharp
[Fact]
public Task VerifyJsonString()
{
    var json = "{'key': {'msg': 'No action taken'}}";
    return VerifyJson(json);
}
```

```json
{
  key: {
    msg: No action taken
  }
}
```

<br/>

## Snapshot 관리 도구

`verify.tool`은 수동으로 `.received` 파일을 비교/수락/거부하는 번거로움을 줄여주는 CLI 도구입니다.

### 설치

```bash
dotnet tool install -g verify.tool
dotnet tool list -g
```

### 명령어

| 명령어                          | 설명                                      |
|-------------------------------|-----------------------------------------|
| `dotnet verify review`        | `.received.*` 파일을 하나씩 검토합니다.    |
| `dotnet verify accept -y`     | 모든 `.received.*` 파일을 `.verified.*`로 수락합니다. |
| `dotnet verify reject -y`     | 모든 `.received.*` 파일을 삭제합니다.     |


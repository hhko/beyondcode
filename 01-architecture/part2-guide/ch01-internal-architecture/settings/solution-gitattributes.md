# 솔루션 설정 .gitattributes

## 개요
> OS 간 라인 엔딩 문제를 해결하기 위해 사용됩니다.

## 예제
```
# Verify 검증 파일 변환
*.verified.txt text eol=lf working-tree-encoding=UTF-8
*.verified.xml text eol=lf working-tree-encoding=UTF-8
*.verified.json text eol=lf working-tree-encoding=UTF-8

# 이미지 파일 바이너리 처리
*.png binary
*.jpg binary

# JSON 파일 diff 적용
*.json diff=json
```

- `*.verified.json text eol=lf`: 텍스트 파일로 취급하고 체크아웃할 때 줄바꿈을 LF(\n: 리눅스 스타일) 형식으로 강제합니다
- `working-tree-encoding=UTF-8`: Git 저장소는 파일을 바이너리 형태로 저장하며, 인코딩 정보를 따로 관리하지 않기 때문에 파일을 꺼낼 때(체크아웃 시) UTF-8로 변환합니다.
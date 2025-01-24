---
outline: deep
---

# Docusaurs

## 실행
### 로컬 실행
```shell
npm install
npm ls

# http://localhost:3000/
npm run build
npm run serve
```

### 컨테이너 실행
| 구분 | 이름 |
| --- | --- |
| 이미지   | `{조직}/{프로젝트}/infra/wiki` |
| 컨테이너 | `{조직}.{프로젝트}.infra.wiki` |

```shell
# 이미지 빌드
docker compose build

# 컴포즈 실행: http://localhost:3000/
docker compose up -d

# 컴포즈 종료
docker compose down
```

## 구축

### 사이트 생성
- https://docusaurus.io/docs
```shell
npm show @docusaurus/core versions
npx create-docusaurus@3.7.0 classic --typescript
npm ls
npm run build
npm run serve
```

### 로컬 검색
- https://github.com/cmfcmf/docusaurus-search-local

```shell
npm show @easyops-cn/docusaurus-search-local versions
npm install --save @easyops-cn/docusaurus-search-local@0.48.4
npm ls
```

- docusaurus.config.ts

```ts
  i18n: {
    defaultLocale: 'ko',
    locales: ['ko'],
  },

  // 검색
  //
  themes: [
    // ... Your other themes.
    [
      require.resolve("@easyops-cn/docusaurus-search-local"),
      /** @type {import("@easyops-cn/docusaurus-search-local").PluginOptions} */
      ({
        // docusaurus-search-local Options
        // https://github.com/easyops-cn/docusaurus-search-local?tab=readme-ov-file#theme-options

        // `hashed` is recommended as long-term-cache of index file is possible.
        // Whether to add a hashed query when fetching index
        //  (based on the content hash of all indexed *.md in docsDir and blogDir if applicable).
        //  Setting to "filename" will save hash in filename instead of query.
        hashed: true,             // 페이지 경로의 해시 생성 여부

        // Whether to index docs, 문서 인덱싱 여부
        indexDocs: true,
        // Whether to index blog, 블로그 인덱싱 여부
        indexBlog: true,
        // Whether to index pages, 일반 페이지 인덱싱 여부
        indexPages: true,

        // 인덱싱할 언어 설정
        language: ['ko', 'en'],

        // Highlight search terms on target page, 검색 결과 페이지 Highlight 표시
        highlightSearchTermsOnTargetPage: true,

        // Whether an explicit path to a heading should be presented on a suggestion template.
        explicitSearchResultPath: true,
      }),
    ],
  ],
```

### 이미지 확대 플러그인
- https://github.com/gabrielcsapo/docusaurus-plugin-image-zoom

```shell
npm show docusaurus-plugin-image-zoom versions
npm install --save docusaurus-plugin-image-zoom@2.0.0
npm ls
```

- docusaurus.config.ts

```ts
  plugins: [
    'docusaurus-plugin-image-zoom',
  ],

  themeConfig: {
    // 이미지 확대
    zoom: {
      selector: '.markdown :not(em) > img',
      background: {
        light: 'rgb(255, 255, 255)',
        dark: 'rgb(50, 50, 50)'
      },
      config: {
        // options you can specify via https://github.com/francoischalifour/medium-zoom#usage
      }
    },
```

### Mermaid 플러그인
- https://docusaurus.io/docs/next/api/themes/@docusaurus/theme-mermaid

```shell
npm show @docusaurus/theme-mermaid versions
npm install --save @docusaurus/theme-mermaid@3.7.0
npm ls
```

- docusaurus.config.ts

```ts
    // mermaid 테마
    markdown: {
      mermaid: true,
    },

    // mermaid 테마
    '@docusaurus/theme-mermaid',
```

### Draw.io 플러그인
- https://github.com/xiguaxigua/docusaurus-plugin-drawio

```shell
npm show docusaurus-plugin-drawio versions
npm install --save docusaurus-plugin-drawio@0.4.0
npm ls
```

- docusaurus.config.ts

```ts
  plugins: [
    'docusaurus-plugin-image-zoom',
    'drawio'
  ],
```

- 작성 방법
  - 문서 파일: .mdx
  - DrawIo 파일: `./drawio-graph/simple.drawio`
  - DrawIo 설정
    - 페이지: `page={번호}`
    - 그 외 설정: [링크](https://github.com/xiguaxigua/docusaurus-plugin-drawio?tab=readme-ov-file#props)

```
import Drawio from '@theme/Drawio'
import simpleGraph from '!!raw-loader!./drawio-graph/simple.drawio';

<Drawio content={simpleGraph} />

----

<Drawio content={simpleGraph} page={1} />
```

### 컨테이너
```shell
# 이미지 빌드
docker compose build
docker compose build --no-cache
docker image ls

# 컴포즈 실행
docker compose up -d
docker compose ls
docker container ls
```


## 배포 자동화
### SSH 구성
```
Windows -{SSH}-> Linux
```

```shell
#
# 1. SSH 키 생성 : RSA 기반 형식으로 키 만들기
#   - $env:USERPROFILE\.ssh\id_rsa
#   - $env:USERPROFILE\.ssh\id_rsa.pub
ssh-keygen

#
# Home 디렉토리에 .ssh 폴더를 생성하기(없다면)
#
ssh {서버 계정}@{서버 이름 | 서버 IP}
mkdir ~\.ssh

#
# 2. SSH Public 키 내용 복사
#   - yes
#   - 로그인 암호 입력
type $env:USERPROFILE\.ssh\id_rsa.pub | ssh {서버 계정}@{서버 이름 | 서버 IP} "cat >> .ssh/authorized_keys"
# 예.
# type $env:USERPROFILE\.ssh\id_rsa.pub | ssh xxx@xxx.xxx.xxx.xxx "cat >> .ssh/authorized_keys"

#
# 3. SSH 접속
#   - 암호 입력 없음
ssh {서버 계정}@{서버 이름 | 서버 IP}
```

## TODO
- [ ] docusaurus.config.ts 소스 링크
- [ ] 다국어
- [ ] 주요 문서 작성법
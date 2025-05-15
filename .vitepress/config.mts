import { type DefaultTheme, defineConfig } from "vitepress";

// https://vitepress.dev/reference/site-config
export default defineConfig({
  base: "/beyondcode/",

  title: "beyond code",
  description: "VOC |> DDD |> Architecture",
  lang: "ko-KR",
  themeConfig: {
    outline: {
      label: "페이지 내용"
    },
    docFooter: {
      prev: "이전 페이지",
      next: "다음 페이지"
    },
    lastUpdated: {
      text: "마지막 업데이트"
    },

    editLink: {
      pattern: "https://github.com/hhko/beyondcode/edit/main/:path",
      text: "이 페이지 편집 제안하기"
    },

    socialLinks: [
      { icon: 'github', link: 'https://github.com/hhko/beyondcode' }
    ],

    // https://vitepress.dev/reference/default-theme-config
    nav: nav(),
    sidebar: sidebar(),

    search: {
      provider: 'local',
      options: {
        locales: {
          root: { // 기본 로케일을 번역하려면 이것을 `root`로 만드십시오.
            translations: {
              button: {
                buttonText: '검색',
                buttonAriaLabel: '검색'
              },
              modal: {
                displayDetails: '상세 목록 표시',
                resetButtonTitle: '검색 지우기',
                backButtonTitle: '검색 닫기',
                noResultsText: '결과를 찾을 수 없습니다',
                footer: {
                  selectText: '선택',
                  selectKeyAriaLabel: '선택하기',
                  navigateText: '탐색',
                  navigateUpKeyAriaLabel: '위로',
                  navigateDownKeyAriaLabel: '아래로',
                  closeText: '닫기',
                  closeKeyAriaLabel: 'esc'
                }
              }
            }
          }
        }
      }

    }
  }
})

//
// 상단 메뉴
//
function nav(): DefaultTheme.NavItem[] {
  return [
    { text: '홈', link: '/' },
    { text: '아키텍처', link: '/01-architecture/part1-overview/ch01-architecture/'},
    // { text: '튜토리얼', link: '/02-tutorial/ddd/ch01-domain-glossary/' },
    // { text: '참고', link: '/04-reference/' },
  ];
}

//
// 사이드 메뉴
//
function sidebar(): DefaultTheme.Sidebar {
  return {
    '/01-architecture/': [
      {
        text: '아키텍처',
        items: [
          {
            text: '아키텍처 개요', link: '/01-architecture/part1-overview/',
            items: [
              { text: '아키텍처', link: '/01-architecture/part1-overview/ch01-architecture/' },
              { text: '아키텍처 진단', link: '/01-architecture/part1-overview/ch02-architecture-diagnosis/' },
              { text: 'Internal 아키텍처', link: '/01-architecture/part1-overview/ch03-internal-architecture/' },
              { text: 'External 아키텍처', link: '/01-architecture/part1-overview/ch04-external-architecture/' }
            ],
          },
          {
            text: 'Internal 아키텍처 가이드', link: '/01-architecture/part2-internal-architecture/',
            items: [
              {
                text: '설치',
                collapsed: true,
                items: [
                  { text: '개발 환경', link: '/01-architecture/part2-internal-architecture/setup/dev-environment.md' },
                  { text: '도커', link: '/01-architecture/part2-internal-architecture/setup/docker.md' },
                ]
              },
              {
                text: '설정',
                collapsed: true,
                items: [
                  { text: '.gitignore', link: '/01-architecture/part2-internal-architecture/settings/solution-gitignore.md' },
                  { text: '.gitattributes', link: '/01-architecture/part2-internal-architecture/settings/solution-gitattributes.md' },
                  { text: 'global.json', link: '/01-architecture/part2-internal-architecture/settings/solution-globaljson.md' },
                  { text: 'nuget.config', link: '/01-architecture/part2-internal-architecture/settings/solution-nugetconfig.md' },
                  { text: 'appsettings.json', link: '/01-architecture/part2-internal-architecture/settings/project-appsettings.md' }
                ]
              },
              {
                text: '기본',
                collapsed: true,
                items: [
                  { text: 'AssemblyReference.cs', link: '/01-architecture/part2-internal-architecture/foundation/assemblyreference.md' },
                  { text: '접근 제어자', link: '/01-architecture/part2-internal-architecture/foundation/access-modifiers.md' },
                  { text: '순수 함수', link: '/01-architecture/part2-internal-architecture/foundation/pure-function.md' },
                ]
              },
              {
                text: 'Domain 레이어',
                collapsed: true,
                items: [
                  { text: 'Domain 레이어 구성', link: '/01-architecture/part2-internal-architecture/domain-layer/domain-layer-structure.md' },
                  { text: 'Domain 에러', link: '/01-architecture/part2-internal-architecture/domain-layer/domain-error.md' },
                ]
              },
              {
                text: 'Application 레이어',
                collapsed: true,
                items: [
                  { text: 'Application 레이어 구성', link: '/01-architecture/part2-internal-architecture/application-layer/application-layer-structure.md' }
                ]
              },
              {
                text: 'Adapter 레이어',
                collapsed: true,
                items: [
                  { text: '옵션 유효성 검사', link: '/01-architecture/part2-internal-architecture/infrastructure-layer/options-validator.md' }
                ]
              },
              {
                text: '테스트',
                collapsed: true,
                items: [
                  { text: '단위 테스트 구성', link: '/01-architecture/part2-internal-architecture/testing/unit-testing-structure.md' },
                  { text: '레이어 의존성 테스트', link: '/01-architecture/part2-internal-architecture/testing/architecture-layer-dependency.md' },
                  { text: '네이밍컨벤션 테스트', link: '/01-architecture/part2-internal-architecture/testing/architecture-namingconventions.md' },
                  { text: 'WebApi 통합 테스트', link: '/01-architecture/part2-internal-architecture/testing/integration-webapi.md' },
                  { text: '유효성 검사 테스트', link: '/01-architecture/part2-internal-architecture/testing/infra-validator.md' }
                ]
              }
            ]
          },
          {
            text: 'External 아키텍처 가이드', link: '/01-architecture/part3-external-architecture/',
            items: [
              {
                text: '관찰 가능성',
                collapsed: true,
                items: [
                  { text: 'Aspire 대시보드', link: '/01-architecture/part3-external-architecture/observability/aspire-dashboard/' },
                  { text: 'otel-collector', link: '/01-architecture/part3-external-architecture/observability/otel-collector/' },
                ]
              },
            ]
          }
        ],
      },
    ],
    // '/02-tutorial/': [
    //   {
    //     text: '튜토리얼',
    //     items: [
    //       {
    //         text: '도메인 주도 설계', link: '/02-tutorial/ddd/',
    //         collapsed: false,
    //         items: [
    //           { text: '도메인 용어', link: '/02-tutorial/ddd/ch01-domain-glossary/'},
    //           { text: '도메인 탐색', link: '/02-tutorial/ddd/ch02-domain-exploration/'},
    //         ]
    //       }
    //     ]
    //   }
    // ],
  }
}

export const search: DefaultTheme.LocalSearchOptions["locales"] = {
  root: {
    translations: {
      button: {
        buttonText: "검색",
        buttonAriaLabel: "검색"
      },
      modal: {
        backButtonTitle: "뒤로가기",
        displayDetails: "더보기",
        footer: {
          closeKeyAriaLabel: "닫기",
          closeText: "닫기",
          navigateDownKeyAriaLabel: "아래로",
          navigateText: "이동",
          navigateUpKeyAriaLabel: "위로",
          selectKeyAriaLabel: "선택",
          selectText: "선택"
        },
        noResultsText: "검색 결과를 찾지 못했어요.",
        resetButtonTitle: "모두 지우기"
      }
    }
  }
};

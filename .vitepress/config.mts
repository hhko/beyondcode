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

function nav(): DefaultTheme.NavItem[] {
  return [
    { text: '홈', link: '/' },
    { text: '아키텍처', link: '/01-architecture/part1-overview/ch02-architecture/'},
    { text: '세미나', link: '/02-seminar/ch01-internal-architecture/'},
    { text: '튜토리얼', link: '/03-tutorial/ddd/ch01-domain-exploration/' },
    { text: '참고', link: '/04-reference/youtube/' },
  ];
}
function sidebar(): DefaultTheme.Sidebar {
  return {
    '/01-architecture/': [
      {
        text: '아키텍처',
        items: [
          {
            text: '개요',
            items: [
              { text: '개발 환경', link: '/01-architecture/part1-overview/ch01-prerequisite/' },
              { text: '아키텍처', link: '/01-architecture/part1-overview/ch02-architecture/' },
              { text: '아키텍처 진단', link: '/01-architecture/part1-overview/ch03-architecture-diagnosis/' },
              { text: 'Internal 아키텍처', link: '/01-architecture/part1-overview/ch04-internal-architecture/' },
              { text: 'External 아키텍처', link: '/01-architecture/part1-overview/ch05-external-architecture/' }
            ],
          },
          {
            text: '가이드',
            items: [
              {
                text: 'Internal 아키텍처', link: '/01-architecture/part2-guide/ch01-internal-architecture/',
                items: [
                  {
                    text: '설정',
                    items: [
                      { text: '.gitignore', link: '/01-architecture/part2-guide/ch01-internal-architecture/settings/solution-gitignore.md' },
                      { text: '.gitattributes', link: '/01-architecture/part2-guide/ch01-internal-architecture/settings/solution-gitattributes.md' },
                      { text: 'global.json', link: '/01-architecture/part2-guide/ch01-internal-architecture/settings/solution-globaljson.md' },
                      { text: 'nuget.config', link: '/01-architecture/part2-guide/ch01-internal-architecture/settings/solution-nugetconfig.md' },
                      { text: 'appsettings.json', link: '/01-architecture/part2-guide/ch01-internal-architecture/settings/project-appsettings.md' }
                    ]
                  },
                  {
                    text: '레이어',
                    items: [
                      { text: 'AssemblyReference.cs', link: '/01-architecture/part2-guide/ch01-internal-architecture/layers/common-assemblyreference.md' },
                      { text: '접근 제어자', link: '/01-architecture/part2-guide/ch01-internal-architecture/layers/common-access-modifiers.md' },
                      { text: '순수 함수', link: '/01-architecture/part2-guide/ch01-internal-architecture/layers/common-pure-function.md' }
                    ]
                  }
                ]
              }
            ]
          }
        ],
      },
    ],
    '/02-seminar/': [
      {
        text: '세미나',
        items: [
          { text: 'Internal 아키텍처', link: '/02-seminar/ch01-internal-architecture/'}
        ]
      }
    ],
    '/03-tutorial/': [
      {
        text: '튜토리얼',
        items: [
          {
            text: '도메인 주도 설계', link: '/03-tutorial/ddd/',
            items: [
              { text: '도메인 탐색', link: '/03-tutorial/ddd/ch01-domain-exploration/'},
              { text: '도메인 구조화', link: '/03-tutorial/ddd/ch02-domain-structuring/'},
              { text: '유스케이스 탐색', link: '/03-tutorial/ddd/ch03-usecase-exploration/'}
            ]
          }
        ]
      }
    ],
    '/04-reference/': [
      {
        text: '참고',
        items: [
          { text: 'Youtube', link: '/04-reference/youtube/' },
          { text: 'Blog', link: '/04-reference/blog/' },
          { text: 'GitHub', link: '/04-reference/github/' }
        ]
      }
    ]
  }
}

//       {
//         text: 'TUTORIAL',
//         items: [
//           {
//             text: 'Observability',
//             items: [
//               { text: '로그', link: 'tutorials/observability/logs/readme' }
//             ]
//           },
//         ]
//       },
//       {
//         text: 'INFRA',
//         items: [
//           {
//             text: 'Docker',
//             items: [
//               { text: '설치', link: 'infra/docker/install/readme' }
//             ]
//           },
//           {
//             text: 'Observability',
//             items: [
//               { text: 'Aspire', link: 'infra/observability/aspire/readme' },
//               { text: 'Otel-Collector', link: 'infra/observability/otel-collector/readme' }
//             ]
//           },
//           {
//             text: '사이트 생성기',
//             items: [
//               { text: 'Docusaurus', link: '/infra/ssg/docusaurus/readme' }
//             ]
//           }
//         ]
//       }
//     ]
//   ];
// }

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

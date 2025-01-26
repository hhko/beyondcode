import { type DefaultTheme, defineConfig } from "vitepress";

// https://vitepress.dev/reference/site-config
export default defineConfig({
  base: "/beyondcode/",

  title: "Beyond Code",
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

    // search: {
    //   provider: "local",
    //   options: {
    //     locales: {
    //       ...search()
    //     }
    //   }
    // },

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
    { text: 'README',
      items: [
        {
          text: '개요',
          items: [
            { text: '프로그램 환경', link: '/part1-overview/ch01-prerequisite/readme' },
            { text: '아키텍처', link: '/part1-overview/ch02-architecture/readme' },
            { text: '아키텍처 진단', link: '/part1-overview/ch03-architecturediagnosis/readme' },
            { text: 'Internal 아키텍처', link: '/part1-overview/ch04-internalarchitecture/readme' },
            { text: 'External 아키텍처', link: '/part1-overview/ch05-externalarchitecture/readme' }
          ]
        },
      ]
    }
  ];
}

function sidebar(): DefaultTheme.Sidebar {
  return [
    {
      text: 'README',
      items: [
        {
          text: '세미나',
          items: [
            { text: 'Internal 아키텍처 개요', link: '/part0-seminar/ch01.internalarchitecture/readme' }
          ]
        },
        {
          text: '개요',
          items: [
            { text: '프로그램 환경', link: '/part1-overview/ch01-prerequisite/readme' },
            { text: '아키텍처', link: '/part1-overview/ch02-architecture/readme' },
            { text: '아키텍처 진단', link: '/part1-overview/ch03-architecturediagnosis/readme' },
            { text: 'Internal 아키텍처', link: '/part1-overview/ch04-internalarchitecture/readme' },
            { text: 'External 아키텍처', link: '/part1-overview/ch05-externalarchitecture/readme' }
          ]
        },
        {
          text: 'Internal 아키텍처',
          items: [
            { text: '솔루션 구성', link: '/part2-solution/ch01-solutionlstructure/readme' }
          ]
        },
      ]
    },
    {
      text: 'TUTORIAL',
      items: [
        {
          text: 'Observability',
          items: [
            { text: '로그', link: 'tutorials/observability/logs/readme' }
          ]
        },
      ]
    },
    {
      text: 'INFRA',
      items: [
        {
          text: 'Docker',
          items: [
            { text: '설치', link: 'infra/docker/install/readme' }
          ]
        },
        {
          text: 'Observability',
          items: [
            { text: 'Aspire', link: 'infra/observability/aspire/readme' },
            { text: 'Otel-Collector', link: 'infra/observability/otel-collector/readme' }
          ]
        },
        {
          text: '사이트 생성기',
          items: [
            { text: 'Docusaurus', link: '/infra/ssg/docusaurus/readme' }
          ]
        }
      ]
    }
  ];
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

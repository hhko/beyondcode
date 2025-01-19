import { type DefaultTheme, defineConfig } from "vitepress";

// https://vitepress.dev/reference/site-config
export default defineConfig({
  title: "Beyond Code",
  description: "VOC |> DDD |> Architecture",
  lang: "ko",
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
    { text: 'README', link: '/Part1-Overview/Ch02-Architecture/README' }
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
            { text: 'Internal 아키텍처 개요', link: '/Part0-Seminar/Ch01.InternalArchitecture/README' }
          ]
        },
        {
          text: '개요',
          items: [
            { text: '프로그램 환경', link: '/Part1-Overview/Ch01-Prerequisite/README' },
            { text: '아키텍처', link: '/Part1-Overview/Ch02-Architecture/README' },
            { text: '아키텍처 진단', link: '/Part1-Overview/Ch03-ArchitectureDiagnosis/README' },
            { text: 'Internal 아키텍처', link: '/Part1-Overview/Ch04-InternalArchitecture/README' },
            { text: 'External 아키텍처', link: '/Part1-Overview/Ch05-ExternalArchitecture/README' }
          ]
        },
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

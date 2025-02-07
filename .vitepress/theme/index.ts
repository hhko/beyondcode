// https://vitepress.dev/guide/custom-theme
import { h } from 'vue'
import type { Theme } from 'vitepress'
import DefaultTheme from 'vitepress/theme'
import mediumZoom from 'medium-zoom';
import './style.css'

export default {
  extends: DefaultTheme,
  Layout: () => {
    return h(DefaultTheme.Layout, null, {
      // https://vitepress.dev/guide/extending-default-theme#layout-slots
    })
  },
  enhanceApp({ app, router, siteData }) {
    // ...
    router.onAfterRouteChanged = () => {
      mediumZoom('.main img', {
        margin: 100,  // 이미지와 화면의 여백 설정
        background: '#000',  // 확대 시 배경색
      });
    };
  },
} satisfies Theme

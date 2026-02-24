// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: "2025-07-15",
  devtools: { enabled: true },
  pages: true,
  ssr: false,
  css: ["vuetify/styles", "@mdi/font/css/materialdesignicons.min.css"],
  plugins: ["~/plugins/vuetify.ts"],
  components: true,

  build: {
    transpile: ["vuetify"],
  },

  modules: ["@pinia/nuxt"],
});
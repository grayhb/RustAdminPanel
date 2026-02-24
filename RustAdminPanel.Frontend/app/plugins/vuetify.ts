import { createVuetify } from "vuetify";
import * as components from "vuetify/components";
import { ru } from "vuetify/locale";

export default defineNuxtPlugin((nuxtApp) => {
  const vuetify = createVuetify({
    components,
    locale: {
      locale: "ru", // Default locale
      fallback: "ru", // Fallback locale
      messages: { ru }, // Provided translations
    },
  });
  nuxtApp.vueApp.use(vuetify);
});

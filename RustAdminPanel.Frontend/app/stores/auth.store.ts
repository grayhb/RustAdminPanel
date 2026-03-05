import { checkApiKey } from "~/services";

export const useAuthStore = defineStore("auth", {
  state: () => ({
    authed: false,
  }),
  getters: {},
  actions: {
    async checkAuth(apiKey: string | undefined | null = undefined) {
      this.authed = await checkApiKey(apiKey);
      return this.authed;
    },
  },
});

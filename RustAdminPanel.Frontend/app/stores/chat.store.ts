import type { ChatMessage, ChatMessageQuery } from "~/types/chat.types";

export const useChatStore = defineStore("chat", {
  state: () => ({
    loading: false,
    entites: [] as ChatMessage[],
  }),
  getters: {},
  actions: {
    async fetchData(query: ChatMessageQuery) {
      this.loading = true;
      this.entites = [];

      const data = await useAPI("/get-data/chat-messages", "POST", query);

      this.loading = false;

      if (data) this.entites = data as ChatMessage[];
    },
  },
});

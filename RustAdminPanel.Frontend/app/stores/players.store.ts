import type {
  PlayerConnectionLog,
  PlayerConnectionQuery,
} from "~/types/player.types";

export const usePlayersStore = defineStore("players", {
  state: () => ({
    loading: false,
    entites: [] as PlayerConnectionLog[],
  }),
  getters: {},
  actions: {
    async fetchData(query: PlayerConnectionQuery) {
      this.loading = true;
      this.entites = [];

      const data = await useAPI("/get-data/player-connection", "POST", query);

      this.loading = false;

      if (data) this.entites = data as PlayerConnectionLog[];
    },
  },
});

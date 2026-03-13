import type {
  PlayerConnectionLog,
  PlayerConnectionQuery,
  PlayerProfile,
  PlayerProfileQuery,
  ProfileUpdateDto,
} from "~/types/player.types";

export const usePlayersStore = defineStore("players", {
  state: () => ({
    connectionLogsLoading: false,
    connectionLogs: [] as PlayerConnectionLog[],

    profilesLoading: false,
    profiles: [] as PlayerProfile[],

    createProfilesFromLogsLoading: false,
    refreshSteamDataLoading: false,
  }),
  getters: {},
  actions: {
    async fetchConnectionLogs(query: PlayerConnectionQuery) {
      this.connectionLogsLoading = true;
      this.connectionLogs = [];

      const data = await useAPI("/get-data/player-connection", "POST", query);

      this.connectionLogsLoading = false;

      if (data) this.connectionLogs = data as PlayerConnectionLog[];
    },
    async fetchProfiles(query: PlayerProfileQuery) {
      this.profilesLoading = true;
      this.profiles = [];

      const data = await useAPI("/player-profiles/list", "POST", query);

      this.profilesLoading = false;

      if (data) this.profiles = data as PlayerProfile[];
    },
    async createProfilesFromLogs() {
      this.createProfilesFromLogsLoading = true;
      await useAPI("/player-profiles/create-profiles-from-logs", "POST");
      this.createProfilesFromLogsLoading = false;
    },
    async refreshSteamData() {
      this.refreshSteamDataLoading = true;
      await useAPI("/player-profiles/refresh-data-from-steam", "POST");
      this.refreshSteamDataLoading = false;
    },
    async update(dto: ProfileUpdateDto) {
      try {
        const data = await useAPI("/player-profiles/update", "PUT", dto);

        if (!data) return;

        const profile = this.profiles.find((e) => e.id === dto.id);

        if (profile) {
          profile.note = (data as PlayerProfile).note;
        }
      } catch {
        console.error("Ошибка сохранения данных");
      }
    },
  },
});

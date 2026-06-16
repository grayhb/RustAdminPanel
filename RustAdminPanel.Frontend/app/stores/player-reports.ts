import type {
  PlayerReport,
  PlayerReportData,
  PlayerReportQuery,
} from "~/types/player-report.types";

export const usePlayerReportsStore = defineStore("player-reports", {
  state: () => ({
    playerReportsLoading: false,
    playerReports: [] as PlayerReport[],
  }),
  getters: {},
  actions: {
    async fetchPlayerReports(query: PlayerReportQuery) {
      this.playerReportsLoading = true;
      this.playerReports = [];

      const data = await useAPI("/player-reports/list", "POST", query);

      this.playerReportsLoading = false;

      if (data)
        this.playerReports = (data as PlayerReport[]).map((e) => {
          e.dataParsed = JSON.parse(e.data) as PlayerReportData;
          return e;
        });
    },
  },
});

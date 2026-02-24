export interface PlayerConnectionLog {
  steamName: string;
  steamId: string;
  connectionIp: string;
  connectionTimestamp: number;
}

export interface PlayerConnectionQuery {
  from?: string;
  to?: string;
  steamId?: string;
  steamName?: string;
}

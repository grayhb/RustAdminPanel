export interface PlayerConnectionLog {
  id: string;
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

export interface PlayerProfile {
  id: string;
  steamId: string;
  avatar: string;
  personaName: string;
  updatedAt: string;
  steamNames: string[];
  note: string;
  lastServerConnectionAt: string;
}

export interface PlayerProfileQuery {
  from?: string;
  to?: string;
  steamId?: string;
  steamName?: string;
}

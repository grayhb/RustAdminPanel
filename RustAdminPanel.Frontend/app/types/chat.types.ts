export interface ChatMessage {
  channel: number;
  message: string;
  time: number;
  steamName: string;
  steamId: string;
}

export interface ChatMessageQuery {
  from?: string;
  to?: string;
  steamId?: string;
  steamName?: string;
  messageSearch?: string;
  channel?: number;
}

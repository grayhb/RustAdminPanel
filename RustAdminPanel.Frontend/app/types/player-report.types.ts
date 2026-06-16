export interface PlayerReportQuery {
  from?: string;
  to?: string;
}

export interface PlayerReport {
  id: string;
  data: string;
  createdAt: string;

  dataParsed?: PlayerReportData;
}

export interface PlayerReportData {
  Subject: string;
  Message: string;
  Type: number;
  TargetId?: string;
  TargetName?: string;
  AppInfo: PlayerReportDataAppInfo;
}

export interface PlayerReportDataAppInfo {
  UserId: string;
  UserName: string;
}

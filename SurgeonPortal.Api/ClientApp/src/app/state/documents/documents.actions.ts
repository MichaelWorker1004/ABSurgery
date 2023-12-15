export class GetAllDocuments {
  static readonly type = '[Documents] Get all documents';
}

export class GetCertificateByType {
  static readonly type = '[Documents] Get certificate by type';
  constructor(public certificateType: number) {}
}

export class DownloadDocument {
  static readonly type = '[Documents] Download document';
  constructor(public payload: { documentId: number; documentName: string }) {}
}

export class DeleteCertificate {
  static readonly type = '[Documents] Delete certificate';
  constructor(public payload: { documentId: number }) {}
}

export class DeleteDocument {
  static readonly type = '[Documents] Delete document';
  constructor(public payload: { documentId: number }) {}
}

export class UploadDocument {
  static readonly type = '[Documents] Upload document';
  constructor(public payload: { model: FormData }) {}
}

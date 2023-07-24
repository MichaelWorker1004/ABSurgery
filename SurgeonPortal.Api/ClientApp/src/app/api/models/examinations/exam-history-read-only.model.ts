
export interface IExamHistoryReadOnlyModel {
    userId: number;
    examinationId: number;
    examinationName: string;
    examinationDate: string;
    documentId: number;
    status: string;
    result: string;
}

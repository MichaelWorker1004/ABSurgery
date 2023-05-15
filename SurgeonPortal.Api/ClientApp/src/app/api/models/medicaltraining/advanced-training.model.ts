
export interface IAdvancedTrainingModel {
  id?: number;
  userId?: number;
  trainingTypeId?: number;
  trainingType?: string;
  programId?: number;
  institutionName?: string;
  city?: string;
  state?: string;
  other?: string;
  startDate?: string;
  endDate?: string;
  createdByUserId?: number;
  createdAtUtc?: string;
  lastUpdatedAtUtc?: string;
  lastUpdatedByUserId?: number;
}

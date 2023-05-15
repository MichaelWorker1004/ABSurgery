
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
  startDate?: Date;
  endDate?: Date;
  createdByUserId?: number;
  createdAtUtc?: Date;
  lastUpdatedAtUtc?: Date;
  lastUpdatedByUserId?: number;
}

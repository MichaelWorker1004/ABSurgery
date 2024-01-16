import {
  Status,
  StatusPdAttestation,
} from 'src/app/shared/components/action-card/status.enum';

// status +1 to get correct value
export const statusTypes = [
  Status.Contingent, //-1
  Status.InProgress, //0
  Status.Completed, //1
  Status.PendingApproval, //2
  //Status.Alert, //3
];

export const statusTypesPDAttestation = [
  StatusPdAttestation.GmeNotCompleted, //-1
  StatusPdAttestation.GmeCompletedPdAvailable, //0
  StatusPdAttestation.GmeCompletedPdApproved, //1
  StatusPdAttestation.GmeCompletedPdPending, //2
  StatusPdAttestation.GmeCompletedPdSignedWaitingApproval, //3
];

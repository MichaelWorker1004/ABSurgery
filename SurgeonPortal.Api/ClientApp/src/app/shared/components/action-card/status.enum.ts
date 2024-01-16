export enum Status {
  Completed = 'completed',
  InProgress = 'in-progress',
  Contingent = 'contingent',
  Alert = 'alert',
  PendingApproval = 'pending-approval',
}

export enum StatusPdAttestation {
  GmeNotCompleted = 'GME-not-completed',
  GmeCompletedPdAvailable = 'available',
  GmeCompletedPdApproved = 'approved',
  GmeCompletedPdPending = 'pending-approval',
  GmeCompletedPdSignedWaitingApproval = 'signed-waiting-approval',
}

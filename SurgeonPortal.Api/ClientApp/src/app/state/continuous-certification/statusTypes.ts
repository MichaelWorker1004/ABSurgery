import { Status } from 'src/app/shared/components/action-card/status.enum';

// status +1 to get correct value
export const statusTypes = [
  Status.Contingent, //-1
  Status.InProgress, //0
  Status.Completed, //1
  Status.PendingApproval, //2
  //Status.Alert, //3
];

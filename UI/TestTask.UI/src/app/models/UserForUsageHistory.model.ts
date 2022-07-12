export interface UserForUsageHistory{
    email: string;
    jobName: string;
    apiUrlForJob: string;
    status: string;
    lastExecution?: Date;
}
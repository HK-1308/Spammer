export interface JobForListDto {
    id?: string;
    name: string;
    description: string;
    period: number;
    periodFormat: string;
    lastExecutionDate?: Date;
    nextExecutionDate: Date;
    apiUrlForJob: string;
    params: string;
}
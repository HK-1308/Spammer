export interface JobDto {
    id?: string,
    name: string;
    description: string;
    period: number;
    periodFormat: string;
    startDate: Date;
    apiUrlForJob: string;
    params: string;
}
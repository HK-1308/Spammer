export interface UserForAdminTableDto {
    email: string;
    numberOfTasks?: number;
    lastExecutionDate?: Date;
    nextExecutionDate?: Date;
}
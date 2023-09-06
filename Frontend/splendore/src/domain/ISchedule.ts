import { IBaseEntity } from "./IBaseEntity";

export interface ISchedule extends IBaseEntity {
    date: string,
    isBusy: boolean,
    stylistId: string
}
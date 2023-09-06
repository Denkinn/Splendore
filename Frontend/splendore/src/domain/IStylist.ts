import { IBaseEntity } from "./IBaseEntity";

export interface IStylist extends IBaseEntity {
    name: string,
    phoneNumber: string,
    salonId: string,
    appUserId: string
}
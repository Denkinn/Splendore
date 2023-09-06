import { IBaseEntity } from "./IBaseEntity";

export interface ISalon extends IBaseEntity {
    name: string,
    address: string,
    email: string,
    phoneNumber: string
}
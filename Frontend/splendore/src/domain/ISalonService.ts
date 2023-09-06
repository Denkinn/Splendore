import { IBaseEntity } from "./IBaseEntity";

export interface ISalonService extends IBaseEntity {
    price: string,
    time: string,
    serviceName: string,
    serviceType: string,
    salonName: string
}
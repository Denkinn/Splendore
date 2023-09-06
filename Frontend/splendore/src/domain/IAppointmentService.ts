import { IBaseEntity } from "./IBaseEntity";

export interface IAppointmentService extends IBaseEntity {
    appointmentId: string,
    salonServiceId: string,
    name: string,
    price: string,
    time: string,
    serviceName: string,
    serviceType: string
}
import { IBaseEntity } from "./IBaseEntity";

export interface IAppointment extends IBaseEntity {
    date: string,
    totalPrice: string,
    stylistId: string,
    appointmentStatusId: string,
    scheduleId: string,
    
    paymentMethodId: string,
    appUserId: string,
    salonId: string,
    salonName: string,
    stylistName: string,
    appointmentStatusName: string
}
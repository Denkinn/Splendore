import { IAppointment } from "../domain/IAppointment";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseEntityService";

export class AppointmentService extends BaseEntityService<IAppointment> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/Appointments', setJwtResponse);
    }


}
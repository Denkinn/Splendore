import { IAppointmentStatus } from "../domain/IAppointmentStatus";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseEntityService";

export class AppointmentStatusService extends BaseEntityService<IAppointmentStatus> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/AppointmentStatuses', setJwtResponse);
    }

    async getStatusId(name: string) {
        try {
            const response = await this.axios.get<IAppointmentStatus[]>('');

            if (response.status === 200) {
                return response.data.filter(status => status.name == name)[0].id;
            }
            return undefined;

        } catch (e) {
            console.log('error: ', (e as Error).message);
            return undefined;
        }
    }

}
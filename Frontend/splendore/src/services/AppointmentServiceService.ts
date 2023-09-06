import { IAppointmentService } from "../domain/IAppointmentService";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseEntityService";

export class AppointmentServiceService extends BaseEntityService<IAppointmentService> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/AppointmentServices', setJwtResponse);
    }

    async getAllByAppointmentId(apppointmentId: string ): Promise<IAppointmentService[] | undefined> {
        try {
            const response = await this.axios.get<IAppointmentService[]>('',
                {
                    params: {
                        "appointmentId": apppointmentId
                    }
                }
            );

            console.log('response', response);
            if (response.status === 200) {
                return response.data;
            }
            return undefined;

        } catch (e) {
            console.log('error: ', (e as Error).message);

            return undefined;
        }
    }

}
import { ISchedule } from "../domain/ISchedule";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseEntityService";

export class ScheduleService extends BaseEntityService<ISchedule> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/Schedules', setJwtResponse);
    }

    async getAllByStylistId(stylistId: string): Promise<ISchedule[] | undefined> {
        try {
            const response = await this.axios.get<ISchedule[]>('',
                {
                    params: {
                        "stylistId": stylistId
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
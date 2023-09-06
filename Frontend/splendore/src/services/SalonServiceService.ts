import { AxiosError } from "axios";
import { ISalon } from "../domain/ISalon";
import { ISalonService } from "../domain/ISalonService";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseEntityService";
import { IdentityService } from "./IdentityService";

export class SalonServiceService extends BaseEntityService<ISalonService> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/SalonServices', setJwtResponse);
    }

    async getAllBySalonId(salonId: string | undefined): Promise<ISalonService[] | undefined> {

        try {
            const response = await this.axios.get<ISalonService[]>('',
                {
                    params: {
                        "id": salonId
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
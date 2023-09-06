import { IStylist } from "../domain/IStylist";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseEntityService";

export class StylistService extends BaseEntityService<IStylist> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/Stylists', setJwtResponse);
    }

    async getAllBySalonId(salonId: string | undefined): Promise<IStylist[] | undefined> {

        try {
            const response = await this.axios.get<IStylist[]>('',
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
import { ISalon } from "../domain/ISalon";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseEntityService";

export class SalonService extends BaseEntityService<ISalon> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/Salons', setJwtResponse);
    }

    async getAll(): Promise<ISalon[] | undefined> {
        try {
            const response = await this.axios.get<ISalon[]>('');

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

    async findById(id?: string): Promise<ISalon | undefined> {

        try {
            const response = await this.axios.get<ISalon>('/' + id);

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
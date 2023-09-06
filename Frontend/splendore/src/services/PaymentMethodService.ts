import { IPaymentMethod } from "../domain/IPaymentMethod";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseEntityService";

export class PaymentMethodService extends BaseEntityService<IPaymentMethod> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/PaymentMethods', setJwtResponse);
    }

    async getPaymentMethodId(name: string) {
        try {
            const response = await this.axios.get<IPaymentMethod[]>('');

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
import { IReview } from "../domain/IReview";
import { IJWTResponse } from "../dto/IJWTResponse";
import { BaseEntityService } from "./BaseEntityService";


export class ReviewService extends BaseEntityService<IReview> {
    constructor(setJwtResponse: ((data: IJWTResponse | null) => void)) {
        super('v1/Reviews', setJwtResponse);
    }


}
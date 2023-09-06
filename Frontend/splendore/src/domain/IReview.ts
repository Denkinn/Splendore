import { IBaseEntity } from "./IBaseEntity";

export interface IReview extends IBaseEntity {
    salonId: string,
    rating: number,
    commentary: string
}
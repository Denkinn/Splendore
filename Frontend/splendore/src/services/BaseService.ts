import Axios, { AxiosInstance } from 'axios';

export abstract class BaseService {

    private static hostBaseURL = "https://localhost:7154/api/";

    protected axios: AxiosInstance;

    constructor(baseURL: string) {
        this.axios = Axios.create(
            {
                baseURL: BaseService.hostBaseURL + baseURL,
                headers: {
                    common: {
                        'Content-Type': 'application/json'
                    }
                }
            }
        );


        this.axios.interceptors.request.use(request => {
            console.log('Starting Request', JSON.stringify(request, null, 2))
            return request
        })
    }
}
import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../Root";
import { SalonService } from "../../services/SalonService";
import { ISalon } from "../../domain/ISalon";

import { Link } from "react-router-dom";

const Salons = () => {

    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const salonService = new SalonService(setJwtResponse!);

    const [salons, setSalons] = useState([] as ISalon[]);

    useEffect(() => {
        salonService.getAll().then(
                response => {
                    console.log(response);
                    if (response) {
                        setSalons(response);
                    }
                }
            );
    }, []);

    return (
        <>
        <div className="text-center">
            <h2 className="display-4">Salons</h2>
        </div>
        <div className="row">
            {salons.map(salon => 
            <div className="column">
                <div className="card" key={salon.id}>
                    <h3>{salon.name}</h3>
                    <p>{salon.address}</p>
                    <p>{salon.email}</p>
                    <Link to={"../salon/" + salon.id}><button className="button-62">View</button></Link>
                </div>
            </div>)}
        </div>
        </>
    )
}

export default Salons;
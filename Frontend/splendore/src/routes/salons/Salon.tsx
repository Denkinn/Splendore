import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../Root";
import { SalonService } from "../../services/SalonService";
import { ISalon } from "../../domain/ISalon";
import { Link, useLocation, useParams } from "react-router-dom";
import { SalonServiceService } from "../../services/SalonServiceService";
import { StylistService } from "../../services/StylistService";
import { ISalonService } from "../../domain/ISalonService";
import { IStylist } from "../../domain/IStylist";
import { ReviewService } from "../../services/ReviewService";
import { IReview } from "../../domain/IReview";
import "./salon.css";

const Salon = () => {

    const location = useLocation();

    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const salonService = new SalonService(setJwtResponse!);
    const salonServiceService = new SalonServiceService(setJwtResponse!);
    const stylistService = new StylistService(setJwtResponse!);
    const reviewService = new ReviewService(setJwtResponse!);

    const [salon, setSalon] = useState<ISalon>();
    const [salonServices, setServices] = useState([] as ISalonService[]);
    const [stylists, setStylists] = useState([] as IStylist[]);
    const [reviews, setReviews] = useState([] as IReview[]);

    let { id: salonId } = useParams();

    useEffect(() => {
        salonService.findById(salonId!).then(
            response => {
                if (response) {
                    setSalon(response);
                }
            }
        );
        salonServiceService.getAllBySalonId(salonId).then(
            response => {
                if (response) {
                    setServices(response);
                }
            }
        );
        stylistService.getAllBySalonId(salonId).then(
            response => {
                if (response) {
                    setStylists(response);
                }
            }
        )
        if (jwtResponse) {
            reviewService.getAll(jwtResponse).then(
                response => {
                    if (response) {
                        setReviews(response);
                    }
                }
            );
        }
    }, []);


    return (
        <>
            {location.state !== null ? <div className="alert alert-success">{location.state.message}</div> : ""}

            <div className="text-center">
                <h2 className="display-4">{salon?.name}</h2>
                <h4>{salon?.address}</h4>
                <h4>{salon?.email}</h4>
                <h4>{salon?.phoneNumber}</h4>
            </div>
            <br />

            <div className="row">
                <h3>Stylists</h3>
                <div className="alert alert-warning" style={{ 'display': jwtResponse == null ? '' : 'none' }}>Log in to make an appointment!</div>
                <ul className="list-group">
                    {stylists!.map(stylist =>
                        <li className="list-group-item" style={{ height: 55 }}>
                            <strong>{stylist.name} {stylist.phoneNumber}</strong>
                            {jwtResponse !== null ? <Link to={"../createAppointment/" + salonId + "/" + stylist.id} state={{ salonName: salon?.name, stylistName: stylist.name }}>
                                <button className="button-62 align-right">Make appointment</button>
                            </Link> : ""}
                        </li>)}
                </ul>
            </div>

            <div className="row">
                <h3>Available services</h3>
                {salonServices!.map(salonService =>
                    <div className="column">
                        <div className="card">
                            <h4>{salonService.serviceName}</h4>
                            <div>Price: {salonService.price} €</div>
                            <div>{salonService.time} minutes</div>
                        </div>
                    </div>)}
            </div>

            <div className="row">
                <h3>Reviews</h3>
                <ul className="list-group">
                    {reviews.length > 0 ? reviews.map(review =>
                        <li className="list-group-item">
                            <div className="star">
                                <b className={review.rating >= 1 ? "star-filled" : "star-blank"}>★</b>
                                <b className={review.rating >= 2 ? "star-filled" : "star-blank"}>★</b>
                                <b className={review.rating >= 3 ? "star-filled" : "star-blank"}>★</b>
                                <b className={review.rating >= 4 ? "star-filled" : "star-blank"}>★</b>
                                <b className={review.rating >= 5 ? "star-filled" : "star-blank"}>★</b>
                            </div>
                            <div>{review.commentary}</div>
                        </li>) : "No reviews yet"}
                </ul>

            </div>
        </>
    )
}

export default Salon;
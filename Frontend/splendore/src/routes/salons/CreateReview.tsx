import { Link, useNavigate, useParams } from "react-router-dom";
import "./salon.css";
import { useContext, useState, MouseEvent, useEffect } from "react";
import { ReviewService } from "../../services/ReviewService";
import { JwtContext } from "../Root";
import { IReview } from "../../domain/IReview";
import { SalonService } from "../../services/SalonService";
import { ISalon } from "../../domain/ISalon";

const CreateReview = () => {

    const navigate = useNavigate();

    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    let { salonId } = useParams();

    const reviewService = new ReviewService(setJwtResponse!);
    const salonService = new SalonService(setJwtResponse!);

    const [rating, setRating] = useState<number>();
    const [commentary, setCommentary] = useState<string>();
    const [salon, setSalon] = useState<ISalon>();

    useEffect(() => {
        if (jwtResponse) {
            salonService.find(jwtResponse, salonId).then(
                response => {
                    if (response) {
                        setSalon(response);
                    }
                }
            );
        }
    }, [])


    const [validationErrors, setValidationErrors] = useState([] as string[]);

    const handleSubmit = async (event: MouseEvent) => {
        event.preventDefault();

        if (rating === undefined) {
            setValidationErrors(["Please select rating!"]);
            return;
        }

        setValidationErrors([]);

        let review = {
            salonId: salonId,
            rating: rating,
            commentary: commentary
        } as IReview;

        let createdReview = await reviewService.post(jwtResponse!, review);

        if (createdReview === undefined) {
            setValidationErrors(["Error creating review"]);
            return;
        }

        navigate("/salon/" + salonId, { state: { message: "Review successfully submitted!" } });

    }

    return (
        <>
            <Link to={"../appointments"}><button className="button-62">Cancel</button></Link>
            <div className="text-center">
                <h1>Leave review to <b>{salon?.name}</b></h1>
            </div>

            <div style={{ 'display': validationErrors.length === 0 ? 'none' : '' }}>
                <div className="alert alert-danger">
                    {validationErrors.length > 0 ? validationErrors[0] : ''}
                </div>
            </div>

            <label>How would you rate this salon services</label><br />
            <div className="rating">
                <input id="star1" type="radio" name="rating" value="1" onChange={(e) => setRating(parseInt(e.target.value))} />
                <label htmlFor="star1">1</label>
                <input id="star2" type="radio" name="rating" value="2" onChange={(e) => setRating(parseInt(e.target.value))} />
                <label htmlFor="star2">2</label>
                <input id="star3" type="radio" name="rating" value="3" onChange={(e) => setRating(parseInt(e.target.value))} />
                <label htmlFor="star3">3</label>
                <input id="star4" type="radio" name="rating" value="4" onChange={(e) => setRating(parseInt(e.target.value))} />
                <label htmlFor="star4">4</label>
                <input id="star5" type="radio" name="rating" value="5" onChange={(e) => setRating(parseInt(e.target.value))} />
                <label htmlFor="star5">5</label>
            </div>
            <label>You can leave your commentary or suggestions (optional)</label><br />
            <textarea style={{ width: 400, height: 75, resize: "none" }} placeholder="Type here" onChange={(e) => setCommentary(e.target.value)}></textarea><br />
            
        
            <button className="button-62" onClick={(e) => handleSubmit(e)}>Submit review</button>

        </>
    )
}
export default CreateReview;
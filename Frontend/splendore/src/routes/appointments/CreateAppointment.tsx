import { useContext, useEffect, useState, MouseEvent } from "react";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../Root";
import { SalonServiceService } from "../../services/SalonServiceService";
import { ISalonService } from "../../domain/ISalonService";
import "./appointment.css"
import { AppointmentService } from "../../services/AppointmentService";
import { IAppointment } from "../../domain/IAppointment";
import { AppointmentStatusService } from "../../services/AppointmentStatusService";
import { AppointmentServiceService } from "../../services/AppointmentServiceService";
import { IAppointmentService } from "../../domain/IAppointmentService";
import { ScheduleService } from "../../services/ScheduleService";
import { ISchedule } from "../../domain/ISchedule";
import { PaymentMethodService } from "../../services/PaymentMethodService";
import { IPaymentMethod } from "../../domain/IPaymentMethod";


const CreateAppointment = () => {

    const navigate = useNavigate();
    const salonName = useLocation().state.salonName;
    const stylistName = useLocation().state.stylistName;

    const { salonId, stylistId } = useParams();

    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    const salonServiceService = new SalonServiceService(setJwtResponse!);
    const appointmentService = new AppointmentService(setJwtResponse!);
    const appointmentServiceService = new AppointmentServiceService(setJwtResponse!);
    const appointmentStatusService = new AppointmentStatusService(setJwtResponse!);
    const scheduleService = new ScheduleService(setJwtResponse!);
    const paymentMethodService = new PaymentMethodService(setJwtResponse!);

    const [salonServices, setServices] = useState([] as ISalonService[]);
    const [addedServices, setAddedServices] = useState([] as ISalonService[]);
    const [schedules, setSchedules] = useState([] as ISchedule[]);
    const [paymentMethods, setPaymentMethods] = useState([] as IPaymentMethod[]);
    const [activeStatusId, setStatusId] = useState<string>();

    useEffect(() => {
        salonServiceService.getAllBySalonId(salonId).then(
            response => {
                if (response) {
                    setServices(response);
                }
            }
        );
        appointmentStatusService.getStatusId("Active").then(
            response => {
                if (response) {
                    setStatusId(response);
                }
            }
        );
        if (stylistId) {
            scheduleService.getAllByStylistId(stylistId).then(
                response => {
                    if (response) {
                        setSchedules(response);
                    }
                }
            );
        }
        if (jwtResponse) {
            paymentMethodService.getAll(jwtResponse).then(
                response => {
                    if (response) {
                        setPaymentMethods(response);
                    }
                }
            )
        }
    }, []);


    // form handlers
    const [usedCategories, setUsedCategories] = useState([] as string[]);
    const [schedule, setSchedule] = useState<ISchedule>();
    const [paymentMethod, setPaymentMethod] = useState<IPaymentMethod>();
    const addService = (serviceToAdd: ISalonService) => {
        setUsedCategories([...usedCategories, serviceToAdd.serviceType]);
        setAddedServices([...addedServices, serviceToAdd]);
        setServices(salonServices.filter(service => service.id !== serviceToAdd.id));
    }
    const removeService = (serviceToRemove: ISalonService) => {
        setUsedCategories(usedCategories.filter(category => category !== serviceToRemove.serviceType));
        setAddedServices(addedServices.filter(service => service.id !== serviceToRemove.id));
        setServices([...salonServices, serviceToRemove])
    }


    // creation handlers
    const [validationErrors, setValidationErrors] = useState([] as string[]);

    const onSubmit = async (event: MouseEvent) => {
        event.preventDefault();

        if (addedServices.length === 0) {
            setValidationErrors(["You need to choose at least one service!"]);
            return;
        }
        if (schedule === undefined) {
            setValidationErrors(["Please select one of available dates!"]);
            return;
        }
        if (paymentMethod === undefined) {
            setValidationErrors(["Please select payment method!"]);
            return;
        }

        // remove errors
        setValidationErrors([]);

        let calculatedTotalPrice = addedServices.reduce((a, v) => a + parseInt(v.price), 0);

        let appointment = {
            stylistId: stylistId,
            scheduleId: schedule.id,
            date: new Date(schedule.date).toISOString(),
            totalPrice: calculatedTotalPrice.toString(),
            appointmentStatusId: activeStatusId,
            paymentMethodId: paymentMethod.id
        } as IAppointment;

        let createdAppointment = await appointmentService.post(jwtResponse!, appointment);

        if (createdAppointment === undefined) {
            setValidationErrors(["error creating appointment"]);
            return;
        }

        // set schedule status to busy
        schedule.isBusy = true;
        schedule.date = new Date(schedule.date).toISOString();
        await scheduleService.put(jwtResponse!, schedule.id, schedule)

        // add services to created appointment
        const appId = createdAppointment.id;
        addedServices.forEach(async service => {

            let appointmentService = {
                appointmentId: appId,
                salonServiceId: service.id
            } as IAppointmentService;

            let createdService = await appointmentServiceService.post(jwtResponse!, appointmentService)

            if (createdService === undefined) {
                setValidationErrors(["error creating appointment service"])
                return;
            }

        });

        navigate("/appointments", {state: {message: "Appointment successfully created!"}});

    }



    return (
        <>
            <Link to={"../salon/" + salonId}><button className="button-62">Back</button></Link>

            <div className="text-center">
                <h2 >Make new appointment at: <b>{salonName}</b></h2>
                <h3 >Stylist: <b>{stylistName}</b></h3>
            </div>
            <div style={{ 'display': validationErrors.length === 0 ? 'none' : '' }}>
                <div className="alert alert-danger">
                    {validationErrors.length > 0 ? validationErrors[0] : ''}
                </div>
            </div>

            <label>Select appointment date</label><br />
            <select name="date" onChange={(e) => setSchedule(JSON.parse(e.target.value))}>
                <option value="" disabled selected>Date</option>
                {schedules!.map(schedule =>
                    schedule.isBusy === false ?
                        <option value={JSON.stringify(schedule)}>{new Date(schedule.date).toLocaleString()}</option> : "")}
            </select><br />

            <label>Select payment method</label> <br />
            <select name="paymentMethod" onChange={(e) => setPaymentMethod(JSON.parse(e.target.value))}>
                <option value="" disabled selected>Payment method</option>
                {paymentMethods!.map(paymentMethod =>
                    <option value={JSON.stringify(paymentMethod)}>{paymentMethod.name}</option>)}
            </select><br />


            <div className="row">
                <h3>Select appointment services</h3>

                {addedServices!.map(addedService =>
                    // added services
                    <div className="column">
                        <div className="card">
                            <h4>{addedService.serviceName}</h4>
                            <div>Price: {addedService.price} €</div>
                            <div>{addedService.time} minutes</div>
                            <button onClick={() => removeService(addedService)} className="button-3 button-red">Remove service</button>
                        </div>
                    </div>)}

                {salonServices!.map(salonService =>
                    // check if there are already services of this category added
                    !addedServices.some(service => service.serviceType === salonService.serviceType) ?
                        // services to add
                        <div className="column">
                            <div className="card">
                                <h4>{salonService.serviceName}</h4>
                                <div>Price: {salonService.price} €</div>
                                <div>{salonService.time} minutes</div>
                                <button onClick={() => addService(salonService)} className="button-3">Add service</button>
                            </div>
                        </div> : "")}
            </div>

            <button
                onClick={(e) => onSubmit(e)}
                id="postAppointment" className="w-100 btn btn-lg btn-primary">Create appointment
            </button>
        </>
    );
}

export default CreateAppointment;
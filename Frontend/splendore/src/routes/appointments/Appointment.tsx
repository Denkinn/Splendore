import { useContext, useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../Root";
import { AppointmentService } from "../../services/AppointmentService";
import { IAppointment } from "../../domain/IAppointment";
import { AppointmentServiceService } from "../../services/AppointmentServiceService";
import { IAppointmentService } from "../../domain/IAppointmentService";
import { AppointmentStatusService } from "../../services/AppointmentStatusService";
import { ScheduleService } from "../../services/ScheduleService";
import { ISchedule } from "../../domain/ISchedule";
import { SalonService } from "../../services/SalonService";
import { ISalon } from "../../domain/ISalon";
import { StylistService } from "../../services/StylistService";
import { IStylist } from "../../domain/IStylist";

const Appointment = () => {
    let { appointmentId, scheduleId } = useParams();
    const navigate = useNavigate();

    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    const appointmentService = new AppointmentService(setJwtResponse!);
    const appointmentServiceService = new AppointmentServiceService(setJwtResponse!);
    const appointmentStatusService = new AppointmentStatusService(setJwtResponse!);
    const scheduleService = new ScheduleService(setJwtResponse!);
    const salonService = new SalonService(setJwtResponse!);
    const stylistService = new StylistService(setJwtResponse!);

    const [appointment, setAppointment] = useState<IAppointment>();
    const [appointmentServices, setAppointmentServices] = useState([] as IAppointmentService[]);
    const [cancelledStatusId, setStatusId] = useState<string>();
    const [schedule, setSchedule] = useState<ISchedule>();

    const [salon, setSalon] = useState<ISalon>();
    const [stylist, setStylist] = useState<IStylist>();

    useEffect(() => {
        if (jwtResponse) {
            appointmentService.find(jwtResponse, appointmentId).then(
                response => {
                    if (response) {
                        setAppointment(response);
                    }
                }
            );
            appointmentServiceService.getAllByAppointmentId(appointmentId!).then(
                response => {
                    if (response) {
                        setAppointmentServices(response);
                    }
                }
            );
            scheduleService.find(jwtResponse, scheduleId).then(
                response => {
                    if (response) {
                        setSchedule(response);
                    }
                }
            );
            appointmentStatusService.getStatusId("Cancelled").then(
                response => {
                    if (response) {
                        setStatusId(response);
                    }
                }
            );
        }

    }, [jwtResponse]);

    useEffect(() => {
        if (appointment) {
            salonService.findById(appointment?.salonId).then(
                response => {
                    if (response) {
                        setSalon(response);
                    }
                }
            );

            if (jwtResponse) {
                stylistService.find(jwtResponse, appointment.stylistId).then(
                    response => {
                        if (response) {
                            setStylist(response);
                        }
                    }
                );
            }
        }
    }, [appointment])

    const cancelAppointment = async () => {

        if (jwtResponse && appointment && cancelledStatusId && schedule) {
            appointment.appointmentStatusId = cancelledStatusId;
            appointment.date = new Date(appointment.date).toISOString();
            schedule.date = new Date(schedule.date).toISOString();
            schedule.isBusy = false;

            await scheduleService.put(jwtResponse, schedule.id, schedule);
            await appointmentService.put(jwtResponse, appointment.id, appointment);

            navigate("../appointments", { state: { message: "Appointment cancelled!" } });
        }

    }

    const drawButton = () => {
        if (appointment) {
            if (appointment.appointmentStatusName === "Active") {
                return (
                    <button onClick={() => cancelAppointment()} className="button-62">Cancel appointment</button>
                );
            } else if (appointment.appointmentStatusName === "Over") {
                return (
                    <Link to={"../createReview/" + appointment.salonId}><button className="button-62">Leave review</button></Link>
                );
            } else {
                return (<></>);
            }
        }
    }

    if (appointment !== undefined && schedule !== undefined && salon !== undefined && stylist !== undefined) {
        return (
            <>
                <Link to="../appointments"><button className="button-62">Back</button></Link><br />
                <div className="text-center">
                    <h1>Appointment at <b>{salon.name}</b></h1>
                </div>


                <table className="apn-table">
                    <tbody>
                        <tr>
                            <td>Address</td>
                            <td>{salon.address}</td>
                        </tr>
                        <tr>
                            <td>Date and time</td>
                            <td>{new Date(appointment.date).toLocaleDateString()}, {new Date(appointment.date).toLocaleTimeString()}</td>
                        </tr>
                        <tr>
                            <td>Total price</td>
                            <td>{appointment.totalPrice} €</td>
                        </tr>
                        <tr>
                            <td>Contact stylist</td>
                            <td>{stylist.name}, {stylist.phoneNumber}</td>
                        </tr>
                    </tbody>
                </table>

                <div className="row">
                    <h3>Appointment services</h3>
                    {appointmentServices.map(service =>
                        <div className="column">
                            <div className="card">
                                <h4>{service.serviceName}</h4>
                                <div>Price: {service.price} €</div>
                                <div>{service.time} minutes</div>
                            </div>
                        </div>)}
                </div>
                <br />

                {drawButton()}

            </>
        );
    } else {
        return (
            <>
            </>
        )
    }

}

export default Appointment;
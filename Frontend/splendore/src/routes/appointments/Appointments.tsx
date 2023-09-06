import { useContext, useEffect, useState } from 'react';
import { JwtContext } from "../Root";
import { AppointmentService } from '../../services/AppointmentService';
import { IAppointment } from '../../domain/IAppointment';
import "./appointment.css"
import { Link, useLocation } from 'react-router-dom';
import { AppointmentStatusService } from '../../services/AppointmentStatusService';

const Appointments = () => {
    const location = useLocation();

    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    const appointmentService = new AppointmentService(setJwtResponse!);
    const appointmentStatusService = new AppointmentStatusService(setJwtResponse!);

    const [appointments, setAppointments] = useState([] as IAppointment[]);
    const [overStatusId, setStatusId] = useState<string>();

    useEffect(() => {
        if (jwtResponse) {
            appointmentService.getAll(jwtResponse).then(
                response => {
                    console.log(response);
                    if (response) {
                        setAppointments(response);
                    } else {
                        setAppointments([]);
                    }
                }
            );
        }
        appointmentStatusService.getStatusId("Over").then(
            response => {
                if (response) {
                    setStatusId(response);
                }
            }
        );
    }, [jwtResponse]);


    const diffInHours = (dt1: Date, dt2: Date) => {
        let diff = (dt2.getTime() - dt1.getTime()) / 1000;
        diff /= (60 * 60);
        return Math.floor(diff);
    }

    const getAlertStyle = (appointment: IAppointment) => {
        if (appointment.appointmentStatusName === "Active") {
            return "alert-success";
        } else if (appointment.appointmentStatusName === "Over") {
            return "alert-primary";
        } else if (appointment.appointmentStatusName === "Cancelled") {
            return "alert-danger";
        }
    }

    const setAppointmentOver = async (appointment: IAppointment) => {
        if (jwtResponse && overStatusId) {
            appointment.appointmentStatusId = overStatusId;
            appointment.date = new Date(appointment.date).toISOString();
            await appointmentService.put(jwtResponse, appointment.id, appointment);
        }
    }

    appointments.forEach(appointment => {
        if (diffInHours(new Date(appointment.date), new Date()) >= 1 && appointment.appointmentStatusName === "Active") {
            setAppointmentOver(appointment);
        }
    })

    const drawAppointment = (appointment: IAppointment) => {
        return (
            <li className="list-group-item">
                Date: {new Date(appointment.date).toLocaleDateString()} <br />
                Time: {new Date(appointment.date).toLocaleTimeString()} <br />
                Price: {appointment.totalPrice} € <br />
                Salon: {appointment.salonName} <br />
                Stylist: {appointment.stylistName} <br />
                <div className={'alert ' + getAlertStyle(appointment)}>{appointment.appointmentStatusName}</div>
                <Link to={"../appointment/" + appointment.id + "/" + appointment.scheduleId}><button className='button-62 align-right'>View</button></Link>
            </li>
        );
    }


    return (
        <>
            {location.state !== null ? <div className="alert alert-success">{location.state.message}</div> : ""}
            
            <div className='row'>
                <div className="text-center">
                    <h2 className="display-4">Your appointments</h2>
                </div>
                <h4>Active</h4>
                <ul className="list-group">
                    {appointments.map(appointment =>
                        appointment.appointmentStatusName === "Active" ? drawAppointment(appointment) : ""
                    )}
                </ul>
                <h4>Over</h4>
                <ul className="list-group">
                    {appointments.map(appointment =>
                        appointment.appointmentStatusName === "Over" ? drawAppointment(appointment) : ""
                    )}
                </ul>
                <h4>Cancelled</h4>
                <ul className="list-group">
                    {appointments.map(appointment =>
                        appointment.appointmentStatusName === "Cancelled" ? drawAppointment(appointment) : ""
                    )}
                </ul>
            </div>
        </>
    )


}

export default Appointments;
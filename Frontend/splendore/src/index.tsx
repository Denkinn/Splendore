import 'jquery';
import 'popper.js';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'font-awesome/css/font-awesome.min.css';

import './site.css'
import React from 'react';
import ReactDOM from 'react-dom/client';
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import Root from './routes/Root';
import ErrorPage from './routes/ErrorPage';
import Login from './routes/identity/Login';
import Register from './routes/identity/Register';
import Privacy from './routes/Privacy';
import Info from './routes/identity/Info';
import Home from './routes/Home';
import Appointments from './routes/appointments/Appointments';
import Salons from './routes/salons/Salons';
import Salon from './routes/salons/Salon';
import CreateAppointment from './routes/appointments/CreateAppointment';
import Appointment from './routes/appointments/Appointment';
import CreateReview from './routes/salons/CreateReview';

const router = createBrowserRouter([
    {
        path: "/",
        element: <Root />,
        errorElement: <ErrorPage />,
        children: [
            {
                path: "/",
                element: <Home />
            },
            {
                path: "login/",
                element: <Login />
            },
            {
                path: "register/",
                element: <Register />
            },
            {
                path: "info/",
                element: <Info />,
            },
            {
                path: "privacy/:id",
                element: <Privacy />
            },
            {
                path: "salons",
                element: <Salons />
            },
            {
                path: "salon/:id",
                element: <Salon />
            },
            {
                path: "appointments",
                element: <Appointments />
            },
            {
                path: "appointment/:appointmentId/:scheduleId",
                element: <Appointment/>
            },
            {
                path: "createAppointment/:salonId/:stylistId",
                element: <CreateAppointment />
            },
            {
                path: "createReview/:salonId",
                element: <CreateReview />
            }
        ]
    }
]);

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);
root.render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>
);


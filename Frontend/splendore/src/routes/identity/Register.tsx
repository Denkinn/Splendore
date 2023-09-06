import { useState, MouseEvent, useContext } from "react";
import RegisterFormView from "./RegisterFormView";
import { IRegisterData } from "../../dto/IRegisterData";
import { IdentityService } from "../../services/IdentityService";
import { JwtContext } from "../Root";
import { useNavigate } from "react-router-dom";

const Register = () => {
    const navigate = useNavigate();
    const {jwtResponse, setJwtResponse} = useContext(JwtContext);
    const identityService = new IdentityService();

    const [values, setInput] = useState({
        password: "",
        confirmPassword: "",
        email: "",
        firstName: "",
        lastName: ""
    } as IRegisterData);

    const [validationErrors, setValidationErrors] = useState([] as string[]);

    const handleChange = (target: EventTarget & HTMLInputElement) => {
        // console.log(target.value)
        setInput({ ...values, [target.name]: target.value });
    }

    const onSubmit = async (event: MouseEvent) => {
        // console.log('onSubmit', event);
        event.preventDefault();

        if (values.firstName.length == 0 || values.lastName.length == 0 || values.email.length == 0 || values.password.length == 0 || values.password != values.confirmPassword) {
            setValidationErrors(["Bad input values!"]);
            return;
        }

        // remove errors
        setValidationErrors([]);

        // register the user, get jwt and refreshToken
        var jwtData = await identityService.register(values);

        if (jwtData == undefined) {
            // TODO: get error info
            setValidationErrors(["no jwt"]);
            return;
        }

        if (setJwtResponse) {
            setJwtResponse(jwtData);
            navigate("/");
        }
    }

    return (
        <RegisterFormView values={values} handleChange={handleChange} onSubmit={onSubmit} validationErrors={validationErrors} />
    );
}

export default Register;
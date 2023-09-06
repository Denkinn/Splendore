import { useNavigate } from "react-router-dom";
import LoginFormView from "./LoginFormView";
import { ILoginData } from "../../dto/ILoginData";
import { useContext, useState, MouseEvent } from "react";
import { JwtContext } from "../Root";
import { IdentityService } from "../../services/IdentityService";

const Login = () => {
    const navigate = useNavigate();
    const {jwtResponse, setJwtResponse} = useContext(JwtContext);
    const identityService = new IdentityService();

    const [values, setInput] = useState({
        email: "",
        password: "",
    } as ILoginData);

    const [validationErrors, setValidationErrors] = useState([] as string[]);

    const handleChange = (target: EventTarget & HTMLInputElement) => {
        setInput({ ...values, [target.name]: target.value });
    }

    const onSubmit = async (event: MouseEvent) => {
        console.log('onSubmit', event);
        event.preventDefault();

        if (values.email.length == 0 || values.password.length == 0) {
            setValidationErrors(["Bad input values!"]);
            return;
        }
        // remove errors
        setValidationErrors([]);

        var jwtData = await identityService.login(values);

        if (jwtData == undefined) {
            // TODO: get error info
            setValidationErrors(["no jwt"]);
            return;
        } 

        if (setJwtResponse){
             setJwtResponse(jwtData);
             navigate("/");
        }
    }


    return (
        <LoginFormView values={values} handleChange={handleChange} onSubmit={onSubmit} validationErrors={validationErrors} />
    );
}

export default Login;
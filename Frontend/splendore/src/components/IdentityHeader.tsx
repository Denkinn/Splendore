import { useContext } from "react";
import { JwtContext } from "../routes/Root";
import { Link, useNavigate } from "react-router-dom";
import { IdentityService } from "../services/IdentityService";
import jwt_decode from "jwt-decode";

const IdentityHeader = () => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const navigate = useNavigate();
    const identityService = new IdentityService();

    const logout = () => {
        if (jwtResponse)
            identityService.logout(jwtResponse).then(response => {
                if (setJwtResponse)
                //TODO (fix null error, make userinfo page)
                    setJwtResponse(null);
                navigate("/");
            });
    }

    if (jwtResponse) {
        let jwtObject: any = jwt_decode(jwtResponse.jwt);

        console.log(jwtObject);

        return (
            <>
                <li className="nav-item">
                    <Link to="appointments" className="nav-link text-dark">My appointments</Link>
                </li>
                <li className="nav-item">
                    <Link to="info" className="nav-link text-dark">
                        <UserInfo jwtObject={jwtObject} />
                    </Link>
                </li>
                <li className="nav-item">
                    <a onClick={(e) => {
                        e.preventDefault();
                        logout();
                    }} className="nav-link text-dark" href="#">Logout</a>
                </li>
            </>
        );

    } else {
        return (
            <>
                <li className="nav-item">
                    <Link to="register" className="nav-link text-dark" title="Manage">Register</Link>
                </li>
                <li className="nav-item">
                    <Link to="login" className="nav-link text-dark" title="Manage">Login</Link>
                </li>
            </>
        )
    }
}

interface IUserInfoProps {
    jwtObject: any
}

const UserInfo = (props: IUserInfoProps) => {
    return (
        <>
            {props.jwtObject['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname'] + ' '}
            {props.jwtObject['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname']+ ' '}
            ({props.jwtObject['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']})
        </>
    );
}

export default IdentityHeader;
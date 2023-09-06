import { Link } from "react-router-dom";

const Home = () => {
    return (
        <div className="text-center">
            <h1 className="display-4">Splendore</h1>
            
            <Link to="/salons"><button className="button-62">View available salons</button></Link>
        </div>
        
    );
}

export default Home;

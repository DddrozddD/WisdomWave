import React    from "react";
import template from "./MainPage.jsx";
import Layout from "../Layout/Layout.js";
import back1 from "../images/01picture.png"

class MainPage extends React.Component {
  render() {
    return (
    <>
    <Layout/>
    <div>
      <img src={back1} alt="Back1" className="float-right"/>
    </div>
    </>  
    );
  }
}

export default MainPage;


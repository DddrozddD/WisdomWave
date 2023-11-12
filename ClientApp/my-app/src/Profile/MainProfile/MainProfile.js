import React    from "react";
import template from "./MainProfile.jsx";
import { NavLink } from "react-router-dom";
import Layout from "../../Layout/Layout.js";

class MainProfile extends React.Component {

  showEdu=()=>{
    if(document.getElementById("showed_edu").style.display == "none"){
    document.getElementById("showed_edu").style.display = "block";
    }
    else if(document.getElementById("showed_edu").style.display == "block"){
      document.getElementById("showed_edu").style.display = "none";
    }
  }

  render() {  

    return (
      
      
      <>
     
      <div className="links_mainprofile">
        <NavLink to="/profile/userProfile" className={"link_mainprofile"}>
          Профіль
        </NavLink>
        <NavLink to="/profile/userCourses" className={"link_mainprofile"}>
          Авторські курси
        </NavLink>  
        <NavLink onClick={this.showEdu} className={"link_mainprofile"}>
        Навчання
        </NavLink>

       <div id="showed_edu" style={{display: "none"}}>
        <NavLink to="/profile/userStuding" className={"link_mainprofile link_education"}>
        Навчання
        </NavLink>
        <NavLink to="/profile/userStuding" className={"link_mainprofile link_education"}>
        Навчання
        </NavLink>
        </div>
      </div>
      </>
    );
  }
}

export default MainProfile;

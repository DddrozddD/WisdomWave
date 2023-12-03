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
        <NavLink to="/user-profile" className={"link_mainprofile"} id="profile"> 
          Профіль
        </NavLink>
        <NavLink to="/user-courses" className={"link_mainprofile"} id="ownCourses">
          Авторські курси
        </NavLink>  
        <NavLink onClick={this.showEdu} className={"link_mainprofile"} id="education">
        Навчання
        </NavLink>

       <div id="showed_edu" style={{display: "none"}} >
        <NavLink to="/user-studing" id="studing" className={"link_mainprofile link_education"}>
        Навчання
        </NavLink>
        <br/>
        <NavLink to="/user-studed" className={"link_mainprofile link_education"} id="studed">
        Навчання
        </NavLink>
        </div>
      </div>
      </>
    );
  }
}

export default MainProfile;

import React    from "react";
import template from "./MainCoursePage.jsx";
import { getCookie } from "../../CookieHandler.js";
import RightBack from "../../images/Сonstructor/constrRight.png"
import Layout from "../../Layout/Layout.js";
import iconCourse from "../../images/CourseDisplay/iconCourse20.png";
import {variables} from './../../Variables.js';

class MainCoursePage extends React.Component {

  constructor(props) {
    super(props);

    this.state = {
     courseId:"",
     courseName: ""

    }
}


  componentDidMount=async()=>{
   this.setState({courseId: getCookie("ShowingCourseId")})

  }


  getCourse=async()=>{
    try {
      const response = await fetch(variables.API_URL + 'course'+getCookie("ShowingCourseId"));
      const data = await response.json();      
      this.setState({  courseName: data.courseName});
     
      
    } catch (error) {
      console.error("Error fetching page:", error);
    }
}
  render() {
    return (
    <>
    <Layout/>
    <div className="background">
    <img src={RightBack} alt="constrRight" className="right_back_constr"/>
    <div className="MainCoursePage">
    <img src={iconCourse} alt="iconCourse" className="iconCourse"/>
    <br/>

    <h2>{this.state.courseName}</h2>
    </div>
    </div>
    </>
    );
  }
}

export default MainCoursePage;

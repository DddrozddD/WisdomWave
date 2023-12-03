import React    from "react";
import template from "./MainCoursePage.jsx";
import { getCookie, setCookie } from "../../CookieHandler.js";
import RightBack from "../../images/Сonstructor/constrRight.png"
import Layout from "../../Layout/Layout.js";
import iconCourse from "../../images/CourseDisplay/iconCourse20.png";
import {variables} from './../../Variables.js';
import { NavLink } from "react-router-dom";

class MainCoursePage extends React.Component {

  constructor(props) {
    super(props);

    this.state = {
     courseId:"",
     courseName: "",
     courseCreatorName:"",
     courseCreatorId: ""
    }
}


  componentDidMount=async()=>{
   this.setState({courseId: getCookie("ShowingCourseId")})
   this.getCourse();
  }


  getCourse=async()=>{
    try {
      const response = await fetch(variables.API_URL + 'courses/'+getCookie("ShowingCourseId"));
      const data = await response.json();      
      this.setState({  courseName: data.courseName, courseCreatorName: data.creatorUserName});
     
      
    } catch (error) {
      console.error("Error fetching page:", error);
    }
}

checkOnCreator=async()=>{
  try {
    const response = await fetch(variables.API_URL + 'courses/checkCourseOfCreator/'+this.state.courseId+"/"+getCookie("ShowingCourseId"));
    const data = await response.json();      
   return data;
    
  } catch (error) {
    console.error("Error fetching page:", error);
  }
}

createCourse=async()=>{
  setCookie("EditCourseId", this.state.courseId);
  window.location.href="http://localhost:3000/edit-course"
}
  render() {
    return (
    <>
    <div className="MainCoursePage">
<div className="left_site">
    <h2>{this.state.courseName}</h2>
    <p>Творець: {this.state.courseCreatorName}</p>
    {this.checkOnCreator() ? (
      <>
      <br/>
      <NavLink className="orangeBtn" onClick={this.createCourse}>Редагувати</NavLink>
      <br/>
      <br/>
      <br/>
      </>

    ):(<>
    <br/>
    <br/>
    </>)}
    <NavLink className="link_mainprofile link_course" to="/course-info">Загальна інформація</NavLink>
    <br/>
    <br/>
    <NavLink className="link_mainprofile link_course" to="/course-info">Матеріали курсу</NavLink>
    </div>
    </div>
   
    </>
    );
  }
}

export default MainCoursePage;

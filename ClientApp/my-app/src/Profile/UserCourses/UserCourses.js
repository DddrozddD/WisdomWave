import React    from "react";
import template from "./UserCourses.jsx";
import Layout from "../../Layout/Layout.js";
import MainProfile from "../MainProfile/MainProfile.js";
import {variables} from './../../Variables.js';
import { setCookie, deleteCookie, getCookie } from './../../CookieHandler.js';
import profile_right from './../../images/profile_right.png'
import profile_left from './../../images/profile_left.png'

class UserCourses extends React.Component {

  constructor(props){
    super(props);

    this.state={
      userCourses: []
    }
  }

  componentDidMount=async()=>{
    this.GetUserCourses();
  }
  GetUserCourses=async()=>{
    try{


      await fetch(variables.API_URL+'courses/GetUserCourses/'+getCookie("UserSecretKey"))
    .then(response=>response.json())
    .then(data=>{
      if(data.title!="Bad Request"){
        console.log(data);
        this.setState({userCourses:data});
      }

    });
  }
  catch(error){
    console.error("Помилка:", error);
  }
  }

  showCourse=(id)=>{
    setCookie("ShowingCourseId", id)
    window.location.href="http://localhost:3000/display-course";
  }
  render() {
    
    return (
      <>
      <Layout/>
      <img src={profile_right} alt="profile_right" className="back_right"/>
      <img src={profile_left} alt="profile_left" className=" left_back_profile"/>

      <div className="profile_body">
      <div>
      <MainProfile/>
      </div>
        <div className="user_courses_list">
      <h1>Авторські курси</h1>

      <button className="orangeBtn" onClick={()=>{window.location.href="http://localhost:3000/add-course"}}>+ Додати курс</button>
      <div className="coursesView">
          {this.state.userCourses.map((course) => {
          
              
              return (
                <div className="courseCard" key={course.id} onClick={()=>this.showCourse(course.id)}>
                  <h3>{course.courseName}</h3>
                  <p>{course.creatorUserName}</p>
                </div>
              );
             
          })}
        </div>
        </div>
        
      </div>
      
      </>
    );
  }
}

export default UserCourses;

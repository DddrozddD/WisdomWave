import React    from "react";
import template from "./UserLearning.jsx";
import Layout from "../../Layout/Layout.js";
import MainProfile from "../MainProfile/MainProfile.js";
import profile_right from './../../images/profile_right.png'
import profile_left from './../../images/profile_left.png'
import {variables} from './../../Variables.js';
import { setCookie, deleteCookie, getCookie } from './../../CookieHandler.js';
class UserLearning extends React.Component {
  constructor(props){
    super(props);

    this.state={
      courses: []
    }
  }
  componentDidMount = () => {
    this.GetLearningCourses();
  };

  GetLearningCourses=async()=>{
    try{


      await fetch(variables.API_URL+'courses/GetUserLearningCourses/'+getCookie("UserSecretKey"))
    .then(response=>response.json())
    .then(data=>{
      if(data.title!="Bad Request"){
        console.log(data);
        this.setState({courses:data});
      }

    });
  }
  catch(error){
    console.error("Помилка:", error);
  }
  }

  showCourse=(id)=>{
    setCookie("ShowingCourseId", id)
    window.location.href=variables.PAGE_URL+"display-course";
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
        <div className="user_learning_courses_list">
      <h1>Курси, що вивчаються</h1>
      <div className="coursesView">
          {this.state.courses.map((course) => {
          
              
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

export default UserLearning;

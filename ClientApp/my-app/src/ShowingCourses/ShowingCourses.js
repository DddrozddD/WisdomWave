import React from "react";
import template from "./ShowingCourses.jsx";
import { variables } from "./../Variables.js";
import Layout from "../Layout/Layout.js";
import { setCookie, deleteCookie, getCookie } from "./../CookieHandler.js";

class ShowingCourses extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      courses: [],
      userId: ""
    };
  }

  componentDidMount = () => {
    this.getUserId();
    this.getAllCourses();
  };



  getUserId=async()=>{
    try{


      await fetch(variables.API_URL+'authorization/GetUser/'+getCookie("UserSecretKey"))
    .then(response=>response.json())
    .then(data=>{
      if(data.title!="Bad Request"){
        console.log(data);
        this.setState({userId: data.id});
      }
      

    });
  }
  catch(error){
    console.error("Помилка:", error);
  }
  }

  getAllCourses = async () => {
    try {
      const response = await fetch(variables.API_URL + "courses/");
      const data = await response.json();
      this.setState({ courses: data });
    
    } catch (error) {
      console.error("Error fetching page:", error);
    }
  };

  getCourseUserName = async (id) => {
    try {
      const response = await fetch(variables.API_URL + "courses/getCourseUser/"+id);
      const data = await response.json();
     return(
      <>
      <p>{data.name} {data.surname}</p>
      </>
     )
    } catch (error) {
      console.error("Error fetching page:", error);
    }
  };

 

  render() {
    return (
      <>
        <Layout />
        <div className="coursesView">
          {this.state.courses.map((course) => {
          
              if(course.creatorUserId != this.state.userId){
              return (
                <div className="courseCard" key={course.id}>
                  <h3>{course.courseName}</h3>
                  <p>{course.creatorUserName}</p>
                </div>
              );
              }
            return null
          })}
        </div>
      </>
    );
  }
}

export default ShowingCourses;

import React    from "react";
import template from "./CourseInfo.jsx";
import MainCoursePage from "../MainCoursePage/MainCoursePage.js";
import {variables} from './../../Variables.js';
import RightBack from "../../images/Сonstructor/constrRight.png"
import {Layout} from './../../Layout/Layout.js';
import { setCookie, deleteCookie, getCookie } from './../../CookieHandler.js';

class CourseInfo extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
     courseId:"",
     courseName: "",
     courseCreatorName:"",
     courseKnowlage:"",
     courseEducation:"",
     courseTheme:"",
     countLearningUsers: 0,
     countCompletedUsers: 0,
     courseDesc:""
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
    this.setState({  courseName: data.courseName, courseCreatorName: data.creatorUserName, 
    courseEducation: data.categories[1].categoryName, courseKnowlage: data.categories[0].categoryName, courseTheme: data.categories[2].categoryName,
     courseDesc: data.description});

  if(data.learningUsers != null){
    this.setState({countLearningUsers: data.learningUsers.count()});
  }
  if(data.completedUsers != null){
    this.setState({countCompletedUsers: data.completedUsers.count()});
  }
    
  } catch (error) {
    console.error("Error fetching page:", error);
  }
}


  render() {
    return (
      <>
          <Layout/>
          <img src={RightBack} alt="constrRight" className="right_back_constr"/>
          <div className="background">

          <div className="course_view_site">
          <MainCoursePage/>
          </div>
          <div className="course_view_site info_view">
            <h1>Загальна інформація про курс</h1>
            <p>Предметна область знань: {this.state.courseKnowlage}</p>
            <p>Сфера навчання: {this.state.courseEducation}</p>
            <p>Тема: {this.state.courseTheme}</p>
            <p>Опис: {this.state.courseDesc}</p>
            <p>Кількість людей, які проходять цей курс: {this.state.countLearningUsers}</p>
            <p>Кількість людей, які пройшли цей курс: {this.state.countCompletedUsers}</p>
            </div>
            </div>
      </>
    )
    ;
  }
}

export default CourseInfo;

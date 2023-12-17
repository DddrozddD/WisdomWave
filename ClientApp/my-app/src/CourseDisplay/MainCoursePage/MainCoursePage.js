import React    from "react";
import template from "./MainCoursePage.jsx";
import { getCookie, setCookie } from "../../CookieHandler.js";
import RightBack from "../../images/Сonstructor/constrRight.png"
import Layout from "../../Layout/Layout.js";
import iconCourse from "../../images/CourseDisplay/iconCourse20.png";
import {variables} from './../../Variables.js';
import Arrow from "./../../images/arrow.png"
import { NavLink } from "react-router-dom";
import warning from "./../../images/ep_warning.png"

class MainCoursePage extends React.Component {

  constructor(props) {
    super(props);

    this.state = {
     courseId:"",
     courseName: "",
     courseCreatorName:"",
     courseCreatorId: "",
     courseKnowlage:"",
     courseEducation:"",
     courseTheme:"",
     countLearningUsers: 0,
     countCompletedUsers: 0,
     isCourseCreator: false, 
     courseDesc:"",
     isCourseLerner: false,
     courseUnits: [],
     isCompletedTests: [],
     isCompletedPages: []
    }
}


  componentDidMount=async()=>{
   this.setState({courseId: getCookie("ShowingCourseId")})
   this.getCourse();
   this.checkCourseCreator();
   this.checkCourseLerner();
   this.getCourseUnits();
  }


  getCourse=async()=>{
    try {
      const response = await fetch(variables.API_URL + 'courses/'+getCookie("ShowingCourseId"));
      const data = await response.json();      
      this.setState({  courseName: data.courseName, courseCreatorName: data.creatorUserName,
        courseEducation: data.categories[1].categoryName, courseKnowlage: data.categories[0].categoryName, courseTheme: data.categories[2].categoryName,
        courseDesc: data.description});
        if(data.learnerUsers != null){
          if(data.learnerUsers.length != 0){
          this.setState({countLearningUsers: data.learningUsers.count()});
          }
        }
        if(data.completedUsers != null){
          if(data.completedUsers.length != 0){
          this.setState({countCompletedUsers: data.completedUsers.count()});
          }
        }
      
    } catch (error) {
      console.error("Error fetching page:", error);
    }
}

show = (id) =>{
  if(document.getElementById(id).style.display == "none"){
    document.getElementById(id).style.display = "block";
    document.getElementById("arrowBtn_"+id).classList.remove("rotated");
  }
  else{
    document.getElementById(id).style.display = "none";
    document.getElementById("arrowBtn_"+id).classList.add("rotated");
  } 
  
}

checkCourseCreator = async () => {
  try {
    const response = await fetch(
      variables.API_URL +
        'courses/checkCourseOfCreator/' +
        getCookie("ShowingCourseId") +
        "/" +
        getCookie("UserSecretKey")
    );
    const data = await response.json();
    this.setState({ isCourseCreator: data });
    return true;
  } catch (error) {
    console.error("Error fetching page:", error);
    return false;
  }
  return false;
};

checkCourseLerner = async () => {
  try {
    const response = await fetch(
      variables.API_URL +
        'courses/checkCourseOfLerner/' +
        getCookie("ShowingCourseId") +
        "/" +
        getCookie("UserSecretKey")
    );
    const data = await response.json();
    this.setState({ isCourseLerner: data });
    return true;
  } catch (error) {
    console.error("Error fetching page:", error);
    return false;
  }
  return false;
};
getCourseUnits = async () => {
  try {
    const response = await fetch(variables.API_URL + 'courses/GetShowCourseUnits/' + getCookie("ShowingCourseId"));
    const data = await response.json();
    console.log(data);
    let Units = data;

    // Process each unit to fetch pages and tests
    await Promise.all(Units.map(async (unit) => {
      try {
        const pagesResponse = await fetch(variables.API_URL + 'unit/GetAllPagesOfUnit/' + unit.id);
        const pagesData = await pagesResponse.json();
        console.log(pagesData);
        const pages = pagesData || [];
        
        // Process each page to check completion
        const compPages = await Promise.all(pages.map(async (page) => {
          const isComp = await this.checkOnCompletePage(page.id);
          return { pId: page.id, isComp };
        }));

        // Update state for completed pages
        unit.pages = pages;
        this.setState(prevState => ({
          isCompletedPages: [...prevState.isCompletedPages, ...compPages],
          courseUnits: Units
        }));
      } catch (error) {
        console.error("Error fetching unit pages:", error);
      }

      try {
        const testsResponse = await fetch(variables.API_URL + 'unit/GetAllTestsOfUnit/' + unit.id);
        const testsData = await testsResponse.json();
        console.log(testsData);
        const tests = testsData || [];
        
        // Process each test to check completion
        const compTests = await Promise.all(tests.map(async (test) => {
          const isComp = await this.checkOnCompleteTest(test.id);
          return { tId: test.id, isComp };
        }));

        // Update state for completed tests
        unit.tests = tests;
        this.setState(prevState => ({
          isCompletedTests: [...prevState.isCompletedTests, ...compTests],
          courseUnits: Units
        }));
      } catch (error) {
        console.error("Error fetching unit tests:", error);
      }
    }));
  } catch (error) {
    console.error("Error fetching course units:", error);
  }
};


toShowPage=async(id)=>{
  setCookie("ShowingPage", id);
  window.location.href = variables.PAGE_URL+"show-page";
}
toEditPage=async(id)=>{
  setCookie("EditPageId", id);
  window.location.href = variables.PAGE_URL+"edit-page";
}
toEditTest=async(id)=>{
  setCookie("EditTestId", id);
  window.location.href = variables.PAGE_URL+"edit-test";
}
toShowTest=async(id)=>{
  setCookie("ShowingTest", id);
  window.location.href = variables.PAGE_URL+"show-test";
}
createCourse=async()=>{
  setCookie("EditCourseId", this.state.courseId);
  window.location.href=variables.PAGE_URL+"edit-course"
}

toNotLerner=async()=>{
  document.getElementById("exForm").style.display = "block";
}



studyCourse=async()=>{
  try {
    const response = await fetch(variables.API_URL + 'courses/startStudyCourse/'+getCookie("ShowingCourseId") +
    "/" +
    getCookie("UserSecretKey"), {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json; odata=verbose'
        },
        body: JSON.stringify({
          
        }), 
        
    })
    .then(response=>response.json())
    .then(data=>{
      if (data=="Bad Request"){
        console.error(data);
      }
      else{
        window.location.reload();
      }
    })
} catch (error) {
    console.error("Помилка:", error);
}
}

checkOnCompletePage=async(id)=>{
  try {
    const response = await fetch(
      variables.API_URL +
        'page/checkCompletePage/' +
        id +
        "/" +
        getCookie("UserSecretKey")
    );
    const data = await response.json();
    this.setState({isCompletedPage: data});
    return data;
  } catch (error) {
    console.error("Error fetching page:", error);
    return false;
  }
  return false;
}

checkOnCompleteTest=async(id)=>{
  try {
    const response = await fetch(
      variables.API_URL +
        'test/checkCompleteTest/' +
        id +
        "/" +
        getCookie("UserSecretKey")
    );
    const data = await response.json();
    this.setState({isCompletedTest: data});
    return data;
  } catch (error) {
    console.error("Error fetching page:", error);
    return false;
  }
  return false;
}


  render() {
    
    return (
    <>
     <Layout/>
          <img src={RightBack} alt="constrRight" className="right_back_constr"/>
          <div className="background">
    <div className="MainCoursePage">
<div className="left_site">
    <h2>{this.state.courseName}</h2>
    <p>Творець: {this.state.courseCreatorName}</p>
    {this.state.isCourseCreator ? (
            <>
              <br />
              <NavLink className="orangeBtn" onClick={this.createCourse}>
                Редагувати
              </NavLink>
              <br />
              <br />
            </>
          ) : !this.state.isCourseLerner ? (
            <>
              <br />
              <NavLink className="whiteBtn" onClick={this.studyCourse}>
                Вивчати!
              </NavLink>
              <br />
              <br />
            </>
          ):(
            <>
             <br />
              <br />
            </>
          )}
   </div>
    </div>
    <div className="course_view_site info_view info_course">
            <h1>Загальна інформація про курс</h1>
            <p>Предметна область знань: {this.state.courseKnowlage}</p>
            <p>Сфера навчання: {this.state.courseEducation}</p>
            <p>Тема: {this.state.courseTheme}</p>
            <p>Опис: {this.state.courseDesc}</p>
            {/*<p>Кількість людей, які проходять цей курс: {this.state.countLearningUsers}</p>
            <p>Кількість людей, які пройшли цей курс: {this.state.countCompletedUsers}</p>*/}

            </div>
            <br/>
            <div className="course_view_site info_view">
              <div className="exForm" id="exForm">
              <img src={warning} alt="warning" className="" />
                <p>Для доступу до матеріалів курсу необхідно почати його вивчення!</p>
              </div>
            <h1>Матеріали курсу</h1>
            {this.state.courseUnits != null && this.state.courseUnits.map((unit) => 
    
    <div className="unitView">
      <div className="unitNameBlock">
      <NavLink id="btnGeneral" className="showBtn btn" onClick={()=>this.show(`unit_${unit.id}`)}>
        <img src={Arrow} alt="arrow" id={`arrowBtn_unit_${unit.id}`} className="arrowBtn rotated" />
        {unit.unitName}</NavLink>
      </div>
      <div className="dropDownForm"  style={{ display: "none"}} id={`unit_${unit.id}`}>
      <div>
        <h4>Текстові матеріали</h4>
      {unit.pages != null && unit.pages.map(page=>
      
      <>
      <NavLink
        className="materials"
        onClick={
          this.state.isCourseCreator
            ? () => this.toEditPage(page.id)
            : this.state.isCourseLerner
            ? () => this.toShowPage(page.id)
            : () => this.toNotLerner()
        }
      >
        {page.pageName}
      </NavLink>
      {this.state.isCompletedPages.find((p) => p.pId === page.id)?.isComp === true ? (
        <span style={{ color: "green" }}>Виконано</span>
      ) : (
        <></>
      )}
      <br />
    </>
        )}
      
          
          <h4>Тести</h4>
          {unit.tests != null && unit.tests.map(test=>
        <>
        
        <NavLink  className="materials"onClick={
          this.state.isCourseCreator ? (
          ()=>this.toEditTest(test.id)
          ) : this.state.isCourseLerner ? (
            ()=>this.toShowTest(test.id)
            ) : (
              ()=>this.toNotLerner())
        }>{test.testName}</NavLink>
        { this.state.isCompletedTests.find((t)=>t.tId == test.id)?.isComp === true ? (
          <>
  <span style={{color: "green"}}>Виконано</span>
  </>
) : (<></>)
}

        <br/>
        </>
          )}
          </div>
      </div>
      </div>
    )}

            </div>
    </div>

   
    </>
    );
  }
}

export default MainCoursePage;

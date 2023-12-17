import React from "react";
import template from "./ShowingCourses.jsx";
import { variables } from "./../Variables.js";
import Layout from "../Layout/Layout.js";
import { setCookie, deleteCookie, getCookie } from "./../CookieHandler.js";
import RightBack from "../images/Сonstructor/constrRight.png";

class ShowingCourses extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      courses: [],
      userId: "",
      Knowledge: "",
      Education: "",
      Theme: "",
      Knowledges:[],
      Educations:[],
      Themes:[]
    };
  }

  componentDidMount = () => {
    this.getUserId();
    this.getAllCourses();
    this.getCategories();
  };

  handleInputChange = async(e) => {
    const { name, value } = e.target;
    this.setState({
        [name]: value
    });
    if(name=="Theme"){
      if(((this.state.Education == "") && (this.state.Knowledge == ""))){

        let educations = [];
        try{
  
  
          await fetch(variables.API_URL+'category/parent-categories/'+value)
        .then(response=>response.json())
        .then(data=>{
          if(data.title!="Bad Request"){
            const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
            this.setState({Educations:sorted});
            educations = data;
          }
        });
      }
      catch(error){
        console.error("Помилка:", error);
      }
      let knowledges = [];
      educations.map(async education=>{
        try{
  
  
          await fetch(variables.API_URL+'category/parent-categories/'+education.categoryName)
        .then(response=>response.json())
        .then(data=>{
          if(data.title!="Bad Request"){
            data.map(async knowledge=>{
              knowledges.push(knowledge)
            })
          }
        });
      }
      catch(error){
        console.error("Помилка:", error);
      }
      })
      const sorted = [...knowledges].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
            this.setState({Knowledges:sorted});
      }
      else if((this.state.Education != "") && (this.state.Knowledge == "")){
        this.state.Educations.map(async education=>{
          try{
    
    
            await fetch(variables.API_URL+'category/parent-categories/'+education.categoryName)
          .then(response=>response.json())
          .then(data=>{
            if(data.title!="Bad Request"){
              const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
              this.setState({Knowledges:sorted});
            }
          });
        }
        catch(error){
          console.error("Помилка:", error);
        }
      })
      }
      else if((this.state.Education == "") && (this.state.Knowledge != "")){
        this.state.Knowledges.map(async knowledge=>{
          try{
    
    
            await fetch(variables.API_URL+'category/child-categories/'+knowledge.categoryName)
          .then(response=>response.json())
          .then(data=>{
            if(data.title!="Bad Request"){
              const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
              this.setState({Educations:sorted});
            }
          });
        }
        catch(error){
          console.error("Помилка:", error);
        }
      })
      }
     
    }
    else if(name=="Education"){
      if(((this.state.Theme == "") && (this.state.Knowledge == ""))){
       
      try{


        await fetch(variables.API_URL+'category/parent-categories/'+value)
      .then(response=>response.json())
      .then(data=>{
        if(data.title!="Bad Request"){
          const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
          this.setState({Knowledges:sorted});
        }
      });
    }
    catch(error){
      console.error("Помилка:", error);
    }
    try{


      await fetch(variables.API_URL+'category/child-categories/'+value)
    .then(response=>response.json())
    .then(data=>{
      if(data.title!="Bad Request"){
        const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
        this.setState({Themes:sorted});
      }
    });
  }
  catch(error){
    console.error("Помилка:", error);
  }
  }
  else if((this.state.Theme != "") && (this.state.Knowledge == "")){
   
      try{


        await fetch(variables.API_URL+'category/parent-categories/'+value)
      .then(response=>response.json())
      .then(data=>{
        if(data.title!="Bad Request"){
          const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
          this.setState({Knowledges:sorted});
        }
      });
    }
    catch(error){
      console.error("Помилка:", error);
    }
  }
   
    else if((this.state.Theme == "") && (this.state.Knowledge != "")){
      try{


        await fetch(variables.API_URL+'category/child-categories/'+value)
      .then(response=>response.json())
      .then(data=>{
        if(data.title!="Bad Request"){
          const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
          this.setState({Themes:sorted});
        }
      });
    }
    catch(error){
      console.error("Помилка:", error);
    }
      }
      else{
        this.setState({Theme:""});
        try{


          await fetch(variables.API_URL+'category/child-categories/'+value)
        .then(response=>response.json())
        .then(data=>{
          if(data.title!="Bad Request"){
            const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
            this.setState({Themes:sorted});
          }
        });
      }
      catch(error){
        console.error("Помилка:", error);
      }
      }
    }
    else if(name == "Knowledge"){
      if(((this.state.Theme == "") && (this.state.Education == "")) || ((this.state.Theme != "") && (this.state.Education != ""))){
        if((this.state.Theme != "") && (this.state.Education != "") && this.state.Knowledge == ""){
          return;
        }
        if((this.state.Theme != "") && (this.state.Education != "")){
          this.setState({Education:"", Theme:""})
        }
      
        let educations = [];
        try{
  
  
          await fetch(variables.API_URL+'category/child-categories/'+value)
        .then(response=>response.json())
        .then(data=>{
          if(data.title!="Bad Request"){
            const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
            this.setState({Educations:sorted});
            educations = data
            // `
          }
        });
      }
      catch(error){
        console.error("Помилка:", error);
      }
  
      let themes = [];
      educations.map(async education=>{
        try{
  
  
          await fetch(variables.API_URL+'category/child-categories/'+education.categoryName)
        .then(response=>response.json())
        .then(data=>{
          if(data.title!="Bad Request"){
            data.map(async knowledge=>{
              themes.push(knowledge)
            })
          }
        });
      }
      catch(error){
        console.error("Помилка:", error);
      }
      const sorted = [...themes].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
      this.setState({Themes:sorted});
      })
    }
    else if((this.state.Theme != "") && (this.state.Education == "")){
        try{
  
  
          await fetch(variables.API_URL+'category/child-categories/'+value)
        .then(response=>response.json())
        .then(data=>{
          if(data.title!="Bad Request"){
            const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
            this.setState({Education:sorted});
          }
        });
      }
      catch(error){
        console.error("Помилка:", error);
      }
    }
      
      else if((this.state.Theme == "") && (this.state.Education != "")){
        this.state.Educations.map(async education=>{
          try{
    
    
            await fetch(variables.API_URL+'category/child-categories/'+education.categoryName)
          .then(response=>response.json())
          .then(data=>{
            if(data.title!="Bad Request"){
              const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
              this.setState({Themes:sorted});
            }
          });
        }
        catch(error){
          console.error("Помилка:", error);
        }
      })
        }
      } 
  
}


Search=async()=>{

  var knowledge = this.state.Knowledge;
  var education = this.state.Education;
  var theme = this.state.Theme;


 if(this.state.Education == ""){
  education = "undefined";
 }
 if(this.state.Knowledge == ""){
  knowledge = "undefined";
 }
 if(this.state.Theme == ""){
  theme = "undefined";
 }
  try{


    await fetch(variables.API_URL+'courses/getByCategories/'+knowledge+"/"+education+"/"+theme)
  .then(response=>response.json())
  .then(data=>{
    if(data.title!="Bad Request"){
      
      this.setState({courses:data});
    }
  });
}
catch(error){
  console.error("Помилка:", error);
}
}

  getCategories= async ()=>{
    try{


      await fetch(variables.API_URL+'category/without-parents')
    .then(response=>response.json())
    .then(data=>{
      if(data.title!="Bad Request"){
        const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
        this.setState({Knowledges:sorted});
      }
    });
  }
  catch(error){
    console.error("Помилка:", error);
  }

  try{


    await fetch(variables.API_URL+'category/with-children&parents')
  .then(response=>response.json())
  .then(data=>{
    if(data.title!="Bad Request"){
      const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
      this.setState({Educations:sorted});
    }
  });
}
catch(error){
  console.error("Помилка:", error);
}

try{


  await fetch(variables.API_URL+'category/without-children')
.then(response=>response.json())
.then(data=>{
  if(data.title!="Bad Request"){
    const sorted = [...data].sort((a, b) => a.categoryName.localeCompare(b.categoryName));
    this.setState({Themes:sorted});
  }
});
}
catch(error){
console.error("Помилка:", error);
}
  }

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
        <img src={RightBack} alt="constrRight" className="right_back_constr" />
        <div className="filtrCategories">
          <h3>Пошук курсів за категорією</h3>
        <div className="inputArea">
       <section>
        <p className="textAreaOfKnowledge textOption">Предметна область знань курсу</p>
        </section>
        <section>
        <select id="Knowledge" className="textInput" name="Knowledge" onChange={this.handleInputChange}>
          <option value=""></option>
        {this.state.Knowledges.map(knowledge=>
        <>
            <option value={knowledge.categoryName} selected={knowledge.categoryName === this.state.Knowledge ? true : false}>{knowledge.categoryName}</option>
            </>
          )}
        </select>
        </section>
        </div>

        <div className="inputArea">
        <section>
        <p className="textFieldOfEducation textOption">Сфера навчання курсу</p>
        </section>
        <section>
        <select id="Education" className="textInput" name="Education" onChange={this.handleInputChange}>
        <option value=""></option>
        {this.state.Educations.map(education=>
        <>
            <option value={education.categoryName} selected={education.categoryName === this.state.Education ? true : false}>{education.categoryName}</option>
          </>  
          )}
        </select>
        </section>
        </div>
        <div className="inputArea">
        <section>
        <p className="textTheme textOption">Тема</p>
        </section>
        <section>
        <select id="Theme" className="textInput" name="Theme" onChange={this.handleInputChange}>
        <option value=""></option>
          {this.state.Themes.map(theme=>
            <>
            <option value={theme.categoryName} selected={theme.categoryName === this.state.Theme ? true : false}>{theme.categoryName}</option>
            </>
          )}

        </select>

        </section>    
        </div>
        <button className="blueBtn" onClick={this.Search}>Знайти</button>
        </div>

        <div className="coursesView">
        <h3>Список курсів</h3>
          {this.state.courses.map((course) => {
          
              if(course.creatorUserId != this.state.userId){
              return (
                <div className="courseCard" key={course.id} onClick={()=>{setCookie("ShowingCourseId", course.id); window.location.href=variables.PAGE_URL+"display-course"}}>
                  <h4>{course.courseName}</h4>
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

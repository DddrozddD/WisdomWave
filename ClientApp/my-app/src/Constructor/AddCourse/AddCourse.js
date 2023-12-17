import React    from "react";
import template from "./AddCourse.jsx";
import Layout from "../../Layout/Layout.js";
import {variables} from './../../Variables.js';
import RightBack from "../../images/Сonstructor/constrRight.png"
import LeftBack from "../../images/Сonstructor/constrLeft.png"
import Arrow from "../../images/arrow.png"
import { NavLink } from "react-router-dom";
import { setCookie, deleteCookie, getCookie } from './../../CookieHandler.js';

class AddCourse extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      FullName: "",
      Knowledge: "",
      Education: "",
      Theme: "",
      Annotation: "",
      Language: "",
      Knowledges:[],
      Educations:[],
      Themes:[]
    }
}
  componentDidMount=async()=>{
    document.getElementById("general").style.display = "block";
    document.getElementById("arrowBtn_general").classList.remove("rotated");
    document.getElementById("annotation").style.display = "block";
    document.getElementById("arrowBtn_annotation").classList.remove("rotated");
    document.getElementById("view").style.display = "block" 
    document.getElementById("arrowBtn_view").classList.remove("rotated");

    this.getCategories();
  }


  

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
  createCourse = async () => {
    const {FullName, Knowledge, Education, Theme, Annotation, Language } = this.state;
      try {
          const response = await fetch(variables.API_URL + 'courses/'+ getCookie("UserSecretKey"), {

              method: 'POST',
              headers: {
                  'Accept': 'application/json',
                  'Content-Type': 'application/json; odata=verbose'
              },
              body: JSON.stringify({
                  "CourseName":FullName,
                  "Description": Annotation,
                  "Knowledge": Knowledge,
                  "Education": Education,
                  "Theme": Theme,
                  "Language": Language
              }), 
              
          })
          if(response.ok){
            window.location.href = variables.PAGE_URL+ "user-courses"
          }

      } catch (error) {
          console.error("Помилка:", error);
      }
  }

  
  render() {
    return (
      <>
       
      <Layout/>
      <div className="background">
      <img src={LeftBack} alt="constrLeft" className="left_back_constr"/>
      <img src={RightBack} alt="constrRight" className="right_back_constr"/>
      <div className="constrView">
      <h1>Додати новий курс</h1>
      <NavLink id="btnGeneral" className="showBtn btn" onClick={()=>this.show("general")}>
        <img src={Arrow} alt="arrow" id="arrowBtn_general" className="arrowBtn " />
        Загальне</NavLink>
      <div className="general dropDownForm" id="general">
      
      <div className="inputArea">
        <section>
        <p className="textFullNameCourse textOption">Повна назва курсу<span className="redStar">*</span></p>
        </section>
        <section>
        <input className="inputFullName textInput " name="FullName" type="text" onChange={this.handleInputChange}></input> 
        </section>
       </div>

       <div className="inputArea">
       <section>
        <p className="textAreaOfKnowledge textOption">Предметна область знань курсу<span className="redStar">*</span></p>
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
        <p className="textFieldOfEducation textOption">Сфера навчання курсу<span className="redStar">*</span></p>
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
        <p className="textTheme textOption">Тема<span className="redStar">*</span></p>
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
      <div className="inputArea">
        <br/>
       
      </div>
      </div>
      <br/>
      <NavLink id="btn" className="showBtn btn" onClick={()=>this.show("annotation")}>
        <img src={Arrow} alt="arrow" id="arrowBtn_annotation" className="arrowBtn" />
        Опис</NavLink>
      <div className="annotation dropDownForm" id="annotation">

        <p className="textOption inputTextarea">Анотація курсу</p>
      
      
        <textarea className="inputAnnotation" name="Annotation" onChange={this.handleInputChange}/> <br/>
     
      </div>
      <br/>
      <NavLink id="btn" className="showBtn btn" onClick={()=>this.show("view")}>
        <img src={Arrow} alt="arrow" id="arrowBtn_view" className="arrowBtn" />
        Вигляд</NavLink>
      <div className="view dropDownForm" id="view">
      <div className="inputArea">
        <section>
        <p className="textView textOption">Мова викладання</p>
        </section>
        <section>
        <select id="Language" className="inputLanguage textInput" name="Language" onChange={this.handleInputChange}>
          <option>Українська</option>
          <option>Англійська</option>
          <option>Німецька</option>
          <option>Французька</option>
          <option>Італійська</option>
          <option>Іспанська</option>
        </select>
        </section>
      </div>
      </div>
      <br/>
      <div className="resultBtns">
      <NavLink className="saveBtn btn resultBtn" onClick={this.createCourse}>Зберегти зміни</NavLink>
      <NavLink className="cancelBtn btn resultBtn" to="/user-courses">Скасувати</NavLink>
      </div>
      <p className="downP">Обов’язкові поля  форми помічені символом<span className="redStar">*</span></p>
      </div>
      </div>
      </>
    );
  }
}

export default AddCourse;

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
      Language: ""
    }
}
  componentDidMount=async()=>{
    document.getElementById("general").style.display = "none";
    document.getElementById("arrowBtn_general").classList.add("rotated");
    document.getElementById("annotation").style.display = "none";
    document.getElementById("arrowBtn_annotation").classList.add("rotated");
    document.getElementById("view").style.display = "none" 
    document.getElementById("arrowBtn_view").classList.add("rotated");
     
    this.setState({Knowledge: document.getElementById("Knowledge").value, 
    Education: document.getElementById("Education").value, 
    Theme: document.getElementById("Theme").value,
    Language: document.getElementById("Language").value  })
  }

  handleInputChange = (e) => {
    const { name, value } = e.target;
    this.setState({
        [name]: value
    });
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

  createCourse = async () => {
    const {FullName, Knowledge, Education, Theme, Annotation, Language } = this.state;
      try {
          const response = await fetch(variables.API_URL + 'courses/'+ getCookie("YourSecretKeyHere"), {

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

          .then(response=>response.json())
          .then(data=>{
            if (data!="Bad Request"){
              setCookie("CreatingCourseId", data, {secure: true, 'max-age': 3600});
              window.location.assign('http://localhost:3000/marge-units');
            }
            else{
              console.error("Увійти не вдалась");
            }
          })

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

          <option>Веб розробка</option>

        </select>
        </section>
        </div>

        <div className="inputArea">
        <section>
        <p className="textFieldOfEducation textOption">Сфера навчання курсу<span className="redStar">*</span></p>
        </section>
        <section>
        <select id="Education" className="textInput" name="Education" onChange={this.handleInputChange}>

        <option>JavaScript</option>

        </select>
        </section>
        </div>
        <div className="inputArea">
        <section>
        <p className="textTheme textOption">Тема<span className="redStar">*</span></p>
        </section>
        <section>
        <select id="Theme" className="textInput" name="Theme" onChange={this.handleInputChange}>

        <option>ReactJS</option>

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
        <p className="textView textOption">Мова</p>
        </section>
        <section>
        <select id="Language" className="inputLanguage textInput" name="Language" onChange={this.handleInputChange}>
          <option>Українська</option>
        </select>
        </section>
      </div>
      </div>
      <br/>
      <div className="resultBtns">
      <NavLink className="saveBtn btn resultBtn" onClick={this.createCourse}>Зберегти зміни</NavLink>
      <NavLink className="cancelBtn btn resultBtn">Скасувати</NavLink>
      </div>
      <p className="downP">Обов’язкові поля  форми помічені символом<span className="redStar">*</span></p>
      </div>
      </div>
      </>
    );
  }
}

export default AddCourse;

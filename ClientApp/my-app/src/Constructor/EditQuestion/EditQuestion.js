import React    from "react";
import template from "./EditQuestion.jsx";
import Layout from "../../Layout/Layout.js";
import {variables} from './../../Variables.js';
import RightBack from "../../images/Сonstructor/constrRight.png"
import LeftBack from "../../images/Сonstructor/constrLeft.png"
import Arrow from "../../images/arrow.png"
import { NavLink } from "react-router-dom";
import { setCookie, deleteCookie, getCookie } from './../../CookieHandler.js';


class EditQuestion extends React.Component {

 constructor(props) {
      super(props);

      this.state = {
        QuestionName: "",
        QuestionText: "",
        QuestionType: "",
        QuestionPoints: 0,
        chosenType: "OneAnswer",
        answers: [],
        InitialQuestionType:"",
        correctAnswer: 0
        
      }
    }

    
    componentDidMount=async()=>{
  
      document.getElementById("infoQ").style.display = "block";
      
      try{
      document.getElementById("TypeAnswer").style.display = "none";
      }
      catch(e){

      }
      this.getQuestion(getCookie("EditQuestionId"))

  
    }
    handleInputChange = (e) => {
      const { name, value } = e.target;
      this.setState({
          [name]: value
      });
  }
  show = (id) =>{
    if(document.getElementById(id).style.display === "none"){
      document.getElementById(id).style.display = "block";
      document.getElementById("arrowBtn_"+id).classList.remove("rotated");
    }
    else{
      document.getElementById(id).style.display = "none";
      document.getElementById("arrowBtn_"+id).classList.add("rotated");
    }
    
  }

  getQuestion=async(id)=>{
    try {
      const response = await fetch(variables.API_URL + 'question/'+id);
      const data = await response.json();
      console.log(data);
      this.setState({  QuestionName: data.questionName, QuestionText: data.questionText,QuestionType:data.questionType, QuestionPoints: data.countOfPoints,
      InitialQuestionType: data.questionType});
      document.getElementById("InputQuestionPoints").defaultValue = data.countOfPoints;
      if(data.questionType === "OneAnswer"){
        document.getElementById("typeQ1").defaultChecked = true;
      }
      else if(data.questionType === "TrueFalseAnswer"){
        document.getElementById("typeQ2").defaultChecked = true;
      }
      else if(data.questionType === "ManyAnswers"){
        document.getElementById("typeQ3").defaultChecked = true;
      }
      
      
    } catch (error) {
      console.error("Error fetching page:", error);
    }

    try {
      const response = await fetch(variables.API_URL + 'question/GetQuestionAnswers/'+id);
      const data = await response.json();
      console.log(data);
      
      this.setState({  answers: data});
     
      
    } catch (error) {
      console.error("Error fetching page:", error);
    }
    
  }


  seveOneAnswer=async()=>{
      
  }

  updateType=async(option)=>{
    if(option === "option1"){
      this.setState({QuestionType: "OneAnswer"})
    }
    else if(option === "option2"){
      this.setState({QuestionType: "TrueFalseAnswer"})
    }
    else{
      this.setState({QuestionType: "ManyAnswers"})
    }
  }

  saveQuestion=async()=>{
    try {
      const response = await fetch(variables.API_URL + 'question/'+getCookie("EditQuestionId") , {
          method: 'PUT',
          headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json; odata=verbose'
          },
          body: JSON.stringify({
            "Id": getCookie("EditQuestionId"),
            "QuestionName": this.state.QuestionName,
            "QuestionText": this.state.QuestionText,
            "CountOfPoints": this.state.QuestionPoints,
            "QuestionType": this.state.QuestionType,
            "testId": getCookie("EditTestId")
          }), 
          
      })
      if(response.ok){

        let Method = "";

        if(this.state.QuestionType === this.state.InitialQuestionType){
          Method = "PUT";
        }
        else{
          Method = "POST"
        }
          if(this.state.QuestionType === "OneAnswer"){
            const correctAnsw = document.getElementById("IsCorrectAnswer").value;
          let countOkRes = 0;
          for(let i = 1; i <= 4; i++){
            let isCorrect = false;
            if(i.toString()===correctAnsw){
               isCorrect = true;
            }
            const answerText = document.getElementById("inputOneAnswer"+i.toString()).value;
            let metogLink = "";
            if(Method === "PUT"){
              metogLink = variables.API_URL + 'answers/'+this.state.answers[i-1].id;
            }
            else{
              metogLink = variables.API_URL + 'answers';
            }
            const response1 = await fetch(metogLink , {
              method: Method,
              headers: {
                  'Accept': 'application/json',
                  'Content-Type': 'application/json; odata=verbose'
              },
              body: JSON.stringify({
                "AnswerText": answerText,
                "IsCorrect": isCorrect,
                "questionId": getCookie("EditQuestionId")
              })
              
          })
          if(response1.ok){
            countOkRes++;
          }
        }
        if(countOkRes===4){
          window.location.href = "/edit-test";
        }
      }
          else if(this.state.QuestionType === "ManyAnswers"){
            let countOkRes = 0;
            for(let i = 1; i <= 4; i++){
              const isCorrectAnsw = document.getElementById("IsCorrectManyAnswers"+i.toString());

            let isCorrect = false;
            if(isCorrectAnsw.value ==="Правильна"){
               isCorrect = true;
            }
            const answerText = document.getElementById("inputManyAnswers"+i).value;
            let metogLink = "";
            if(Method === "PUT"){
              metogLink = variables.API_URL + 'answers/'+this.state.answers[i-1].id;
            }
            else{
              metogLink = variables.API_URL + 'answers';
            }
            const response1 = await fetch(metogLink , {
              method: Method,
              headers: {
                  'Accept': 'application/json',
                  'Content-Type': 'application/json; odata=verbose'
              },
              body: JSON.stringify({
                "AnswerText": answerText,
                "IsCorrect": isCorrect,
                "questionId": getCookie("EditQuestionId")
              })
              
          })
          if(response1.ok){
            countOkRes++;
          }
        }
        if(countOkRes===4){
          window.location.href = "/edit-test";
        }
      }
      else if(this.state.QuestionType === "TrueFalseAnswer"){
        let isCorrect = false
          if(document.getElementById("IsCorrectAnswer").value === "Правильно"){
            isCorrect = true;
          }
          let metogLink = "";
          if(Method === "PUT"){
            metogLink = variables.API_URL + 'answers/'+this.state.answers[0].id;
          }
          else{
            metogLink = variables.API_URL + 'answers';
          }
          const response1 = await fetch(metogLink , {
            method: Method,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; odata=verbose'
            },
            body: JSON.stringify({
              "AnswerText": "",
              "IsCorrect": isCorrect,
              "questionId": getCookie("EditQuestionId")
            })
            
        })
        if(response1.ok){
          window.location.href = "/edit-test";
        }
      }
    
        }
  } catch (error) {
      console.error("Помилка:", error);
  }
  }


  getTypeView=()=>{

    if(this.state.QuestionType === "OneAnswer")
    {
      if (this.state.InitialQuestionType === "OneAnswer"){
        const returnAnswers = [];
        if(this.state.answers.length!=0){
        
    for (let i = 0; i < 4; i++) {
      if(this.state.answers[i].isCorrect === true){
         this.setState({ correctAnswer: i+1});
      }
      returnAnswers.push(
      
        <div className="inputArea">
             <section>
             <p className="textAnswer1 textOption">Варіант відповіді 1<span className="redStar">*</span></p>
             </section>
     
             <textarea className="inputAnnotation inputAnswer" id={`inputOneAnswer`+(i+1)} name={`inputOneAnswer`+(i+1)} 
             defaultValue={this.state.answers[i].answerText}></textarea> <br/>
     
             <br/>
            </div>           
      );
    }
    }
        return (
          <>
           <NavLink id="btnTypeAnswer" className="showBtn btn" onClick={()=>this.show("TypeAnswer")}>
          <img src={Arrow} alt="arrow" id="arrowBtn_TypeAnswer" className="arrowBtn rotated" />
          Відповіді до питання Вибір однієї правильної відповіді з багатьох інших </NavLink>
        <div className="dropDownForm" id="TypeAnswer" style={{display:"none"}}>
         {returnAnswers}
        <div className="inputArea">
        <section>
        <p className="textOneAnswer4 textOption">Яка відповыдь є правильною<span className="redStar">*</span></p>
        </section>
        <section>
        <select className="inputIsCorrectOneAnswer textInput " name="IsCorrectAnswer" id="IsCorrectAnswer" type="text"  
        defaultValue={this.state.correctAnswer}   
        >
           <option>
           
          </option>

          <option value={1} selected={1 === this.state.correctAnswer ? true : false}>
            1
          </option>
          <option value={2} selected={2 === this.state.correctAnswer ? true : false}>
            2
          </option>
          <option value={3} selected={3 === this.state.correctAnswer ? true : false}>
            3
          </option>
          <option value={4} selected={4 === this.state.correctAnswer ? true : false}>
            4
          </option>
        </select> 
        </section>
        <br/>
       </div>
         </div>
        </>
        );
      }
     
      return (
        <>
         <NavLink id="btnTypeAnswer" className="showBtn btn" onClick={()=>this.show("TypeAnswer")}>
        <img src={Arrow} alt="arrow" id="arrowBtn_TypeAnswer" className="arrowBtn rotated" />
        Відповіді до питання Вибір однієї правильної відповіді з багатьох інших </NavLink>
      <div className="dropDownForm" id="TypeAnswer" style={{display:"none"}}>
      <div className="inputArea">
        <section>
        <p className="textAnswer1 textOption">Варіант відповіді 1<span className="redStar">*</span></p>
        </section>

        <textarea className="inputAnnotation inputAnswer1" id={`inputOneAnswer1`} name={`inputOneAnswer1`}  ></textarea> <br/>

        <br/>
       </div>
      

       <div className="inputArea">
        <section>
        <p className="textOneAnswer2 textOption">Варіант відповіді 2<span className="redStar">*</span></p>
        </section>

        <textarea className="inputAnnotation inputOneAnswer2" id={`inputOneAnswer2`} name={`inputOneAnswer2`}  ></textarea> <br/>

        <br/>
       </div>
      

       
       <div className="inputArea">
        <section>
        <p className="textOneAnswer3 textOption">Варіант відповіді 3<span className="redStar">*</span></p>
        </section>

        <textarea className="inputAnnotation inputOneAnswer3" id={`inputOneAnswer3`} name={`inputOneAnswer3`}  ></textarea> <br/>
       </div>
       <div className="inputArea">
        
        <br/>
        <br/>
       </div>
       <div className="inputArea">
        <section>
        <p className="textOneAnswer2 textOption">Варіант відповіді 4<span className="redStar">*</span></p>
        </section>

        <textarea className="inputAnnotation inputOneAnswer4" id={`inputOneAnswer4`} name={`inputOneAnswer4`}  ></textarea> <br/>

        <br/>
       </div>
       <div className="inputArea">
        <section>
        <p className="textOneAnswer4 textOption">Яка відповыдь є правильною<span className="redStar">*</span></p>
        </section>
        <section>
        <select className="inputIsCorrectOneAnswer textInput " name="IsCorrectAnswer" id="IsCorrectAnswer" type="text"     
        >
           <option>
           
          </option>

          <option>
            1
          </option>
          <option>
            2
          </option>
          <option>
            3
          </option>
          <option>
            4
          </option>
        </select> 
        </section>
        <br/>
       </div>
      </div>
        </>
      )
      
    }
    else if(this.state.QuestionType === "TrueFalseAnswer"){
    if (this.state.InitialQuestionType === "TrueFalseAnswer" && this.state.answers.length!=0){
     

      return (
        
        <>
          <NavLink id="btnTypeAnswer" className="showBtn btn" onClick={()=>this.show("TypeAnswer")}>
         <img src={Arrow} alt="arrow" id="arrowBtn_TypeAnswer" className="arrowBtn rotated" />
        Питання типу Правильно/Неправильно</NavLink>
      <div className="dropDownForm" id="TypeAnswer" style={{display:"none"}}>
      <div className="inputArea">
        <section>
        <p className="textOneAnswer textOption">Правильна відповідь<span className="redStar">*</span></p>
        </section>
        <section>
        <select className="inputIsCorrectOneAnswer textInput " name="IsCorrectAnswer" id="IsCorrectAnswer" type="text"     
        >
           <option selected={this.state.answers[0].isCorrect ? true : false}>
           Правильно
          </option>

          <option selected={!this.state.answers[0].isCorrect ? true : false}>
           Неправильно
          </option>
        </select> 
        </section>
        <br/>
       </div>
      </div>
      </>
      );
    }
    
    
      return (
        <>
         <NavLink id="btnTypeAnswer" className="showBtn btn" onClick={()=>this.show("TypeAnswer")}>
         <img src={Arrow} alt="arrow" id="arrowBtn_TypeAnswer" className="arrowBtn rotated" />
        Питання типу Правильно/Неправильно</NavLink>
      <div className="dropDownForm" id="TypeAnswer" style={{display:"none"}}>
      <div className="inputArea">
        <section>
        <p className="textOneAnswer4 textOption">Правильна відповідь<span className="redStar">*</span></p>
        </section>
        <section>
        <select className="inputIsCorrectOneAnswer textInput " name="IsCorrectAnswer" id="IsCorrectAnswer" type="text"     
        >
           <option>
           Правильно
          </option>

          <option>
           Неправильно
          </option>
        </select> 
        </section>
        <br/>
       </div>
      </div>
        </>
      )
    }
  
    else if(this.state.QuestionType === "ManyAnswers"){
      if (this.state.InitialQuestionType === "ManyAnswers"){
        const returnAnswers = [];
        if(this.state.answers.length!=0){
        
    for (let i = 0; i < 4; i++) {
      
      returnAnswers.push(
      <>
        <div className="inputArea">
        <section>
          <p className="textManyAnswers1 textOption">Варіант відповіді {i+1}<span className="redStar">*</span></p>
        </section>
        <textarea className="inputAnnotation inputManyAnswers" id={`inputManyAnswers`+(i+1)} name={`inputManyAnswers`+(i+1)} defaultValue={this.state.answers[i].answerText}></textarea> <br/>
        <br/>
      </div>
      <div className="inputArea">
        <section>
          <p className="textIsCorrectManyAnswers1 textOption">Правильна відповідь {i+1}<span className="redStar">*</span></p>
        </section>
        <section>
          <select className="inputIsCorrectManyAnswers1 textInput " name={`CorrectManyAnswers`+(i+1)} id={`IsCorrectManyAnswers`+(i+1)} type="text">
            <option></option>
            <option selected={this.state.answers[i].isCorrect ? true : false}>Правильна</option>
            <option selected={!this.state.answers[i].isCorrect ? true : false}>Неправильна</option>
          </select> 
        </section>
        <br/>
      </div>     
      </>
      );
    }
    }
        return (
          <>
           <NavLink id="btnTypeAnswer" className="showBtn btn" onClick={()=>this.show("TypeAnswer")}>
          <img src={Arrow} alt="arrow" id="arrowBtn_TypeAnswer" className="arrowBtn rotated" />
          Відповіді до питання Вибір однієї правильної відповіді з багатьох інших </NavLink>
        <div className="dropDownForm" id="TypeAnswer" style={{display:"none"}}>
         {returnAnswers}
       
         </div>
        </>
        );
      }
      return (
        <>
        <NavLink id="btnTypeAnswer" className="showBtn btn" onClick={()=>this.show("TypeAnswer")}>
  <img src={Arrow} alt="arrow" id="arrowBtn_TypeAnswer" className="arrowBtn rotated" />
  Питання типу Множинний вибір
</NavLink>
<div className="dropDownForm" id="TypeAnswer" style={{display:"none"}}>
  <div className="inputArea">
    <section>
      <p className="textManyAnswers1 textOption">Варіант відповіді 1<span className="redStar">*</span></p>
    </section>
    <textarea className="inputAnnotation inputManyAnswers1" id={`inputManyAnswers1`} name={`inputManyAnswers1`} ></textarea> <br/>
    <br/>
  </div>
  <div className="inputArea">
    <section>
      <p className="textIsCorrectManyAnswers1 textOption">Правильна відповідь 1<span className="redStar">*</span></p>
    </section>
    <section>
      <select className="inputIsCorrectManyAnswers1 textInput " name="CorrectManyAnswers1" id="IsCorrectManyAnswers1" type="text">
        <option></option>
        <option>Правильна</option>
        <option>Неправильна</option>
      </select> 
    </section>
    <br/>
  </div>

  <div className="inputArea">
    <section>
      <p className="textManyAnswers2 textOption">Варіант відповіді 2<span className="redStar">*</span></p>
    </section>
    <textarea className="inputAnnotation inputManyAnswers2" id={`inputManyAnswers2`} name={`inputManyAnswers2`} ></textarea> <br/>
    <br/>
  </div>
  
  <div className="inputArea">
    <section>
      <p className="textIsCorrectManyAnswers2 textOption">Правильна відповідь 2<span className="redStar">*</span></p>
    </section>
    <section>
      <select className="inputIsCorrectManyAnswers2 textInput " name="CorrectManyAnswers2" id="IsCorrectManyAnswers2" type="text">
        <option></option>
        <option>Правильна</option>
        <option>Неправильна</option>
      </select> 
    </section>
    <br/>
</div>

  <div className="inputArea">
    <section>
      <p className="textManyAnswers3 textOption">Варіант відповіді 3<span className="redStar">*</span></p>
    </section>
    <textarea className="inputAnnotation inputManyAnswers3" id={`inputManyAnswers3`} name={`inputManyAnswers3`} ></textarea> <br/>
  </div>

  <div className="inputArea">
    <section>
      <p className="textIsCorrectManyAnswers3 textOption">Правильна відповідь 3<span className="redStar">*</span></p>
    </section>
    <section>
      <select className="inputIsCorrectManyAnswers3 textInput " name="CorrectManyAnswers3" id="IsCorrectManyAnswers3" type="text">
        <option></option>
        <option>Правильна</option>
        <option>Неправильна</option>
      </select> 
    </section>
    <br/>
</div>

  <div className="inputArea">
    <section>
      <p className="textManyAnswers4 textOption">Варіант відповіді 4<span className="redStar">*</span></p>
    </section>
    <textarea className="inputAnnotation inputManyAnswers4" id={`inputManyAnswers4`} name={`inputManyAnswers4`} ></textarea> <br/>
    <br/>
  </div>
  <div className="inputArea">
    <section>
      <p className="textIsCorrectManyAnswers4 textOption">Правильна відповідь 4<span className="redStar">*</span></p>
    </section>
    <section>
      <select className="inputIsCorrectManyAnswers4 textInput " name="CorrectManyAnswers4" id="IsCorrectManyAnswers4" type="text">
        <option></option>
        <option>Правильна</option>
        <option>Неправильна</option>
      </select> 
    </section>
    <br/>
</div>
  
</div>

        </>
      )
    }
 
  }

  render() {
    return (
      <>
       <Layout/>
        <div className="background">
        <img src={LeftBack} alt="constrLeft" className="left_back_constr"/>
        <img src={RightBack} alt="constrRight" className="right_back_constr"/>
        
        <div className="questionView">
        <h1>Редагування питання</h1>
        <NavLink id="btnInfoQ" className="showBtn btn" onClick={()=>this.show("infoQ")}>
        <img src={Arrow} alt="arrow" id="arrowBtn_infoQ" className="arrowBtn " />
        Інформація про питання </NavLink>
      <div className="infoQ dropDownForm" id="infoQ">
      
      <div className="inputArea">
        <section>
        <p className="textQuestionName textOption">Назва питання<span className="redStar">*</span></p>
        </section>
        <section>
        <input className="inputQuestionName textInput " name="QuestionName" id="QuestionName" type="text" onChange={this.handleInputChange} defaultValue={this.state.QuestionName}></input> 
        </section>
        <br/>
       </div>    
      <div className="inputArea">
    <p className="textOption inputTextarea">Текст питання<span className="redStar">*</span></p>
    
     
    <textarea className="inputAnnotation ParagraphText" id={`QuestionText`} name={`QuestionText`}  
    onChange={this.handleInputChange} defaultValue={this.state.QuestionText}></textarea> <br/>

    
    </div>
    <div className="inputArea">
        <section>
        <p className="textQuestionPoints textOption">Бал за відповідь<span className="redStar">*</span></p>
        </section>
        <section>
        <select id="InputQuestionPoints" className="InputQuestionPoints textInput" name="QuestionPoints" onChange={this.handleInputChange} >
  {Array.from({ length: 20 }, (_, index) => (
    
    <option key={index} value={index+1} selected={index + 1 === this.state.QuestionPoints ? true : false}>{index+1}</option>
  ))}
</select>

        </section>
        <br/>
       </div>  
       <div className="inputArea">
        <section>
        <p className="textQuestionType textOption">Тип питання<span className="redStar">*</span></p>
        </section>
        <section>

        <input type="radio" name={`typeQ`} id={`typeQ1`} className="orangeRadio" onClick={()=>this.updateType("option1")} /> <span>Питання типу Вибір однієї правильної відповіді з багатьох </span>
              <br />
              <input type="radio" name={`typeQ`} id={`typeQ2`} className="orangeRadio" 
              onClick={()=>this.updateType("option2")}/> <span>Питання типу Правильно/Неправильно</span>
              <br />
              <input type="radio" name={`typeQ`} id={`typeQ3`} className="orangeRadio" 
                onClick={()=>this.updateType("option3")}/> <span>Питання типу Множинний вибір</span>
                <br/>
        </section>
        <br/>
       </div>  

        <br/>
        </div>

        {this.getTypeView()}
       

      <div className="resultBtns">
      <NavLink className="saveBtn btn resultBtn" onClick={this.saveQuestion}>Зберегти зміни</NavLink>
      <NavLink className="cancelBtn btn resultBtn">Скасувати</NavLink>
      </div>
      <p className="downP">Обов’язкові поля форми помічені символом<span className="redStar">*</span></p>
        </div>

        </div>
     </>
    );
  }
}

export default EditQuestion;

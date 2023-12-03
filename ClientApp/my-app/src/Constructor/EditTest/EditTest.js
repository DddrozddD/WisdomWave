  import React    from "react";
  import template from "./EditTest.jsx";
  import RightBack from "../../images/Сonstructor/constrRight.png"
  import LeftBack from "../../images/Сonstructor/constrLeft.png"
  import Layout from "../../Layout/Layout.js";
  import {variables} from './../../Variables.js';
  import ph_pencil from "../../images/Сonstructor/ph_pencil.png"
  import ic_for_add from "../../images/Сonstructor/ic_for_add.png"
  import { getCookie, setCookie } from "../../CookieHandler.js";
  import { BrowserRouter, Route, Routes, NavLink } from 'react-router-dom'; 
  import {usePagination} from "../../Pagination.js"
  import Arrow from "../../images/arrow.png"

  class EditTest extends React.Component {
    constructor(props) {
      super(props);

      this.state = {
        questions:[],
        selectedOptions: {},
        TestName:"",
        
      }
    }

  
    componentDidMount=async()=>{
      document.getElementById("general").style.display = "block";
      this.getTest();
      this.getQuestions();
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


    getTest=async()=>{
      try {
        const response = await fetch(variables.API_URL + 'test/'+getCookie("EditTestId"));
        const data = await response.json();
          this.setState({TestName: data.testName});

      } catch (error) {
        console.error("Error fetching course units:", error);
      }
    }


    getQuestions=async()=>{
      try {
        const response = await fetch(variables.API_URL + 'test/GetQuestions/'+getCookie("EditTestId"));
        const data = await response.json();
          this.setState({questions: data});

      } catch (error) {
        console.error("Error fetching course units:", error);
      }
    }

    returnAddBtn=async(id, option)=>{
      

      if(option == "option1"){
        
        document.getElementById("addBtnForQuestion_"+id).value="1"
      }
      else if(option == "option2"){

        
        document.getElementById("addBtnForQuestion_"+id).value="2"
      }
      else if(option == "option3"){

        document.getElementById("addBtnForQuestion_"+id).value="3"
        

      }
      else{

        document.getElementById("addBtnForQuestion_"+id).value="Додати"
      }
    }

    addQuestion=async()=>{
      try {
        const response = await fetch(variables.API_URL + 'question/', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; odata=verbose'
            },
            body: JSON.stringify({
              "questionName": "Питання тесту",
              "questionType": "",
              "questionText": "",
              "testId": getCookie("EditTestId")
            }), 
            
        })
        .then(response=>response.json())
        .then(data=>{
          if (data!="Bad Request"){
            this.getQuestions();
          }
          else{
            console.error("");
          }
        })
    } catch (error) {
        console.error("Помилка:", error);
    }
    
    }
    
    editQuestion=async(id)=>{
      setCookie("EditQuestionId", id);
      window.location.href = "/edit-question";
    }

    saveName=async()=>{
      try {
        const response = await fetch(variables.API_URL + 'test/'+getCookie("EditPageId"), {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; odata=verbose'
            },
            body: JSON.stringify({
              "TestName": this.state.TestName,
              "TestDescription": "",
              "unitId": 0
            }), 
            
        })
        .then(response=>response.json())
        .then(data=>{
          if (data!="Bad Request"){
            this.getPage(getCookie("EditPageId"));
          }
          else{
            console.error("");
          }
        })
    } catch (error) {
        console.error("Помилка:", error);
    }
    
    }

    render() {
      
      
      
      


      const PaginationWrapper = ({ questions }) => {
        
        
        const {
          page,
          totalPages,
          firstContentIndex,
          lastContentIndex,
          nextPage,
          prevPage,
          setPage,
          gaps,
        } = usePagination({ contentPerPage: 1, count: this.state.questions.length });
    
        if (!Array.isArray(questions)) {
          window.location.reload();
          return <></>;
        }
        const currentItems = questions.slice(firstContentIndex, lastContentIndex);
      
        return (
          <>
            {currentItems.map((question) => 
            question.questionType === "OneAnswer" ? (
              <>
              <div className="Question">
              <h2>{question.questionName}</h2>
             <span>Питання типу Вибір однієї правильної відповіді з багатьох </span><input type="image" src={ph_pencil} alt="ph_pencil" className="ph_pencil" onClick={()=>this.editQuestion(question.id)}/>
            </div>
            
              </>
          ) :
          question.questionType === "TrueFalseAnswer" ? (
            <>
            <div className="Question">
            <h2>{question.questionName}</h2>
           <span>Питання типу Правильно/Неправильно</span><input type="image" src={ph_pencil} alt="ph_pencil" className="ph_pencil" onClick={()=>this.editQuestion(question.id)}/>
          </div>
          
            </>
        ):
        question.questionType === "ManyAnswers" ? (
          <>
          <div className="Question">
          <h2>{question.questionName}</h2>
         <span>Питання типу Множинний вибір</span><input type="image" src={ph_pencil} alt="ph_pencil" className="ph_pencil" onClick={()=>this.editQuestion(question.id)}/>
        </div>
        
          </>
      ): (
        <>
        <div className="Question">
        <h2>{question.questionName}</h2>
       <span>Невичзначений тип питання</span><input type="image" src={ph_pencil} alt="ph_pencil" className="ph_pencil" onClick={()=>this.editQuestion(question.id)}/>
      </div>
        </>
      )
          )}
              
              
              
              <input type="image" src={ic_for_add} alt="ic_for_add" className="ic_for_add addNewQuestion" onClick={this.addQuestion}/>
              <div className="Pagination">
                <button onClick={prevPage} disabled={page === 1}>
                  Previous
                </button>
                {gaps.before && <span>...</span>}
                {gaps.paginationGroup.map((pageNum) => (
                  <button key={pageNum} onClick={() => setPage(pageNum)}>
                    {pageNum}
                  </button>
                ))}
                {gaps.after && <span>...</span>}
                <button onClick={nextPage} disabled={page === totalPages}>
                  Next
                </button>
              </div>
              </>
        );
      };

    
      
      return (
        <>

  <Layout/>
        <div className="background">
        <img src={LeftBack} alt="constrLeft" className="left_back_constr"/>
        <img src={RightBack} alt="constrRight" className="right_back_constr"/>
        <div className="TestNameView constrView "> 
        <h1>Редагування тесту</h1>
      <NavLink id="btnGeneral" className="showBtn btn" onClick={()=>this.show("general")}>
        <img src={Arrow} alt="arrow" id="arrowBtn_general" className="arrowBtn " />
        Назва сторінки</NavLink>
      <div className="general dropDownForm" id="general">
      
      <div className="inputArea">
        <section>
        <p className="textTestNameCourse textOption">Назва<span className="redStar">*</span></p>
        </section>
        <section>
        <input className="inputTestName textInput " name="TestName" id="TestName" type="text" onChange={this.handleInputChange} defaultValue={this.state.TestName}></input> 
        </section>
        <div className="resultBtns">
      <NavLink className="saveBtn btn resultBtn" onClick={this.saveName}>Зберегти зміни</NavLink>
      {/*<NavLink className="cancelBtn btn resultBtn">Скасувати</NavLink>*/}
      </div>
      <br/>
       </div>   
        </div>
        </div>
        <div className="testView">
        <h1>Банк питань тесту</h1>
      
    <PaginationWrapper questions={this.state.questions} />
        
      
      </div>
      </div>
        </>
      );
    }
  }

  export default EditTest;

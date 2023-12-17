import React from "react";
import "./ShowingTest.css";
import Layout from "../../Layout/Layout.js";
import { variables } from './../../Variables.js';
import { getCookie } from "../../CookieHandler.js";

import LeftBack from "../../images/Сonstructor/constrLeft.png";

class ShowingTest extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      questions: [],
      TestName: "",
      questionsAnswers: []
    };
  }

  componentDidMount = async () => {
    await this.getTest();
    await this.getQuestions();
    
  };


  
  getTest = async () => {
    try {
      const response = await fetch(variables.API_URL + 'test/' + getCookie("ShowingTest"));
      const data = await response.json();
      this.setState({ TestName: data.testName });
    } catch (error) {
      console.error("Error fetching course units:", error);
    }
  };

  getQuestions = async () => {
    try {
      const response = await fetch(variables.API_URL + 'test/GetQuestions/' + getCookie("ShowingTest"));
      const questionsData = await response.json();
      this.setState({ questions: questionsData });

      const questionsWithAnswers = await Promise.all(
        questionsData.map(async (question) => {
          try {
            const answerResponse = await fetch(variables.API_URL + 'question/GetQuestionAnswers/' + question.id);
            const answersData = await answerResponse.json();
            return { qId: question.id, answers: answersData };
          } catch (error) {
            console.error("Error fetching answers:", error);
            return { qId: question.id, answers: [] };
          }
        })
      );

      this.setState({ questionsAnswers: questionsWithAnswers });
    } catch (error) {
      console.error("Error fetching questions:", error);
    }
  };

  completeTest = async () => {
    var totalscore = 0;
    let allPoints = 0;
    this.state.questionsAnswers.map(questionsAnswer=>{
      var question = this.state.questions.find((q)=>q.id===questionsAnswer.qId);
      allPoints+=question.countOfPoints;
      if(question.questionType === "ManyAnswers"){
        var mainCorrectCount = 0;
        var correctCount = 0;
      questionsAnswer.answers.map(answer=>{
        

        var btn = document.getElementById("answer_"+answer.id);
        var btnText = document.getElementById("answerText_"+answer.id);
          if(answer.isCorrect === true){
            mainCorrectCount++;
            btnText.style.color = `green`;
            if(btn.checked === true){
           
            correctCount++;
          }
        }
        else{
          if(btn.checked === true){
            btnText.style.color = `red`;
          }
        }
      })
      var score = question.countOfPoints * (correctCount/mainCorrectCount);
      totalscore+=score;
    }
    else if(question.questionType === "OneAnswer"){
      questionsAnswer.answers.map(answer=>{
        

        var btn = document.getElementById("answer_"+answer.id);
        var btnText = document.getElementById("answerText_"+answer.id);
          if(answer.isCorrect === true){
            
            btnText.style.color = `green`;
            if(btn.checked === true){
              totalscore+=question.countOfPoints;
            
          }
        }
        else{
          if(btn.checked === true){
            btnText.style.color = `red`;
          }
        }
      })
    }
    else{
      questionsAnswer.answers.map(answer=>{
        

        var btns = document.getElementsByName("question_"+question.id);
        for (var i = 0; i < btns.length; i++) {
          if(btns[i].checked === true && btns[i].value === "true"){
          if(answer.isCorrect === true){
            
            document.getElementById("answerTextTrue_"+answer.id).style.color = `green`;
            
              totalscore+=question.countOfPoints;
            
          }
          else{
            document.getElementById("answerTextTrue_"+answer.id).style.color = `red`;
          }
        }
        else if(btns[i].checked === true && btns[i].value === "false"){
          if(answer.isCorrect === false){
            document.getElementById("answerTextFalse_"+answer.id).style.color = `green`;
            totalscore+=question.countOfPoints;
          }
          else{
            document.getElementById("answerTextFalse_"+answer.id).style.color = `red`;
          }
        }
        }
          
      })
    }
    })
    var resText = document.getElementById("points");
    resText.innerText  = "Результат: " + totalscore+"/"+allPoints;

    const inputs = document.querySelectorAll('input');
const buttons = document.querySelectorAll('button');
inputs.forEach(input => {
  input.disabled = true;
});
buttons.forEach(button => {
  button.disabled = true;
});
this.userCompleteTest();
  };

  userCompleteTest=async()=>{
    try {
      const response = await fetch(variables.API_URL + 'test/userCompleteTest/'+
      getCookie("ShowingTest") +
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
  render() {
    return (
      <>
        <Layout />
        <div className="background">
          <img src={LeftBack} alt="constrLeft" className="left_back_constr" />
          <div className="showTestView">
            <h2>{this.state.TestName}</h2>
            {this.state.questions.map((question) => {
              const foundQuestion = this.state.questionsAnswers.find((q) => q.qId === question.id);
              if (foundQuestion && foundQuestion.answers.length !== 0) {
                return (
                  <div key={question.id}>
                    <h4 className="question_name">{question.questionName}</h4>

                    <h5>{question.questionText}</h5>

                    <p>Балів: {question.countOfPoints}</p>
                    {/* Проверка типа вопроса и отображение соответствующих ответов */}
                    {question.questionType === "TrueFalseAnswer" && (
                      <div>
                       {foundQuestion.answers.map((filteredAnswer) => (
                      <div>
                        <input type="radio" id={`answer_${filteredAnswer.id}`} name={`question_${question.id}`} value="true" className="orangeRadio" /> <span id={`answerTextTrue_${filteredAnswer.id}`}>Правильно</span>
                        <input type="radio" id={`answer_${filteredAnswer.id}`} name={`question_${question.id}`} value="false" className="orangeRadio" /> <span id={`answerTextFalse_${filteredAnswer.id}`}>Неправильно</span>
                      </div>
                      ))}
                      </div>
                    )}
                    {question.questionType === "OneAnswer" && (
                      <div>
                        {foundQuestion.answers.map((filteredAnswer) => (
                          <div key={filteredAnswer.id}>
                            <input type="radio" id={`answer_${filteredAnswer.id}`} name={`question_${question.id}`} value={filteredAnswer.id} className="orangeRadio" /> <span id={`answerText_${filteredAnswer.id}`}>{filteredAnswer.answerText}</span>
                          </div>
                        ))}
                      </div>
                    )}
                    {question.questionType === "ManyAnswers" && (
                      <div>
                        {foundQuestion.answers.map((filteredAnswer) => (
                          <div key={filteredAnswer.id}>
                            <input type="checkbox" id={`answer_${filteredAnswer.id}`} name={`question_${question.id}`} value={filteredAnswer.id} className="blueRadioCheckbox" style={{ width: '1.3rem', height: '1.3rem' }}/> <span id={`answerText_${filteredAnswer.id}`}>{filteredAnswer.answerText}</span>
                          </div>
                        ))}
                      </div>
                    )}
                  </div>
                );
              }
             
            })}
            <button className="blueBtn" onClick={this.completeTest}>Виконати</button>
            <p id="points"></p>
          </div>
        </div>
      </>
    );
  }
}

export default ShowingTest;

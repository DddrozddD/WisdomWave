import React from "react";
import { useLocation } from "react-router";
import Layout from "../../Layout/Layout.js";
import {variables} from './../../Variables.js';
import RightBack from "../../images/Сonstructor/constrRight.png"
import LeftBack from "../../images/Сonstructor/constrLeft.png"
import ic_for_add from "../../images/Сonstructor/ic_for_add.png"
import Arrow from "../../images/arrow.png"
import { NavLink } from "react-router-dom";
import { setCookie, deleteCookie, getCookie } from './../../CookieHandler.js';
import {usePagination} from "../../Pagination.js"

class EditPage extends React.Component {

 constructor(props) {
    super(props);

    this.state = {
      pageId: 0,
      paragraphs: [],
      pageName: ""
    }
} 

componentDidMount=()=>{

  this.setState({pageId:getCookie("EditPageId")});
  document.getElementById("general").style.display = "block";
    document.getElementById("textInfo").style.display = "block";
    document.getElementById("view").style.display = "block" 

    this.getPage(getCookie("EditPageId"));
    
  
}

getPage=async(id)=>{
  try {
    const response = await fetch(variables.API_URL + 'page/'+id);
    const data = await response.json();
    console.log(data);
    this.setState({  pageName: data.pageName});
    document.getElementById(`PageName`).value = data.pageName;
    
  } catch (error) {
    console.error("Error fetching page:", error);
  }
  try {
    const response = await fetch(variables.API_URL + 'page/GetParagraphsOfPage/'+id);
    const data = await response.json();
    console.log(data);
    this.setState({  paragraphs: data});
    
  } catch (error) {
    console.error("Error fetching page paragraphs:", error);
  }
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
addParagraph=async()=>{
  try {
    const response = await fetch(variables.API_URL + 'paragraph', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json; odata=verbose'
        },
        body: JSON.stringify({
          "ParagraphName":"",
          "ParagraphText": "",
          "pageId" : this.state.pageId
      }), 
    })

    if (response.ok) {
        console.log("Сторінка створена");
        this.getPage();
    } else {
        console.error("створити не вдалось");
    }
} catch (error) {
    console.error("Помилка:", error);
}
}

saveParagraph=async(idP)=>{
  try {
    const nameP =document.getElementById("ParagraphName_"+idP).value;
    const textP =document.getElementById("ParagraphText_"+idP).value;
    const response = await fetch(variables.API_URL + 'paragraph/'+idP , {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json; odata=verbose'
        },
        body: JSON.stringify({
          "ParagraphName": nameP,
          "ParagraphText": textP,
          "pageId" : this.state.pageId
        }), 
        
    })
    if(response.ok){
      this.getPage();
    }
} catch (error) {
    console.error("Помилка:", error);
}
}
  


   render() {
    const PaginationWrapper = ({ paragraphs }) => {
      
      const {
        page,
        totalPages,
        firstContentIndex,
        lastContentIndex,
        nextPage,
        prevPage,
        setPage,
        gaps,
      } = usePagination({ contentPerPage: 1, count: paragraphs.length });
      if (!Array.isArray(paragraphs)) {
        window.location.reload();
        return <></>;
      }
      const currentItems = paragraphs.slice(firstContentIndex, lastContentIndex);
     
      return (
        <>
           {currentItems.map(paragraph => 
            <>
           <div key={paragraph.id} className="inputArea">
            <section>
            <p className="textPageNameCourse textOption">Назва розділу<span className="redStar">*</span></p>
            </section>
            <section>
            <input className="inputPageName textInput " name={`ParagraphName_${paragraph.id}`} id={`ParagraphName_${paragraph.id}`} defaultValue={paragraph.paragraphName} type="text"  ></input> 
            </section>
           </div>   
    <div className="inputArea">
    <p className="textOption inputTextarea">Текст</p>
    
    
    <textarea className="inputAnnotation ParagraphText" id={`ParagraphText_${paragraph.id}`} name={`ParagraphText_${paragraph.id}`} key={paragraph.id} >{paragraph.paragraphText}</textarea> <br/>
    </div>
    <div className="resultBtns">
      
          <NavLink className="saveBtn btn resultBtn" onClick={()=>this.saveParagraph(paragraph.id)} >Зберегти зміни</NavLink>
          <NavLink className="cancelBtn btn resultBtn">Скасувати</NavLink>
          </div>
          <br/>
         
            </>
            
            )}
            <input type="image" src={ic_for_add} alt="ic_for_add" className="ic_for_add addText" onClick={this.addParagraph}/>
            <div>
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

       <div className="constrView"> 
      <h1>Редагування сторінки</h1>
      <NavLink id="btnGeneral" className="showBtn btn" onClick={()=>this.show("general")}>
        <img src={Arrow} alt="arrow" id="arrowBtn_general" className="arrowBtn " />
        Назва сторінки</NavLink>
      <div className="general dropDownForm" id="general">
      
      <div className="inputArea">
        <section>
        <p className="textPageNameCourse textOption">Назва<span className="redStar">*</span></p>
        </section>
        <section>
        <input className="inputPageName textInput " name="PageName" id="PageName" type="text" onChange={this.handleInputChange} ></input> 
        </section>
        <div className="resultBtns">
      <NavLink className="saveBtn btn resultBtn" onClick={this.createCourse}>Зберегти зміни</NavLink>
      <NavLink className="cancelBtn btn resultBtn">Скасувати</NavLink>
      </div>
      <br/>
       </div>   
        </div>
        
      <br/>
      <NavLink id="btn" className="showBtn btn" onClick={()=>this.show("textInfo")}>
        <img src={Arrow} alt="arrow" id="arrowBtn_textInfo" className="arrowBtn" />
        Текстова інформація</NavLink>
        <div className="textInfo dropDownForm" id="textInfo">
        <PaginationWrapper paragraphs={this.state.paragraphs} />
        </div>
     
      <br/>
      <NavLink id="btn" className="showBtn btn" onClick={()=>this.show("view")}>
        <img src={Arrow} alt="arrow" id="arrowBtn_view" className="arrowBtn" />
        Додаткові файли</NavLink>
      <div className="view dropDownForm" id="view">
      
      </div>
      <br/>
      
      <p className="downP">Обов’язкові поля  форми помічені символом<span className="redStar">*</span></p>
      </div>
      </div>
      </>
      
    );
  }
}


export default EditPage;
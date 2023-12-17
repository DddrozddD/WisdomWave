import React    from "react";
import template from "./ShowingPage.jsx";
import RightBack from "../../images/Сonstructor/constrRight.png"
import Layout from "../../Layout/Layout.js";
import {variables} from './../../Variables.js';
import ph_pencil from "../../images/Сonstructor/ph_pencil.png"
import ic_for_add from "../../images/Сonstructor/ic_for_add.png"
import { getCookie, setCookie } from "../../CookieHandler.js";
import { BrowserRouter, Route, Routes, NavLink } from 'react-router-dom'; 
import {usePagination} from "../../Pagination.js"

class ShowingPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      pageId: 0,
      paragraphs: [],
      pageName: ""
    }
}
componentDidMount=()=>{

  this.setState({pageId:getCookie("ShowingPage")});

    this.getPage(getCookie("ShowingPage"));
    this.userCompletePage();
  
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

userCompletePage=async()=>{
  try {
    const response = await fetch(variables.API_URL + 'page/userCompletePage/'+
    getCookie("ShowingPage") +
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
           <h3>{paragraph.paragraphName}</h3>
           <p>
      {paragraph.paragraphText}
           </p>
          <br/>
         
            </>
            
            )}
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
          <img src={RightBack} alt="constrRight" className="right_back_constr"/>
          <div className="background">
            <div className="ShowPage">
            <h2>{this.state.pageName}</h2>
          <PaginationWrapper paragraphs={this.state.paragraphs} />
          </div>
          </div>
      </>
    );
  }
}

export default ShowingPage;

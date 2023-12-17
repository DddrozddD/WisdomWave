import React    from "react";
import template from "./MainPage.jsx";
import Layout from "../Layout/Layout.js";
import back1 from "./../images/back1.png"
import block_right2 from "./../images/2block_right.png"
import block_left2 from "./../images/2block_left.png"
import { getCookie } from "../CookieHandler.js";
import {variables} from './../Variables.js';

class MainPage extends React.Component {

  goToWork=async()=>{
    if((getCookie("UserSecretKey")==null) || (getCookie("UserSecretKey")=="")){
      window.location.href=variables.PAGE_URL+"login";
    }
    else{
      window.location.href=variables.PAGE_URL+"user-courses";
    }
  }
  render() {
    return (
    <>
    <Layout/> 
    <div className="block first_block">
      <img src={back1} alt="Back1" className="back_right"/>
      <h1>Wisdom Wave</h1>
      <h2>Онлайн-платформа 
для безперервного навчання
з інтерактивними завданнями</h2>

<button className="whiteBtn allCoursesBtn" type="button" onClick={()=>{window.location.href=variables.PAGE_URL+"courses"}}>Усі курси</button>
    </div>
    <div className="block">
      <img src={block_right2} alt="block_right2" className="back_right"/>
      <img src={block_left2} alt="block_left2" className="back_left"/>
      <div className="subblock2">

      <button className="orangeBtn goBtn" type="button" onClick={()=>this.goToWork()}>Нумо, рушаймо!</button>
      <h1>Ми чекаємо саме на тебе! </h1>
      <h2>Радо вітаємо тебе у спільноті вільних, творчих, натхнених особистостей, готових вчитися, втілювати у життя нові ідеї й підвищувати рівень власних компетентностей у різних сферах знань</h2>
      </div>
    </div>
    </>  
    );
  }
}

export default MainPage;


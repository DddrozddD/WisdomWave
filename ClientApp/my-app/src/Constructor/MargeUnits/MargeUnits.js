import React    from "react";
import template from "./MargeUnits.jsx";
import RightBack from "../../images/Сonstructor/constrRight.png"
import LeftBack from "../../images/Сonstructor/constrLeft.png"
import Layout from "../../Layout/Layout.js";
import {variables} from './../../Variables.js';
import ph_pencil from "../../images/Сonstructor/ph_pencil.png"
import ic_for_add from "../../images/Сonstructor/ic_for_add.png"
import { getCookie, setCookie } from "../../CookieHandler.js";
import { BrowserRouter, Route, Routes, NavLink } from 'react-router-dom'; 

class MargeUnits extends React.Component {


  constructor(props) {
    super(props);

    this.state = {
      units: [],
      courseId: 0,
      changedNameUnit:"",
      tests: [],
      pages: []
    }
} 
componentDidMount=async()=>{
  this.getEditCourseUnits();
}
  getEditCourseUnits = async()=>{
    let Units = [];
    try {
    const response = await fetch(variables.API_URL + 'courses/GetEditCourseUnits/'+getCookie("EditCourseId"));
    const data = await response.json();
    console.log(data);
    Units = data;
    this.setState({  courseId: Units[0].courseId });
    
  } catch (error) {
    console.error("Error fetching course units:", error);
  }
  Units.map(async unit=>{
    try {
      const response = await fetch(variables.API_URL + 'unit/GetAllPagesOfUnit/'+unit.id);
      const data = await response.json();
      console.log(data);
      unit.pages = [];
      if(data!=null){
      unit.pages = data;
      }
      this.setState({units: Units});
    } catch (error) {
      console.error("Error fetching unit tests:", error);
    }
    try {
      const response = await fetch(variables.API_URL + 'unit/GetAllTestsOfUnit/'+unit.id);
      const data = await response.json();
      console.log(data);
      unit.tests = [];
      if(data!=null){
      unit.tests = data;
      }
    } catch (error) {
      console.error("Error fetching unit tests:", error);
    }
    this.setState({units: Units});
  });

}

 

  editUnitName=async (id, name, number, date)=>{
    if(document.getElementById(`unitNameShow_${id}`).style.display == "none"){
      document.getElementById(`unitNameShow_${id}`).style.display = "inline-block";
      try {
        const response = await fetch(variables.API_URL + 'unit/'+id , {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; odata=verbose'
            },
            body: JSON.stringify({
                "Id":id,
                "Number": number,
                "UnitName": this.state.changedNameUnit,
                "courseId": this.state.courseId,
                "DateOfCreate": date
            }), 
            
        })
        if(response.ok){
          this.getEditCourseUnits();
        }
    } catch (error) {
        console.error("Помилка:", error);
    }
    
  
      document.getElementById(`unitNameEdit_${id}`).style.display = "none";
    }
    else{
      document.getElementById(`unitNameShow_${id}`).style.display = "none";
      document.getElementById(`unitNameEdit_${id}`).value = name;
      document.getElementById(`unitNameEdit_${id}`).style.display = "inline-block";
    }
  }

  handleInputChange = (e) => {
    const { name, value } = e.target;
    this.setState({
        [name]: value
    });
  }

  addUnit=async()=>{
    try {
      const response = await fetch(variables.API_URL + 'unit/addDefaultUnit/'+this.state.courseId, {
          method: 'POST',
          headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json; odata=verbose'
          }
      })

      if (response.ok) {
          console.log("Блок створений");
          this.getEditCourseUnits();
      } else {
          console.error("створити не вдалось");
      }
  } catch (error) {
      console.error("Помилка:", error);
  }
  }

  addPage=async(unitId)=>{
    try {
      const response = await fetch(variables.API_URL + 'page', {
          method: 'POST',
          headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json; odata=verbose'
          },
          body: JSON.stringify({
            "PageName":"Нова сторінка",
            "unitId": unitId,
            "PhotoLinks": "",
            "VideoLinks":""
        }), 
      })

      if (response.ok) {
          console.log("Сторінка створена");
          this.getEditCourseUnits();
      } else {
          console.error("створити не вдалось");
      }
  } catch (error) {
      console.error("Помилка:", error);
  }
  }

  addTest=async(unitId)=>{
    try {
      const response = await fetch(variables.API_URL + 'test', {
          method: 'POST',
          headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json; odata=verbose'
          },
          body: JSON.stringify({
            "TestName":"Новий тест",
            "TestDescription": "",
            "unitId": unitId
        }), 
      })

      if (response.ok) {
          console.log("Тест створений");
          this.getEditCourseUnits();
      } else {
          console.error("створити не вдалось");
      }
  } catch (error) {
      console.error("Помилка:", error);
  }
  }
 
    toEditPage=(id)=>{
      setCookie("EditPageId", id, {secure: true, 'max-age': 3600});
    }
    toEditTest=(id)=>{
      setCookie("EditTestId", id, {secure: true, 'max-age': 3600});
    }

  render() {
    



      const {
          units,
          courseId
      }=this.state;

    return ( <>
      <Layout/>
      <div className="background">
      <img src={LeftBack} alt="constrLeft" className="left_back_constr"/>
      <img src={RightBack} alt="constrRight" className="right_back_constr"/>
      <div className="constrView courseView">
      {units.map((unit) => 
    
            <div className="unitView">
              <div className="unitNameBlock">
              <input id={`unitNameEdit_${unit.id}`} name="changedNameUnit" style={{ display: "none"}}  type="text" className="editUnitName" onChange={this.handleInputChange}></input>
              <p id={`unitNameShow_${unit.id}`} style={{ display: "inline-block"}} className="unitName" >{unit.unitName}</p>
              <input type="image" src={ph_pencil} alt="ph_pencil" className="ph_pencil" onClick={()=>this.editUnitName(unit.id, unit.unitName, unit.number,  unit.dateOfCreate)}/>
              </div>
              <div>
              {unit.pages != null && unit.pages.map(page=>
                <>
                <NavLink  className="materials" to={{pathname :"/edit-page" }} onClick={()=>this.toEditPage(page.id)}>{page.pageName}</NavLink>
                <br/>
                </>
                  )}
                  {unit.tests != null && unit.tests.map(test=>
                <>
                <NavLink  className="materials" to={{pathname :"/edit-test" }} onClick={()=>this.toEditTest(test.id)}>{test.testName}</NavLink>
                <br/>
                </>
                  )}
              </div>
              <div className="addButtons">  
                <div style={{ display: "inline-block"}}>
                <button  className="addButton" onClick={()=>this.addPage(unit.id)}>Додати сторінку</button>
                <br/>
                </div>
                <div style={{ display: "inline-block"}}>
                <button  className="addButtonTest addButton" onClick={()=>this.addTest(unit.id)}>Додати тест</button>
                <br/>              
                </div>
              </div>
              </div>
            )}
            <input type="image" src={ic_for_add} alt="ic_for_add" className="ic_for_add" onClick={this.addUnit}/>
      </div>
      </div>
      </>);
  }
}

export default MargeUnits;

import React    from "react";
import template from "./MargeUnits.jsx";
import RightBack from "../../images/Сonstructor/constrRight.png"
import LeftBack from "../../images/Сonstructor/constrLeft.png"
import Layout from "../../Layout/Layout.js";

class MargeUnits extends React.Component {

  constructor(props) {
    super(props);

    this.state = {
      units: [],
      courseId: 0,

    }
} 
  
  getEditCourse = async()=>{
    try{
      await fetch(variables.API_URL+'courses/GetEditCourse')
          .then(response=>response.json())
          .then(data=>{
            console.log(data);
           this.setState({: data.name});
           document.getElementById("userName").value = data.name;  
          });
          
         
          
        }
        catch(error){
          console.error("Помилка:", error);
        }
      }
  }
  getAllunits = async()=> {

  }

  render() {
    return (
      <>
      <Layout/>
      <div className="background">
      <img src={LeftBack} alt="constrLeft" className="left_back_constr"/>
      <img src={RightBack} alt="constrRight" className="right_back_constr"/>
      <div className="constrView"> 

      </div>
      </div>
      </>
    );
  }
}

export default MargeUnits;

import React    from "react";
import template from "./UserProfile.jsx";
import Layout from "../../Layout/Layout.js";
import MainProfile from "../MainProfile/MainProfile.js";
import {variables} from './../../Variables.js';
import { setCookie, deleteCookie, getCookie } from './../../CookieHandler.js';
import profile_right from './../../images/profile_right.png'
import profile_left from './../../images/profile_left.png'


class UserProfile extends React.Component {
  constructor(props){
    super(props);

    this.state={
      userName: "",
      userDate: "",
      userSurname:"",
      userAbout:"",
      userTelephone:"",
      userEmail:""
    }
  }
  componentDidMount=async()=>{
    this.getUser();
    }
       
  getUser=async()=>{
    try{

      await fetch(variables.API_URL+'authorization/GetUser/'+getCookie("UserSecretKey"))
  
          .then(response=>response.json())
          .then(data=>{
            console.log(data);
           this.setState({userName: data.name, userSurname:data.surname, userDate: data.dateOfBorn, userAbout:data.about,
            userEmail: data.email, userTelephone: data.telephone});
           
          });
          
         
          
        }
        catch(error){
          console.error("Помилка:", error);
        }
  }
  
  saveUser=async()=>{
    try {
     
      const response = await fetch(variables.API_URL + 'users/'+ getCookie("UserSecretKey"), {
          method: 'PUT',
          headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json; odata=verbose'
          },
          body: JSON.stringify({
            "Name": this.state.userName,
            "Surname": this.state.userSurname,
            "Telephone": this.state.userTelephone,
            "About" : this.state.userAbout
          }), 
          
      })
      if(response.ok){
        this.getUser();
      }
  } catch (error) {
      console.error("Помилка:", error);
  }
  }
  
  handleInputChange = (e) => {
    const { name, value } = e.target;
    this.setState({
        [name]: value
    });
  }
  render() {
    return (
      <>  

<Layout/>
     <img src={profile_right} alt="profile_right" className="back_right"/>
      <img src={profile_left} alt="profile_left" className=" left_back_profile"/>
      <div className="profile_body">
        <div>
      <MainProfile />
      </div>


      <div className="user_profile">
        {/*
<div className="add_photo_section">
<div className="add_photo_subsection logo_to_add_photo">

      <svg xmlns="http://www.w3.org/2000/svg" width="120" height="120" viewBox="0 0 120 120" fill="none">
<circle cx="60" cy="60" r="60" fill="#D4F3FC"/>
</svg>
</div>
<div className="add_photo_subsection add_photo_btn_section">

        <button onClick={null} className="add_photo_btn" >+ Додати зображення</button><br/>
        
        <label className="text_for_add_photo_btn">Підтримує формати: PNG, JPEG або GIF. Макс. розмір 10 мб. </label>
    </div>
</div>*/}

        <h2 className="profile_settings_label">Налаштування профілю</h2>
        <div className="profile_settings">
            <div className="settings_sections">
              <div className="setting_section">
              <p>Ім’я користувача</p>
              <input id="userName" name="userName" type="text" placeholder="Ім’я" onChange={this.handleInputChange} defaultValue={this.state.userName}/>
              </div>
              <div className="setting_section">
              <p>Призвище користувача</p>
              <input id="userName" name="userName" type="text" placeholder="Ім’я" onChange={this.handleInputChange} defaultValue={this.state.userSurname}/>
              </div>
              {/*
              <div className="setting_section">
              <p>Дата народження</p>
              <input id="userDate" name="userDate" type="date" placeholder="Дата народження" onChange={this.handleInputChange} defaultValue={this.state.dateOfBorn}/>
</div>*/}
              <div className="setting_section">
              <p>Про себе</p>
              <textarea id="userAbout" name="userAbout" placeholder="Щось про себе" className="description_input" onChange={this.handleInputChange} defaultValue={this.state.userAbout}/>
              </div>
            </div>
        </div>
        <h2 className="profile_settings_label">Зв'язок</h2>
        <div className="profile_settings">
            <div className="settings_sections">
              <div className="setting_section">
              <p>Електрона пошта</p>
              <input id="userEmail" name="userName" type="text" placeholder="wisdomwave.ww@gmail.com" onChange={this.handleInputChange} value={this.state.userEmail}/>
              </div>
              <div className="setting_section">
              <p>Номер телефону</p>
              <input id="userTelephone" name="userTelephone" type="tel" placeholder="0000000000" onChange={this.handleInputChange} defaultValue={this.state.userTelephone}/>
              </div>
            </div>
        </div>
        <h2 className="profile_settings_label">Безпека</h2>
        <div className="profile_settings">
            <div className="settings_sections">
              <div className="setting_section">
              <p>Змніа паролю</p>
              <section><a href="">Зміна паролю.</a> Підвищь рівень безпеки обравши більш надійний пароль</section>
              </div>
              
              
            </div>
        </div>
        <button className="save_btn" onClick={this.saveUser}>Зберегти зміни</button>
      </div>
      </div>
      </>
    );
  }
}

export default UserProfile;

import React    from "react";
import template from "./UserProfile.jsx";
import Layout from "../../Layout/Layout.js";
import MainProfile from "../MainProfile/MainProfile.js";
import {variables} from './../../Variables.js';
import { setCookie, deleteCookie, getCookie } from './../../CookieHandler.js';


class UserProfile extends React.Component {
  constructor(props){
    super(props);

    this.state={
      userName: "",
      userDate: "",
      userAbout:""
    }
  }
  componentDidMount=async()=>{
    try{

    await fetch(variables.API_URL+'authorization/GetUser/'+getCookie("YourSecretKeyHere"))

        .then(response=>response.json())
        .then(data=>{
          console.log(data);
         this.setState({userName: data.name});
         document.getElementById("userName").value = data.name;  
        });
        
       
        
      }
      catch(error){
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

      <div className="profile_body">
        <div>
      <MainProfile />
      </div>


      <div className="user_profile">
        
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
</div>

        <h2 className="profile_settings_label">Налаштування профілю</h2>
        <div className="profile_settings">
            <div className="settings_sections">
              <div className="setting_section">
              <p>Ім’я користувача</p>
              <input id="userName" name="userName" type="text" placeholder="Ім’я" onChange={this.handleInputChange} value={this.state.userName}/>
              </div>
              <div className="setting_section">
              <p>Дата народження</p>
              <input id="userDate" name="userDate" type="date" placeholder="Дата народження" onChange={this.handleInputChange}/>
              </div>
              <div className="setting_section">
              <p>Про себе</p>
              <textarea id="userAbout" name="userAbout" placeholder="Щось про себе" className="description_input" onChange={this.handleInputChange}/>
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
        <button className="save_btn">Зберегти зміни</button>
      </div>
      </div>
      </>
    );
  }
}

export default UserProfile;

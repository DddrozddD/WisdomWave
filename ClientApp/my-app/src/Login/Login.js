import React    from "react";
import template from "./Login.jsx";
import {variables} from './../Variables.js';
import RightBack from './../images/BackAuthRight.png'
import LeftBack from './../images/BackAuthLeft.png'
import { NavLink } from "react-router-dom";

export class Login extends React.Component  {

  constructor(props) {
    super(props);

    this.state = {
        email: "",
        password: ""
    }
}
handleInputChange = (e) => {
  const { name, value } = e.target;
  this.setState({
      [name]: value
  });
}
loginClick = async () => {

  const { email, password} = this.state;
  try {
      const response = await fetch(variables.API_URL + 'authorization/LoginUser', {
          method: 'POST',
          headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json; odata=verbose'
          },
          body: JSON.stringify({
            "Email": email,
              "Password": password
              
          })
      });

      if (response.ok) {
        
        window.location.assign('http://localhost:3000/');
      } else {
          console.error("Увійти не вдалась");
      }
  } catch (error) {
      console.error("Помилка:", error);
  }
}
  render() {
    return (
      <>
      <img src={RightBack} alt="RightBack" className="right_back"/>
      <img src={LeftBack} alt="LeftBack" className="left_back"/>
      
      <div className="login">
       
        <div className="login__container">
        <p class="login__welcomeText">Вітаю</p>
        <form className="login_form" action="">
        <p class="login__form__emailText">Електрона пошта</p>
        <input type="email" name="email" id="email" class="login__formInput emailInput" value={this.state.email}
                          onChange={this.handleInputChange}></input>
        <p class="login__form__passwordText">Пароль</p>
        <input type="password" name="password" id="password" class="login__formInput passInput" value={this.state.password}
                          onChange={this.handleInputChange}></input>
        <a href="resetPass.html" className="resetPassBtn">Забув(ла) пароль</a>
        <button type = "button" onClick={this.loginClick} className="login__form__button">Увійти</button>
        <div class="login__formTableText">
        <p class="createAccountText">Немає облікового запису?</p>
        <NavLink to="/registration" className="createAccountBtn">Зареєструвати</NavLink>
        </div>
        </form>
        </div>
     </div>
     </>
    )
  }
}


export default Login;
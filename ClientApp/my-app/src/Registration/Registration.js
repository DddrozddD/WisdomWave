import React,{Component} from 'react';
import template from "./Registration.jsx";
import {variables} from './../Variables.js';
import RightBack from './../images/BackAuthRight.png'
import LeftBack from './../images/BackAuthLeft.png'
import { NavLink } from "react-router-dom";

export class Registration extends Component {
  constructor(props) {
      super(props);

      this.state = {
          email: "",
          name: "",
          surname: "",
          password: "",
          confirmPassword: ""
      }
  }

  handleInputChange = (e) => {
      const { name, value } = e.target;
      this.setState({
          [name]: value
      });
  }
  
  

  regClick = async () => {

      const {name, surname, email, password, confirmPassword } = this.state;
      try {
          const response = await fetch(variables.API_URL + 'authorization/RegUser', {
              method: 'POST',
              headers: {
                  'Accept': 'application/json',
                  'Content-Type': 'application/json; odata=verbose'
              },
              body: JSON.stringify({
                  "Name":name,
                  "Surname": surname,
                  "Password": password,
                  "ConfirmPass": confirmPassword,
                  "Email": email
              }), 
              
          })

          if (response.ok) {
              console.log("Реєстрація успішна");
              window.location.assign('http://localhost:3000/');
          } else {
              console.error("Зареєструватися не вдалась");
          }
      } catch (error) {
          console.error("Помилка:", error);
      }
  }

  render() {
      return (
        <>
        <img src={RightBack} alt="RightBackReg" className="right_back"/>
      <img src={LeftBack} alt="LeftBackReg" className="left_back"/>
      
          <div className="register">
              <div className="register__container">
                  <p className="register__welcomeText">Реєстрація</p>
                  <form className='register_form'>
                      <p className="register__form__Text">Електрона пошта</p>
                      <input
                          type="email"
                          name="email"
                          id="email"
                          className="register__formInput emailInput"
                          value={this.state.email}
                          onChange={this.handleInputChange}
                      />
                       <p className="register__form__Text">Ім’я користувача</p>
                      <input
                          type='text'
                          name="name"
                          id="name"
                          className="register__formInput nameInput"
                          value={this.state.name}
                          onChange={this.handleInputChange}
                      />
                      <p className="register__form__Text">Призвище користувача</p>
                      <input
                          type="text"
                          name="surname"
                          id="surname"
                          className="register__formInput surnameInput"
                          value={this.state.surname}
                          onChange={this.handleInputChange}
                      />
                      <p className="register__form__Text">Пароль</p>
                      <input
                          type="password"
                          name="password"
                          id="password"
                          className="register__formInput passInput"
                          value={this.state.password}
                          onChange={this.handleInputChange}
                      />
                      <p className="register__form__Text">Підтвердіть пароль</p>
                      <input
                          type="password"
                          name="confirmPassword"
                          id="confirmPassword"
                          className="register__formInput"
                          value={this.state.confirmPassword}
                          onChange={this.handleInputChange}
                      />
                      <button type="button" onClick={this.regClick} className="register__form__button">Зареєструватися</button>
                  </form>
              </div>
          </div>
          </>
      )
  }
}

export default Registration;
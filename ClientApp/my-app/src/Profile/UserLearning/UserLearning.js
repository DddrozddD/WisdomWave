import React    from "react";
import template from "./UserLearning.jsx";
import Layout from "../../Layout/Layout.js";
import MainProfile from "../MainProfile/MainProfile.js";
import profile_right from './../../images/profile_right.png'
import profile_left from './../../images/profile_left.png'

class UserLearning extends React.Component {
  render() {
    return (
      
        <>
      <Layout/>
      <img src={profile_right} alt="profile_right" className="back_right"/>
      <img src={profile_left} alt="profile_left" className=" left_back_profile"/>
      <div className="profile_body">
      <div>
      <MainProfile/>
      </div>
        <div className="user_learning_courses_list">
      <h1>Пройдені курси</h1>
        </div>
      </div>
      
      </>
      
    );
  }
}

export default UserLearning;

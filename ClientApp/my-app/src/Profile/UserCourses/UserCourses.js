import React    from "react";
import template from "./UserCourses.jsx";
import Layout from "../../Layout/Layout.js";
import MainProfile from "../MainProfile/MainProfile.js";

class UserCourses extends React.Component {
  render() {
    
    return (
      <>
      <Layout/>
      
      <div className="profile_body">
      <div>
      <MainProfile/>
      </div>
        <div className="user_courses_list">
      <h1>Авторські курси</h1>
        </div>
      </div>
      
      </>
    );
  }
}

export default UserCourses;

import React    from "react";
import template from "./UserLearning.jsx";
import Layout from "../../Layout/Layout.js";
import MainProfile from "../MainProfile/MainProfile.js";

class UserLearning extends React.Component {
  render() {
    return (
      
        <>
      <Layout/>
      
      <div className="profile_body">
      <div>
      <MainProfile/>
      </div>
        <div className="user_learning_courses_list">
      <h1>Навчання</h1>
        </div>
      </div>
      
      </>
      
    );
  }
}

export default UserLearning;

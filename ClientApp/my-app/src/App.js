import './App.css';
import {Layout} from './Layout/Layout';
import { Registration } from './Registration/Registration';
import { Login } from './Login/Login';
import { BrowserRouter, Route, Routes, NavLink } from 'react-router-dom'; 
import MainProfile from './Profile/MainProfile/MainProfile';
import UserProfile from './Profile/UserProfile/UserProfile';
import UserCourses from './Profile/UserCourses/UserCourses';
import UserLearning from './Profile/UserLearning/UserLearning';
import { useState } from 'react';
import MainPage from './MainPage/MainPage';
import AddCourse from './Constructor/AddCourse/AddCourse';
import MargeUnits from './Constructor/MargeUnits/MargeUnits';
import EditPage from './Constructor/EditPage/EditPage';
import EditTest from './Constructor/EditTest/EditTest';
import EditQuestion from './Constructor/EditQuestion/EditQuestion';
import MainCoursePage from './CourseDisplay/MainCoursePage/MainCoursePage';
import ShowingCourses from './ShowingCourses/ShowingCourses';
import EditCourse from './Constructor/EditCourse/EditCourse';
import ShowingPage from './CourseDisplay/ShowingPage/ShowingPage';
import ShowingTest from './CourseDisplay/ShowingTest/ShowingTest';
import AboutUs from './AboutUs/AboutUs';





function App() {
 
  


  return (

    
    <BrowserRouter>
    <Routes>
    <Route path='/' element={<MainPage />} />
    <Route path='/registration' element={<Registration />} />
    <Route path='/login' element={<Login />} />
    <Route path='/home' element={<Layout />} />
    <Route path='/profile' element={<MainProfile />} />
    <Route path='/user-profile' element={<UserProfile />} />
    <Route path='/user-courses' element={<UserCourses />} />
    <Route path='/user-studing' element={<UserLearning />} />
    <Route path='/add-course' element={<AddCourse />} />
    <Route path='/marge-units' element={<MargeUnits />} />
    <Route path='/edit-page' element={<EditPage />} />
    <Route path='/edit-test' element={<EditTest />}/>
    <Route path='/edit-question' element={<EditQuestion />}/>
    <Route path='/display-course' element={<MainCoursePage />}/>
    <Route path='/courses' element={<ShowingCourses />}/>
    <Route path='/edit-course' element={<EditCourse />}/>
    <Route path='/show-page' element={<ShowingPage />}/>
    <Route path='/show-test' element={<ShowingTest />}/>
    <Route path='/about-us' element={<AboutUs />}/>
    </Routes>
    </BrowserRouter>
  );
}

export default App;
import React from "react";
import template from "./Layout.jsx";
import { Registration } from '../Registration/Registration.js';
import { BrowserRouter, Route, Routes, NavLink } from 'react-router-dom'; 
import IdentityUser from './../Variables.js';
import {variables} from './../Variables.js';
import { setCookie, deleteCookie, getCookie } from './../CookieHandler.js';


export class Layout extends React.Component {
  constructor(props){
    super(props);

    this.state={
    UserName:"",
    UserSurname:""
    }
  }
  
  componentDidMount=async()=>{
    try{


      await fetch(variables.API_URL+'authorization/GetUser/'+getCookie("UserSecretKey"))
    .then(response=>response.json())
    .then(data=>{
      if(data.title!="Bad Request"){
        console.log(data);
        this.setState({UserName:data.name, UserSurname:data.surname});
      }
      

    });
  }
  catch(error){
    console.error("Помилка:", error);
  }
    
  
  }

<<<<<<< Updated upstream
=======



  
>>>>>>> Stashed changes
  singOut=async()=>{
    try {
      const response = await fetch(variables.API_URL + 'authorization/LogoutUser');
      const data = await response.json();
      console.log(data);
      
      
    } catch (error) {
      console.error("Error fetching page:", error);
    }
    deleteCookie("UserSecretKey");
    window.location.href="http://localhost:3000/"
  }

  render = () => {
    let userDiv;
   
      if((this.state.UserName!="")){
      userDiv = (
        <>
        <div className='right_header'>
        <NavLink to="/user-profile"className={"user_botton btn"}>
        {this.state.UserName} {this.state.UserSurname}
        </NavLink>
        <NavLink className={"reg_button"} onClick={this.singOut}>
         Вийти
     </NavLink>
         </div>
        </> 
      )
      }
    
    else{
      userDiv = (

             
         <>
         <div className='right_header'>
     <NavLink to="/login"className={"login_button"}>
         Увійти  
     </NavLink>
     <NavLink to="/registration" className={"reg_button"}>
         Зареєструватися
     </NavLink>
     {/*<div className='search'>
         
     <svg className='loupe' xmlns="http://www.w3.org/2000/svg" width="24" height="23" viewBox="0 0 24 23" fill="none">
 <path d="M9.13606 2.557e-08C10.8838 -3.55639e-05 12.5949 0.474241 14.0663 1.36656C15.5377 2.25888 16.7076 3.53178 17.4373 5.03425C18.167 6.53672 18.4258 8.20569 18.1831 9.84314C17.9403 11.4806 17.2061 13.0178 16.0677 14.2724L24 21.7769L23.0064 22.7169L15.0726 15.2111C13.9537 16.1163 12.6201 16.7523 11.1854 17.0649C9.75074 17.3775 8.25745 17.3574 6.83273 17.0063C5.40802 16.6552 4.0941 15.9836 3.00288 15.0486C1.91166 14.1136 1.07546 12.9429 0.565513 11.6363C0.055564 10.3296 -0.113031 8.92577 0.0740861 7.54422C0.261203 6.16266 0.79849 4.84436 1.64018 3.70159C2.48188 2.55882 3.60305 1.62543 4.9082 0.980929C6.21335 0.336423 7.66382 -0.000107105 9.13606 2.557e-08ZM9.13606 1.32964C7.08596 1.32964 5.11983 2.10012 3.67019 3.47158C2.22055 4.84303 1.40614 6.70313 1.40614 8.64267C1.40614 10.5822 2.22055 12.4423 3.67019 13.8138C5.11983 15.1852 7.08596 15.9557 9.13606 15.9557C11.1862 15.9557 13.1523 15.1852 14.6019 13.8138C16.0516 12.4423 16.866 10.5822 16.866 8.64267C16.866 6.70313 16.0516 4.84303 14.6019 3.47158C13.1523 2.10012 11.1862 1.32964 9.13606 1.32964Z" fill="#190C09"/>
 </svg>
     <input placeholder="Пошук" type='text' className='search_line'/>
      </div>*/}
 
 </div>
         </>
          )
    }
    
     return (
      
      <>

        <header id='header'>
            <div className='left_header'>
                {/*<div className='top_left_header'>
                <svg className='grom' xmlns="http://www.w3.org/2000/svg" width="40" height="40" viewBox="0 0 40 40" fill="none">
<g clip-path="url(#clip0_387_2278)">
<path d="M20.0003 38.3333C30.1253 38.3333 38.3337 30.125 38.3337 20C38.3337 9.875 30.1253 1.66666 20.0003 1.66666M20.0003 38.3333C9.87533 38.3333 1.66699 30.125 1.66699 20C1.66699 9.875 9.87533 1.66666 20.0003 1.66666M20.0003 38.3333C25.0003 38.3333 26.667 30 26.667 20C26.667 10 25.0003 1.66666 20.0003 1.66666M20.0003 38.3333C15.0003 38.3333 13.3337 30 13.3337 20C13.3337 10 15.0003 1.66666 20.0003 1.66666M3.33366 26.6667H36.667M3.33366 13.3333H36.667" stroke="#190C09" stroke-width="2"/>
</g>
<defs>
<clipPath id="clip0_387_2278">
<rect width="40" height="40" fill="white"/>
</clipPath>
</defs>
</svg>
<NavLink className={"left_header_links header_links"}>
    Тема
</NavLink>
<NavLink className={"left_header_links header_links"}>
Контакти 
<svg xmlns="http://www.w3.org/2000/svg" width="14" height="8" viewBox="0 0 14 8" fill="none">
<path fill-rule="evenodd" clip-rule="evenodd" d="M0 1.06066L6.44646 7.50712L6.51536 7.43821L6.61579 7.53864L13.0622 1.09218L12.0016 0.0315229L6.54689 5.48623L1.06066 0L0 1.06066Z" fill="#190C09"/>
</svg>
</NavLink>
     </div>*/}
                <div className=' down_left_header'>
                <NavLink className={"navlink first_down_link"} to="/">
    Головна
</NavLink>
<NavLink className={"navlink left_header_links"}>
    Навчальні програми
</NavLink>
<NavLink className={"navlink left_header_links"}>
   Про нас
</NavLink>
<NavLink className={"navlink left_header_links"} to="/user-courses">
    Для работи
</NavLink>
                </div>     
                </div>
                <svg className='logo' xmlns="http://www.w3.org/2000/svg" width="127" height="73" viewBox="0 0 127 73" fill="none">
<path d="M74.9043 36.5067C74.9043 36.4277 74.9286 36.3579 74.9772 36.2971C75.0258 36.2364 75.0896 36.1574 75.1686 36.0602C75.3387 35.8658 75.5361 35.6623 75.7609 35.4497C75.9856 35.231 76.2134 35.0214 76.4443 34.8209C76.5901 34.7055 76.6994 34.6478 76.7723 34.6478C76.8452 34.6478 76.9576 34.7055 77.1095 34.8209C77.6016 35.2462 78.0207 35.6593 78.367 36.0602C78.446 36.1513 78.5098 36.2303 78.5584 36.2971C78.613 36.3579 78.6404 36.4277 78.6404 36.5067C78.6404 36.5857 78.613 36.6556 78.5584 36.7163C78.5098 36.7771 78.4429 36.8591 78.3579 36.9623C78.1939 37.1507 78.0025 37.3542 77.7838 37.5729C77.5651 37.7855 77.3403 37.989 77.1095 38.1834C77.0244 38.2563 76.9576 38.3049 76.909 38.3292C76.8604 38.3474 76.8149 38.3565 76.7723 38.3565C76.7298 38.3565 76.6842 38.3474 76.6357 38.3292C76.5931 38.3049 76.5233 38.2563 76.4261 38.1834C76.1831 37.989 75.9553 37.7855 75.7426 37.5729C75.53 37.3542 75.3387 37.1446 75.1686 36.9441C75.0896 36.8469 75.0258 36.771 74.9772 36.7163C74.9286 36.6556 74.9043 36.5857 74.9043 36.5067ZM78.203 48.4166C78.203 48.5989 78.1635 48.7264 78.0845 48.7993C78.0116 48.8661 77.884 48.8996 77.7018 48.8996H75.8246C75.6424 48.8996 75.5118 48.8661 75.4328 48.7993C75.3599 48.7264 75.3235 48.5989 75.3235 48.4166V40.0514C75.3235 39.8692 75.3599 39.7447 75.4328 39.6778C75.5118 39.6049 75.6424 39.5685 75.8246 39.5685H77.7018C77.884 39.5685 78.0116 39.6049 78.0845 39.6778C78.1635 39.7447 78.203 39.8692 78.203 40.0514V48.4166Z" fill="#F87C56"/>
<path d="M83.2786 49.1274C82.6286 49.1274 82.0879 49.0727 81.6566 48.9633C81.2313 48.854 80.8881 48.7234 80.6269 48.5715C80.3717 48.4136 80.1713 48.2708 80.0255 48.1432C79.9161 48.04 79.904 47.9245 79.989 47.797L80.9185 46.421C81.0521 46.2266 81.204 46.2144 81.3741 46.3845C81.5503 46.5546 81.7872 46.7217 82.0849 46.8857C82.3886 47.0437 82.7652 47.1226 83.2148 47.1226C83.6157 47.1226 83.9438 47.0558 84.1989 46.9222C84.4602 46.7824 84.5908 46.582 84.5908 46.3208C84.5908 46.1385 84.5331 45.9897 84.4176 45.8742C84.3022 45.7588 84.0987 45.6404 83.8071 45.5189C83.5155 45.3974 83.1085 45.2425 82.586 45.0541C81.8145 44.7747 81.2313 44.4375 80.8365 44.0427C80.4416 43.6478 80.2442 43.0707 80.2442 42.3113C80.2442 41.6127 80.4355 41.0447 80.8182 40.6073C81.207 40.1699 81.7021 39.851 82.3036 39.6505C82.911 39.4439 83.5428 39.3407 84.1989 39.3407C85.0312 39.3407 85.6144 39.4196 85.9485 39.5776C86.2887 39.7295 86.5074 39.8479 86.6046 39.933C86.6957 40.0119 86.7595 40.1031 86.796 40.2063C86.8749 40.4433 86.9357 40.7561 86.9782 41.1449C87.0025 41.3697 86.9448 41.5185 86.8051 41.5914C86.7322 41.634 86.6168 41.6795 86.4588 41.7281C86.3009 41.7767 86.1429 41.8192 85.985 41.8557C85.827 41.8921 85.7085 41.9104 85.6296 41.9104C85.6053 41.9104 85.5779 41.9043 85.5476 41.8921C85.5233 41.8739 85.4777 41.8344 85.4109 41.7737C85.3319 41.6947 85.1952 41.6036 85.0008 41.5003C84.8125 41.397 84.5513 41.3454 84.2172 41.3454C83.8891 41.3454 83.6127 41.4001 83.3879 41.5094C83.1692 41.6127 83.0599 41.798 83.0599 42.0653C83.0599 42.3569 83.1996 42.5756 83.4791 42.7214C83.7646 42.8672 84.2323 43.0555 84.8824 43.2863C85.3927 43.4625 85.8361 43.6569 86.2128 43.8695C86.5955 44.0761 86.8901 44.3494 87.0967 44.6896C87.3032 45.0238 87.4065 45.4763 87.4065 46.0474C87.4065 46.7885 87.203 47.3869 86.796 47.8425C86.395 48.2921 85.8786 48.6201 85.2469 48.8267C84.6211 49.0271 83.965 49.1274 83.2786 49.1274Z" fill="#F87C56"/>
<path d="M92.4912 49.1274C91.6832 49.1274 90.9937 48.9299 90.4227 48.5351C89.8577 48.1341 89.4264 47.5843 89.1287 46.8857C88.8371 46.181 88.6913 45.37 88.6913 44.4527C88.6913 43.2438 88.904 42.2657 89.3292 41.5185C89.7605 40.7652 90.365 40.2124 91.1426 39.8601C91.9262 39.5077 92.8496 39.3316 93.9127 39.3316C94.2772 39.3316 94.5779 39.3498 94.8149 39.3862C95.0579 39.4227 95.231 39.4561 95.3343 39.4865V36.0875C95.3343 35.9417 95.3646 35.8294 95.4254 35.7504C95.6805 35.4163 96.0541 35.0548 96.5462 34.666C96.6313 34.6053 96.7072 34.5749 96.774 34.5749C96.853 34.5749 96.9289 34.6053 97.0018 34.666C97.2631 34.8665 97.4818 35.0548 97.6579 35.231C97.8402 35.4072 97.9951 35.5803 98.1227 35.7504C98.1834 35.8294 98.2138 35.9417 98.2138 36.0875V48.4804C98.2138 48.5958 98.1743 48.6961 98.0953 48.7811C98.0163 48.8601 97.9161 48.8996 97.7946 48.8996H96.4369C96.3397 48.8996 96.2516 48.8874 96.1726 48.8631C96.0997 48.8327 96.0298 48.7781 95.963 48.6991L95.3981 47.9701H95.3343C95.2553 48.0916 95.1125 48.2465 94.906 48.4348C94.7055 48.6171 94.4109 48.7781 94.0221 48.9178C93.6394 49.0575 93.1291 49.1274 92.4912 49.1274ZM93.3751 46.7946C93.9218 46.7946 94.3501 46.6913 94.66 46.4848C94.9758 46.2782 95.2006 46.017 95.3343 45.7011V41.883C95.2371 41.8223 95.0761 41.7494 94.8513 41.6643C94.6265 41.5793 94.3106 41.5368 93.9036 41.5368C93.1503 41.5368 92.5854 41.7828 92.2087 42.2749C91.8321 42.7669 91.6437 43.4291 91.6437 44.2614C91.6437 45.124 91.8017 45.7619 92.1176 46.175C92.4335 46.5881 92.8527 46.7946 93.3751 46.7946Z" fill="#F87C56"/>
<path d="M105.148 49.1274C103.617 49.1274 102.405 48.7204 101.512 47.9063C100.619 47.0923 100.173 45.8803 100.173 44.2705C100.173 42.6181 100.635 41.3849 101.558 40.5708C102.487 39.7507 103.727 39.3407 105.276 39.3407C106.77 39.3407 107.967 39.7447 108.866 40.5526C109.765 41.3545 110.215 42.5391 110.215 44.1064C110.215 45.7406 109.765 46.986 108.866 47.8425C107.967 48.6991 106.728 49.1274 105.148 49.1274ZM105.239 46.9222C105.908 46.9222 106.412 46.7156 106.752 46.3025C107.092 45.8834 107.262 45.1696 107.262 44.1611C107.262 43.2013 107.089 42.527 106.743 42.1382C106.403 41.7433 105.896 41.5459 105.221 41.5459C104.571 41.5459 104.058 41.7585 103.681 42.1837C103.311 42.6029 103.125 43.2863 103.125 44.234C103.125 45.2242 103.302 45.9198 103.654 46.3208C104.012 46.7217 104.541 46.9222 105.239 46.9222Z" fill="#F87C56"/>
<path d="M120.521 40.5253C120.746 40.2519 121.092 39.9876 121.56 39.7325C122.034 39.4713 122.699 39.3407 123.555 39.3407C124.77 39.3407 125.648 39.6535 126.189 40.2792C126.729 40.905 127 41.8435 127 43.095V48.4166C127 48.5989 126.96 48.7264 126.881 48.7993C126.808 48.8661 126.681 48.8996 126.499 48.8996H124.621C124.439 48.8996 124.309 48.8661 124.23 48.7993C124.157 48.7264 124.12 48.5989 124.12 48.4166V43.5688C124.12 42.8884 123.996 42.4024 123.747 42.1108C123.498 41.8132 123.124 41.6643 122.626 41.6643C122.182 41.6643 121.83 41.7646 121.569 41.965C121.314 42.1594 121.131 42.4237 121.022 42.7578V48.4166C121.022 48.5989 120.983 48.7264 120.904 48.7993C120.831 48.8661 120.703 48.8996 120.521 48.8996H118.644C118.462 48.8996 118.331 48.8661 118.252 48.7993C118.179 48.7264 118.143 48.5989 118.143 48.4166V43.5688C118.143 42.9066 118.006 42.4237 117.733 42.1199C117.459 41.8162 117.101 41.6643 116.657 41.6643C116.238 41.6643 115.901 41.7676 115.646 41.9741C115.391 42.1807 115.193 42.4419 115.053 42.7578V48.4166C115.053 48.5989 115.014 48.7264 114.935 48.7993C114.862 48.8661 114.735 48.8996 114.552 48.8996H112.675C112.493 48.8996 112.362 48.8661 112.283 48.7993C112.21 48.7264 112.174 48.5989 112.174 48.4166V40.7804C112.174 40.6346 112.204 40.5222 112.265 40.4433C112.393 40.2732 112.545 40.1 112.721 39.9239C112.903 39.7477 113.125 39.5594 113.386 39.3589C113.465 39.2981 113.541 39.2678 113.614 39.2678C113.681 39.2678 113.756 39.2981 113.842 39.3589C114.097 39.5594 114.315 39.7507 114.498 39.933C114.68 40.1152 114.841 40.3035 114.981 40.4979H115.026C115.233 40.2367 115.555 39.9785 115.992 39.7234C116.429 39.4682 117.055 39.3407 117.869 39.3407C118.647 39.3407 119.224 39.4591 119.601 39.6961C119.977 39.9269 120.284 40.2033 120.521 40.5253Z" fill="#F87C56"/>
<path d="M86.3116 72.175C85.2995 72.175 84.494 71.8927 83.895 71.3282C83.3029 70.7567 83.0068 69.9719 83.0068 68.9735C83.0068 68.4572 83.0998 67.9787 83.2857 67.538C83.4785 67.0905 83.8089 66.6981 84.2771 66.3607C84.7522 66.0165 85.4131 65.7411 86.26 65.5345C87.1137 65.328 88.2015 65.2075 89.5234 65.1731C89.5234 64.5465 89.3651 64.0956 89.0483 63.8202C88.7385 63.5448 88.2704 63.4071 87.6438 63.4071C87.0793 63.4071 86.618 63.5035 86.26 63.6963C85.9019 63.889 85.6782 64.0199 85.5887 64.0887C85.4923 64.1713 85.3752 64.2023 85.2375 64.1816C85.0517 64.1541 84.852 64.1059 84.6386 64.0371C84.432 63.9682 84.2083 63.889 83.9673 63.7995C83.9329 63.7858 83.895 63.7651 83.8537 63.7376C83.8124 63.7031 83.7883 63.655 83.7814 63.593C83.7814 63.5586 83.7848 63.5173 83.7917 63.4691C83.7986 63.414 83.8158 63.3176 83.8434 63.1799C83.8709 63.0422 83.9122 62.8735 83.9673 62.6739C84.0224 62.4673 84.074 62.3089 84.1222 62.1988C84.1842 62.0817 84.2874 61.9716 84.432 61.8683C84.5491 61.7719 84.9036 61.617 85.4957 61.4036C86.0878 61.1902 86.945 61.0834 88.0672 61.0834C91.1586 61.0834 92.7042 62.5878 92.7042 65.5965V71.4418C92.7042 71.5726 92.6664 71.6862 92.5906 71.7826C92.5218 71.8721 92.4013 71.9168 92.2292 71.9168H90.6801C90.5699 71.9168 90.4701 71.9031 90.3806 71.8755C90.298 71.8411 90.2188 71.7791 90.143 71.6896L89.5028 70.8635H89.4305C89.1138 71.3247 88.6869 71.6587 88.1499 71.8652C87.6197 72.0718 87.007 72.175 86.3116 72.175ZM87.4476 69.7791C88.081 69.7791 88.5595 69.6173 88.8831 69.2937C89.2067 68.9701 89.4132 68.6362 89.5028 68.2919V67.1456C88.5457 67.1456 87.8263 67.2213 87.3443 67.3728C86.8693 67.5174 86.5526 67.7102 86.3942 67.9511C86.2359 68.1852 86.1567 68.44 86.1567 68.7154C86.1567 69.1147 86.2806 69.3935 86.5285 69.5519C86.7763 69.7033 87.0827 69.7791 87.4476 69.7791Z" fill="#56D2F8"/>
<path d="M97.5168 70.9564C97.1381 70.3574 96.7629 69.6689 96.3911 68.8909C96.0262 68.106 95.6923 67.3143 95.3894 66.5156C95.0933 65.7101 94.8558 64.9769 94.6768 64.3159C94.4978 63.6481 94.4083 63.1351 94.4083 62.7771C94.4083 62.6394 94.4461 62.5258 94.5219 62.4363C94.8386 62.0439 95.2895 61.6342 95.8747 61.2074C95.978 61.1385 96.071 61.1041 96.1536 61.1041C96.2431 61.1041 96.336 61.1385 96.4324 61.2074C96.7491 61.4346 97.0142 61.648 97.2276 61.8477C97.4479 62.0473 97.6338 62.2435 97.7853 62.4363C97.861 62.5258 97.8989 62.6532 97.8989 62.8184V62.7668C97.8989 63.2143 97.9781 63.803 98.1364 64.5328C98.3017 65.2557 98.5254 66.0199 98.8077 66.8255C99.09 67.631 99.417 68.3711 99.7888 69.0458H99.8405C100.15 68.5364 100.426 67.9856 100.667 67.3935C100.908 66.8014 101.111 66.2093 101.276 65.6172C101.441 65.025 101.565 64.4811 101.648 63.9854C101.737 63.4897 101.782 63.0869 101.782 62.7771V62.8184C101.782 62.6532 101.816 62.5258 101.885 62.4363C102.044 62.2435 102.23 62.0473 102.443 61.8477C102.656 61.648 102.915 61.4346 103.218 61.2074C103.314 61.1385 103.403 61.1041 103.486 61.1041C103.582 61.1041 103.672 61.1385 103.755 61.2074C104.064 61.4346 104.326 61.648 104.539 61.8477C104.753 62.0473 104.935 62.2435 105.087 62.4363C105.156 62.5258 105.19 62.6463 105.19 62.7978C105.183 63.1696 105.087 63.6928 104.901 64.3675C104.715 65.0354 104.464 65.7721 104.147 66.5776C103.837 67.3762 103.489 68.1646 103.104 68.9426C102.718 69.7137 102.326 70.385 101.927 70.9564C101.686 71.3075 101.469 71.5554 101.276 71.7C101.083 71.8446 100.749 71.9168 100.274 71.9168H99.1795C98.6838 71.9168 98.3395 71.848 98.1468 71.7103C97.9609 71.5726 97.7509 71.3213 97.5168 70.9564ZM94.4083 62.7668V62.7771C94.4083 62.7909 94.4083 62.8047 94.4083 62.8184V62.7668ZM105.19 62.7771V62.8184V62.7978C105.19 62.7909 105.19 62.784 105.19 62.7771Z" fill="#56D2F8"/>
<path d="M112.285 72.175C111.121 72.175 110.109 71.9891 109.249 71.6174C108.388 71.2387 107.72 70.6535 107.245 69.8617C106.77 69.0699 106.533 68.0475 106.533 66.7945C106.533 64.9149 107.015 63.4932 107.978 62.5293C108.942 61.5654 110.223 61.0834 111.82 61.0834C112.86 61.0834 113.738 61.2693 114.454 61.6411C115.177 62.0129 115.724 62.5258 116.096 63.1799C116.474 63.834 116.664 64.581 116.664 65.4209C116.664 66.2402 116.454 66.8117 116.034 67.1353C115.614 67.4589 115.001 67.6207 114.195 67.6207H109.765C109.82 68.3298 110.078 68.8703 110.54 69.2421C111.008 69.6138 111.641 69.7997 112.44 69.7997C113.073 69.7997 113.579 69.7068 113.958 69.5209C114.344 69.3281 114.612 69.1767 114.764 69.0665C114.894 68.9701 115.042 68.9254 115.208 68.9322C115.338 68.9322 115.524 68.9598 115.765 69.0149C116.013 69.0631 116.258 69.156 116.499 69.2937C116.671 69.3832 116.757 69.5071 116.757 69.6655C116.757 69.755 116.739 69.8824 116.705 70.0476C116.684 70.144 116.65 70.2851 116.602 70.471C116.554 70.65 116.492 70.8187 116.416 70.9771C116.375 71.0528 116.309 71.1251 116.22 71.1939C116.082 71.2903 115.858 71.4177 115.548 71.576C115.245 71.7344 114.829 71.8721 114.299 71.9891C113.769 72.1131 113.097 72.175 112.285 72.175ZM109.744 65.5035H113.111C113.462 65.5035 113.638 65.3418 113.638 65.0182C113.638 64.5293 113.483 64.1197 113.173 63.7892C112.87 63.4518 112.412 63.2832 111.8 63.2832C111.132 63.2832 110.639 63.4966 110.323 63.9235C110.013 64.3503 109.82 64.877 109.744 65.5035Z" fill="#56D2F8"/>
<path d="M20.1928 68.8511C19.2524 67.0713 18.29 64.8191 17.3058 62.0945C16.3216 59.348 15.3812 56.4037 14.4845 53.2616C13.5878 50.1195 12.7895 47.0214 12.0896 43.9672C11.3897 40.8911 10.832 38.1006 10.4165 35.5957C10.0228 33.0909 8.77637 21.8966 8.77637 20.4903C8.77637 19.9191 8.89666 19.4686 9.13724 19.139C9.64027 18.4359 10.6263 16.2243 11.3481 15.4772C12.0917 14.7082 14.5282 12.8057 15.5343 11.9707C15.8624 11.7071 16.4529 11.5752 16.4529 11.5752C16.4529 11.5752 17.0215 11.7071 17.3058 11.9707C18.3338 12.8057 18.7197 14.5279 20.1928 15.4772C21.349 16.2223 22.6574 15.4772 23.9662 16.3966C25.2751 17.316 26.0267 17.7603 26.5388 19.139C27 20.3808 26.5388 22.5329 26.5388 22.5329L25.934 29.2677V43.7036C25.934 43.7036 26.1768 49.1181 26.5388 52.5695C26.8422 55.4633 26.8197 57.733 27.5415 59.9522H27.7383C28.9194 57.0079 30.0238 53.8109 31.0518 50.3612C32.0797 46.9115 33.042 43.385 33.9388 39.7815C34.8355 36.178 35.7431 29.839 36.5305 26.4333C36.7711 25.4445 37.1757 24.7634 37.7443 24.3898C38.3348 23.9943 39.2534 23.7966 40.5001 23.7966L44.3712 26.631C45.4867 26.631 48.6798 25.7411 49.2266 26.0707C49.7734 26.4003 52.1026 27.2792 52.3432 28.3778C52.9119 30.9266 52.1193 33.207 52.3432 36.2879C52.5858 39.6254 52.6984 41.5484 53.1973 44.8571C53.6978 48.1771 54.1554 49.9886 54.9021 53.2616C55.508 55.9173 56.1488 58.1944 56.8705 59.9522H57.0674C57.9859 57.8648 58.8061 55.4039 59.5279 52.5695C60.2715 49.735 60.8948 46.8456 61.3978 43.9013C61.9227 40.935 62.3164 38.1885 62.5789 35.6616C62.8632 33.1348 63.0053 31.1463 63.0053 29.6962V29.828C63.0053 29.1908 63.1147 28.7074 63.3334 28.3778C64.3832 26.9276 65.8267 25.4006 67.6639 23.7966C68.0138 23.5329 68.32 23.4011 68.5825 23.4011C68.823 23.4011 69.1074 23.5329 69.4354 23.7966C70.4196 24.6315 71.2617 25.4335 71.9615 26.2025C72.6614 26.9496 73.2628 27.6747 73.7659 28.3778C73.9846 28.7294 74.0939 29.2018 74.0939 29.795C74.0939 31.6407 73.8862 33.8599 73.4706 36.4527C73.0551 39.0454 72.4864 41.8139 71.7647 44.7582C71.0648 47.6806 70.2665 50.6029 69.3698 53.5253C68.495 56.4476 67.5654 59.1832 66.5812 61.732C65.6189 64.2588 64.6675 66.4011 63.7271 68.1589C62.9397 69.6311 62.2289 70.6308 61.5947 71.1582C60.9823 71.6636 60.0965 71.9162 58.9373 71.9162H54.9021C53.9179 71.9162 53.0868 71.6965 52.4088 71.2571C51.7527 70.8176 51.0638 69.9936 50.342 68.7852C49.7952 67.8623 49.161 66.5659 48.4392 64.896C47.7394 63.2261 47.0286 61.4134 46.3068 59.4578C45.6069 57.4803 44.9727 55.5687 44.404 53.723C43.8354 51.8773 43.4198 50.3173 43.1574 49.0429C42.8512 47.5927 42.5887 46.1974 42.37 44.8571C42.1732 43.4948 42.0529 42.4292 42.0092 41.6601H41.7467C41.703 42.4292 41.5608 43.4948 41.3202 44.8571C41.1015 46.1974 40.8281 47.5927 40.5001 49.0429C40.172 50.4931 39.9614 52.297 39.6273 54.3839C39.2793 56.5573 38.7547 59.9522 38.7547 59.9522C38.7547 59.9522 38.0394 63.292 37.3832 64.896C36.7271 66.5 36.1147 67.7744 35.5461 68.7192C34.9118 69.7959 34.2119 70.5979 33.4465 71.1252C32.7028 71.6526 31.664 71.9162 30.3298 71.9162H24.6545C23.4953 71.9162 22.6205 71.7075 22.03 71.29C21.4395 70.8725 20.8271 70.0596 20.1928 68.8511ZM74.0939 29.6962V29.828V29.795C74.0939 29.7511 74.0939 29.7181 74.0939 29.6962ZM8.77637 20.4574V20.4903V20.5892V20.4574Z" fill="#F87C56"/>
<path d="M55.4084 70.2415C54.8979 69.269 54.3755 68.0384 53.8412 66.5497C53.3069 65.049 52.7964 63.4403 52.3096 61.7235C51.8228 60.0067 51.3895 58.3139 51.0096 56.6451C50.6296 54.9644 50.3269 53.4397 50.1013 52.071C49.8876 50.7024 49.2109 44.5859 49.2109 43.8176C49.2109 43.5054 49.2762 43.2593 49.4068 43.0792C49.6799 42.695 50.2152 41.4866 50.607 41.0784C51.0107 40.6582 52.3334 39.6187 52.8795 39.1625C53.0576 39.0184 53.3782 38.9464 53.3782 38.9464C53.3782 38.9464 53.6869 39.0184 53.8412 39.1625C54.3992 39.6187 54.6088 40.5597 55.4084 41.0784C56.0361 41.4855 56.7463 41.0784 57.4569 41.5808C58.1674 42.0831 58.5754 42.3259 58.8534 43.0792C59.1037 43.7577 58.8534 44.9336 58.8534 44.9336L58.5251 48.6134V56.5011C58.5251 56.5011 58.6569 59.4595 58.8534 61.3453C59.0181 62.9265 59.0059 64.1666 59.3977 65.3792H59.5046C60.1457 63.7705 60.7453 62.0236 61.3033 60.1388C61.8613 58.2539 62.3837 56.327 62.8705 54.3581C63.3573 52.3892 63.85 48.9256 64.2775 47.0647C64.4081 46.5245 64.6277 46.1523 64.9364 45.9482C65.257 45.7321 65.7556 45.6241 66.4324 45.6241L68.5339 47.1728C69.1394 47.1728 70.8728 46.6865 71.1696 46.8666C71.4665 47.0467 72.7309 47.5269 72.8615 48.1272C73.1702 49.5198 72.74 50.7658 72.8615 52.4492C72.9932 54.2728 73.0544 55.3235 73.3251 57.1314C73.5969 58.9454 73.8453 59.9352 74.2506 61.7235C74.5795 63.1746 74.9274 64.4188 75.3192 65.3792H75.4261C75.9247 64.2387 76.3699 62.894 76.7617 61.3453C77.1654 59.7966 77.5038 58.2179 77.7769 56.6091C78.0618 54.9884 78.2755 53.4877 78.418 52.107C78.5724 50.7264 78.6495 49.6399 78.6495 48.8475V48.9196C78.6495 48.5714 78.7089 48.3073 78.8276 48.1272C79.3975 47.3348 80.1811 46.5005 81.1784 45.6241C81.3684 45.48 81.5346 45.408 81.6771 45.408C81.8077 45.408 81.962 45.48 82.1401 45.6241C82.6744 46.0803 83.1315 46.5185 83.5115 46.9387C83.8914 47.3468 84.2179 47.743 84.491 48.1272C84.6097 48.3193 84.6691 48.5774 84.6691 48.9016C84.6691 49.91 84.5563 51.1226 84.3307 52.5392C84.1051 53.9559 83.7964 55.4686 83.4046 57.0773C83.0247 58.6741 82.5913 60.2708 82.1045 61.8676C81.6296 63.4643 81.125 64.959 80.5907 66.3517C80.0683 67.7323 79.5519 68.9028 79.0413 69.8633C78.6139 70.6677 78.228 71.2139 77.8837 71.502C77.5513 71.7782 77.0704 71.9162 76.4412 71.9162H74.2506C73.7164 71.9162 73.2652 71.7962 72.8971 71.5561C72.5409 71.316 72.167 70.8658 71.7752 70.2054C71.4783 69.7012 71.134 68.9929 70.7422 68.0805C70.3623 67.168 69.9764 66.1776 69.5846 65.1091C69.2047 64.0286 68.8604 62.9841 68.5517 61.9756C68.243 60.9671 68.0174 60.1148 67.8749 59.4184C67.7087 58.6261 67.5662 57.8637 67.4475 57.1314C67.3406 56.387 67.2753 55.8047 67.2516 55.3846H67.1091C67.0854 55.8047 67.0082 56.387 66.8776 57.1314C66.7589 57.8637 66.6105 58.6261 66.4324 59.4184C66.2543 60.2108 66.14 61.1965 65.9586 62.3367C65.7697 63.5242 65.4849 65.3792 65.4849 65.3792C65.4849 65.3792 65.0966 67.204 64.7404 68.0805C64.3842 68.9569 64.0518 69.6532 63.7431 70.1694C63.3988 70.7577 63.0188 71.1959 62.6033 71.484C62.1996 71.7722 61.6356 71.9162 60.9114 71.9162H57.8305C57.2012 71.9162 56.7263 71.8022 56.4058 71.5741C56.0852 71.346 55.7528 70.9018 55.4084 70.2415ZM84.6691 48.8475V48.9196V48.9016C84.6691 48.8776 84.6691 48.8595 84.6691 48.8475ZM49.2109 43.7996V43.8176V43.8716V43.7996Z" fill="#56D2F8"/>
<path d="M6.28809 22.1977C6.28809 14.4049 12.4596 8.27917 16.5523 8.80496C19.7055 9.21006 19.1191 12.5137 22.1509 13.4702C24.236 14.128 23.997 13.6025 26.8165 15.0254C29.6359 16.4483 28.6827 24.6387 28.6827 24.6387" stroke="#F87C56" stroke-width="2" stroke-linecap="round"/>
<path d="M3.17773 18.7374C3.17773 10.9446 9.39846 5.04347 17.7964 4.76854C20.9738 4.66452 20.3633 8.47731 23.3951 9.43378C25.4802 10.0916 26.7964 9.84592 29.6158 11.2688C32.4352 12.6917 32.4151 20.5999 32.4151 20.5999" stroke="#F87C56" stroke-linecap="round"/>
<path d="M1 16.5516C1 8.75881 10.02 1.27642 18.418 1.00149C21.5954 0.897471 21.2959 6.26424 24.3277 7.22072C26.4128 7.87854 28.6822 7.84299 31.7926 8.46487C34.9029 9.08675 35.214 18.7285 35.214 18.7285" stroke="#F87C56" stroke-width="0.5" stroke-linecap="round"/>
</svg>

{userDiv}
           
        </header>
        <footer>

        </footer>


       



        
        </>

    );
  }
}

export default Layout;

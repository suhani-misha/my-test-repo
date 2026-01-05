

class Leftmenus extends HTMLElement{
  connectedCallback(){
 this.innerHTML=`   <aside class="sidebar-wrapper" data-simplebar="true">
  <div class="sidebar-header">
    <div class="logo-icon">
      <img src="assets/images/logo-icon.png" class="logo-img" alt="">
    </div>
    <div class="logo-name flex-grow-1">
      <h5 class="mb-0">DesAlpes</h5>
    </div>
    <div class="sidebar-close">
      <span class="material-icons-outlined">close</span>
    </div>
  </div>
  <div class="sidebar-nav">
      <!--navigation-->
      <ul class="metismenu" id="sidenav">
        <li>
          <a href="index.html">
            <div class="parent-icon"><svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-house-door" viewBox="0 0 16 16">
              <path d="M8.354 1.146a.5.5 0 0 0-.708 0l-6 6A.5.5 0 0 0 1.5 7.5v7a.5.5 0 0 0 .5.5h4.5a.5.5 0 0 0 .5-.5v-4h2v4a.5.5 0 0 0 .5.5H14a.5.5 0 0 0 .5-.5v-7a.5.5 0 0 0-.146-.354L13 5.793V2.5a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1.293zM2.5 14V7.707l5.5-5.5 5.5 5.5V14H10v-4a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5v4z"/>
            </svg>
            </div>
            <div class="menu-title">Home</div>
          </a>
        </li>
        <li>
          <a href="teams.html">
            <div class="parent-icon"><svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-user-plus"><path d="M16 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path><circle cx="8.5" cy="7" r="4"></circle><line x1="20" y1="8" x2="20" y2="14"></line><line x1="23" y1="11" x2="17" y2="11"></line></svg>
            </div>
            <div class="menu-title">Your Team</div>
          </a>
        </li>

        <li>
          <a href="matrix.html">
            <div class="parent-icon"><svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-grid"><rect x="3" y="3" width="7" height="7"></rect><rect x="14" y="3" width="7" height="7"></rect><rect x="14" y="14" width="7" height="7"></rect><rect x="3" y="14" width="7" height="7"></rect></svg>
            </div>
            <div class="menu-title">Your Matrix</div>
          </a>
        </li>
        <li>
          <a href="finance.html">
            <div class="parent-icon"><svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-coin" viewBox="0 0 16 16">
              <path d="M5.5 9.511c.076.954.83 1.697 2.182 1.785V12h.6v-.709c1.4-.098 2.218-.846 2.218-1.932 0-.987-.626-1.496-1.745-1.76l-.473-.112V5.57c.6.068.982.396 1.074.85h1.052c-.076-.919-.864-1.638-2.126-1.716V4h-.6v.719c-1.195.117-2.01.836-2.01 1.853 0 .9.606 1.472 1.613 1.707l.397.098v2.034c-.615-.093-1.022-.43-1.114-.9zm2.177-2.166c-.59-.137-.91-.416-.91-.836 0-.47.345-.822.915-.925v1.76h-.005zm.692 1.193c.717.166 1.048.435 1.048.91 0 .542-.412.914-1.135.982V8.518z"/>
              <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
              <path d="M8 13.5a5.5 5.5 0 1 1 0-11 5.5 5.5 0 0 1 0 11m0 .5A6 6 0 1 0 8 2a6 6 0 0 0 0 12"/>
            </svg>
            </div>
            <div class="menu-title">Your Finance</div>
          </a>
        </li>

        <li>
          <a href="#">
            <div class="parent-icon"><svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-wallet" viewBox="0 0 16 16">
              <path d="M0 3a2 2 0 0 1 2-2h13.5a.5.5 0 0 1 0 1H15v2a1 1 0 0 1 1 1v8.5a1.5 1.5 0 0 1-1.5 1.5h-12A2.5 2.5 0 0 1 0 12.5zm1 1.732V12.5A1.5 1.5 0 0 0 2.5 14h12a.5.5 0 0 0 .5-.5V5H2a2 2 0 0 1-1-.268M1 3a1 1 0 0 0 1 1h12V2H2a1 1 0 0 0-1 1"/>
            </svg>
            </div>
            <div class="menu-title">Crypto Wallet</div>
          </a>
        </li>
        <li>
          <a href="support.html">
            <div class="parent-icon"><svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-info-circle" viewBox="0 0 16 16">
              <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
              <path d="m8.93 6.588-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533zM9 4.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0"/>
            </svg>
            </div>
            <div class="menu-title">Support</div>
          </a>
        </li>
           <li>
          <a href="training.html">
            <div class="parent-icon"><i class="bi bi-file-earmark-play font-20"></i>
            </div>
            <div class="menu-title">Training</div>
          </a>
        </li>
<li class="menu-label pb-0">Administration</li>
        <li>
          <a href="members.html">
            <div class="parent-icon"><svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-people" viewBox="0 0 16 16">
              <path d="M15 14s1 0 1-1-1-4-5-4-5 3-5 4 1 1 1 1zm-7.978-1L7 12.996c.001-.264.167-1.03.76-1.72C8.312 10.629 9.282 10 11 10c1.717 0 2.687.63 3.24 1.276.593.69.758 1.457.76 1.72l-.008.002-.014.002zM11 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4m3-2a3 3 0 1 1-6 0 3 3 0 0 1 6 0M6.936 9.28a6 6 0 0 0-1.23-.247A7 7 0 0 0 5 9c-4 0-5 3-5 4q0 1 1 1h4.216A2.24 2.24 0 0 1 5 13c0-1.01.377-2.042 1.09-2.904.243-.294.526-.569.846-.816M4.92 10A5.5 5.5 0 0 0 4 13H1c0-.26.164-1.03.76-1.724.545-.636 1.492-1.256 3.16-1.275ZM1.5 5.5a3 3 0 1 1 6 0 3 3 0 0 1-6 0m3-2a2 2 0 1 0 0 4 2 2 0 0 0 0-4"/>
            </svg>
            </div>
            <div class="menu-title">Members</div>
          </a>
        </li>  
        
        <li>
          <a href="fee-management.html">
            <div class="parent-icon">
            <i class="bi bi-cash font-20"></i>
            </div>
            <div class="menu-title">Fee Mgmt.</div>
          </a>
        </li>

         <li>
          <a href="commission.html">
            <div class="parent-icon">
            <i class="bi bi-cash-coin font-20"></i>
            </div>
            <div class="menu-title">Commission Mgmt.</div>
          </a>
        </li>

           <li>
          <a href="transaction.html">
            <div class="parent-icon"><i class="bi bi-card-list font-20"></i>
            </div>
            <div class="menu-title">Transaction/Payments </div>
          </a>
        </li>  
        
        <!--<li>
          <a href="payments.html">
            <div class="parent-icon"><i class="bi bi-credit-card font-20"></i>
            </div>
            <div class="menu-title">Payments</div>
          </a>
        </li>-->
          <li class="d-none">
          <a href="javascript:;" class="has-arrow">
            <div class="parent-icon"><i class="material-icons-outlined">home</i>
            </div>
            <div class="menu-title">Dashboard</div>
          </a>
          <ul>
            <li><a href="index.html"><i class="material-icons-outlined">arrow_right</i>Analysis</a>
            </li>
            <li><a href="index2.html"><i class="material-icons-outlined">arrow_right</i>eCommerce</a>
            </li>
          </ul>
        </li> 
<li class="mm-active">
          <a href="javascript:;" class="has-arrow">
            <div class="parent-icon"><i class="bi bi-gear font-20"></i>
            </div>
            <div class="menu-title">Settings</div>
          </a>
          <ul>
            <li><a href="company-setting.html"><i class="material-icons-outlined">arrow_right</i>Company Setting</a>
            </li>
            <li><a href="application-setting.html"><i class="material-icons-outlined">arrow_right</i>  Application Setting</a>
            </li>

             <li><a href="smtp.html"><i class="material-icons-outlined">arrow_right</i>SMTP Setting</a>
            </li>
            <li><a href="taxation-setting.html"><i class="material-icons-outlined">arrow_right</i>  Taxation Setting</a>
            </li>
          </ul>
        </li> 
       
       </ul>
      <!--end navigation-->
  </div>
</aside>`

}
}
customElements.define('sidebar-barrrrrr' , Leftmenus);
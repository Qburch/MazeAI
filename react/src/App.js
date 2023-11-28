import logo from './logo.svg';
import './App.css';
import React, {useState, useEffect} from "react";
import { Routes, Route, useLocation } from "react-router-dom";
import Home from "./components/HomePage";
import FirstPage from "./components/FirstPage";


function App() {
  const [activeNav, setActiveNav] = useState("");

  const location = useLocation();

  useEffect(() => {
    setActiveNav(location.pathname);
  },[location]);


  return (
    <React.Fragment>
      <header className='pt-3 pb-2 bg-light border-bottom'>
        <div className='px-3 d-flex flex-wrap'>
          <a href='/' className='d-flex align-items-center link-body-emphasis text-decoration-none icon-link-hover my-logo' 
            >
            <svg className='bi me-3'width={32} height={32}>
              <image href='https://logosandtypes.com/wp-content/uploads/2020/07/paulq.svg' width={32} height={32}/>
            </svg>
            <span className='fs-4 fw-bolder text-dark-emphasis'>Website Name</span>
          </a>
        </div>
      </header>
      <nav className='navbar navbar-expand bg-light border-bottom py-0 px-sm-4 mb-2 my-navbar'>
        <ul className='navbar-nav'>
        <li className={(activeNav === "/" ? 'active ': '') + 'nav-item rounded mx-lg-3 px-sm-2 px-1'}>
            <a href="/" className="nav-link fw-medium">Home</a>
          </li>
          <li className={(activeNav === "/first" ? 'active ': '') + 'nav-item rounded mx-lg-3 px-sm-2 px-1'}>
            <a href="/first" className="nav-link fw-medium">First</a>
          </li>
        </ul>
      </nav>

      <main role="main">
        <Routes>
          <Route path="/" element={<Home/>} />
          <Route path="/first" element={<FirstPage  />} />
        </Routes>
        <button onClick={()=>console.log("clicked")}>Click Me</button>
      </main>
      <footer className="container">
          &copy; Some Footer 2023-2024
          <div className='d-flex'>
            Created with <img src={logo} className='React-logo-footer pb-4 ms--2' alt="react-logo" />
          </div>
      </footer>
    </React.Fragment>
  );
}

export default App;

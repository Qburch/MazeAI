import logo from './logo.svg';
import './App.css';
import React from "react";
import { Routes, Route } from "react-router-dom";
import FirstPage from "./components/FirstPage";


function App() {
  return (
    <React.Fragment>
      <header className='pt-3 pb-3 bg-light border-bottom'>
        <div className='px-3 d-flex flex-wrap'>
          <a href='/' className='d-flex align-items-center link-body-emphasis text-decoration-none icon-link-hover my-logo' 
            >
            <span className='fs-4 fw-bolder text-dark-emphasis'>Q-Learning Maze AI</span>
          </a>
        </div>
      </header>

      <main role="main" className="bg-dark-subtle">
        <Routes>
          <Route path="/" element={<FirstPage  />} />
        </Routes>
        <button onClick={()=>console.log("clicked")}>Click Me</button>
      </main>
      <footer className="container">
          Created by Quinn Burch 2023-2024
          <div className='d-flex'>
            Made with <img src={logo} className='React-logo-footer pb-4 ms--2' alt="react-logo" />
          </div>
      </footer>
    </React.Fragment>
  );
}

export default App;

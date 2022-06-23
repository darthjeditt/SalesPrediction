import './App.css';

import {Home} from './Home';
import {Predict} from './Predict';
import {Navigation} from './Navigation';

import {BrowserRouter, Route, Routes} from 'react-router-dom';
import React from 'react';

function App() {
  return (
    <BrowserRouter>
    <div className="container">
     <h3 className="m-3 d-flex justify-content-center">
       Predicting Sales
     </h3>

     <Navigation/>

     <Routes>
       <Route path='/' element={<Home/>} exact/>
       <Route path='/predict' element={<Predict/>}/>
     </Routes>
    </div>
    </BrowserRouter>
  );
}

export default App;

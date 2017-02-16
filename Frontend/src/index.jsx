import React from 'react';
import { render } from 'react-dom';
import { Provider } from 'react-redux'
import { createStore, applyMiddleware, compose } from 'redux';
import ReduxThunk from 'redux-thunk';
import shortenerApp from './reducers';
import App from './components/app';
require('stylesheets/main.scss');

let store = createStore(shortenerApp, applyMiddleware(ReduxThunk));

render(
    <Provider store={store}>
        <App />
    </Provider>,
    document.getElementById('root')
);



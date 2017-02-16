import React, { PropTypes } from 'react';
import { render } from 'react-dom';
import { connect } from 'react-redux';
import { setUrl, setPath, clearPath, createShortenedUrl, clearForm } from '../../actions';
import { FormState } from '../../reducers';
import UrlForm from './url-form';
require('../../stylesheets/main.scss');

const AppComponent = ({formState, clearForm, setUrl, setCustomPath, url, customPath, createShortenedUrl, shortenedUrl, error}) => {
    switch (formState) {
        case FormState.FORM:
            return (
                <div>
                    <h1>Url Shortner</h1>
                    <UrlForm url={url} clearForm={clearForm} customPath={customPath} setUrl={setUrl} setCustomPath={setCustomPath} createShortenedUrl={createShortenedUrl} />
                </div>
            );
        case FormState.SHORTENING:
            return (<div>please wait...</div>);
        case FormState.SHORTENED:
            return (<div><h1><a style={{ color: '#44C33E' }} href={shortenedUrl}>{shortenedUrl}</a></h1>
                <div>
                    <button onClick={() => { clearForm() }}>back</button>
                </div>
            </div>);
        case FormState.SHORTENING_FAILED:
            return (<div><h1 style={{ color: '#DB5349' }}>{error}</h1>
                <div>
                    <button onClick={() => { clearForm() }}>back</button>
                </div>
            </div>
            );
    }

};

const mapStateToProps = (state) => {
    return {
        url: state.url,
        customPath: state.customPath,
        formState: state.state,
        shortenedUrl: state.shortenedUrl,
        error: state.error
    }
};
const mapDispatchToProps = (dispatch) => {
    return {
        setUrl: (url) => dispatch(setUrl(url)),
        setCustomPath: (path) => dispatch(setPath(path)),
        createShortenedUrl: () => dispatch(createShortenedUrl()),
        clearForm: () => dispatch(clearForm())
    }
};

const App = connect(
    mapStateToProps,
    mapDispatchToProps
)(AppComponent);

export default App;
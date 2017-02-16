import React, { PropTypes } from 'react';
import { render } from 'react-dom';
import { connect } from 'react-redux';
import '../../../stylesheets/main.scss';

const UrlForm = ({url,clearForm, customPath, setUrl, setCustomPath, createShortenedUrl }) => {
    return (
        <span>
            <div className='input-row'>
                <span>Target url:</span>
                <input onChange={(e) => setUrl(e.target.value)} placeholder='target url' value={url? url: ''} />
            </div>
            <div className='input-row'>
                <span>Custom Path (optional):</span>
                <input onChange={(e) => setCustomPath(e.target.value)} placeholder='custom path' value={customPath? customPath: ''} />
            </div>
            <div>
                <button onClick={() => { createShortenedUrl() }}>shorten url</button>
                <button onClick={() => { clearForm() }}>reset</button>
            </div>
        </span>
    );
};

export default UrlForm;
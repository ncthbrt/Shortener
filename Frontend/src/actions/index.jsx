export const SET_URL = 'SET_URL';
export const SET_PATH = 'SET_PATH';
export const CLEAR_FORM = 'CLEAR_FORM';
export const SHORTENING_URL = 'SHORTENING_URL';
export const URL_SHORTENED = 'URL_SHORTENED';
export const URL_SHORTENING_FAIL = 'URL_SHORTENING_FAIL';
import fetch from 'isomorphic-fetch';

export const setUrl = (url) => ({ type: SET_URL, url: url });
export const setPath = (path) => ({ type: SET_PATH, path: path });
export const clearPath = () => ({ type: SET_PATH, path: undefined });
export const clearForm = () => ({ type:CLEAR_FORM});
export const urlShortend = (shortenedUrl) => ({ type: URL_SHORTENED, shortenedUrl: shortenedUrl });


export const createShortenedUrl = () => (dispatch, getState) => {
    const headers = new Headers({
        'Content-Type': 'application/json',
        'Accept': 'application/json',
    });
    const { url, customPath } = getState();
    let options = { method: 'POST', body: JSON.stringify({ url: url }), headers: headers };    
    
    let fetchRequest = customPath
        ? fetch( SHORTENAPI_URL + `api/shortener/${encodeURIComponent(customPath)}`, options)
        : fetch( SHORTENAPI_URL + 'api/shortener/', options);

    dispatch({ type: SHORTENING_URL });

    fetchRequest.then(response => {
        if (!response.ok) {
            return response.text();
        }else{
            return response.json();
        }        
    }, error => {
        console.log(error);
        dispatch({ type: URL_SHORTENING_FAIL, error: error });
    }).then(json => {
        console.log(json);
        if (json && json.url) {
            dispatch(urlShortend(json.url));
        }else{
            dispatch({type: URL_SHORTENING_FAIL, error: json });
        }
    });



}
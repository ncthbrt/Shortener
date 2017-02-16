import { SET_PATH, SET_URL, CLEAR_FORM, SHORTENING_URL, URL_SHORTENED, URL_SHORTENING_FAIL } from '../actions'

export const FormState = {
    FORM: { name: 'FORM' },
    SHORTENING: { name: 'SHORTENING' },
    SHORTENED: { name: 'SHORTENED' },
    SHORTENING_FAILED: { name: 'SHORTENING_FAILED' }
};

const initialState = {
    url: undefined,
    customPath: undefined,
    state: FormState.FORM
}

export default function shortenerApp(state = initialState, action) {
    switch (action.type) {
        case SET_PATH:
            return Object.assign({}, state, { customPath: action.path });
        case SET_URL:
            return Object.assign({}, state, { url: action.url })
        case CLEAR_FORM:
            return initialState;
        case SHORTENING_URL:
            return Object.assign({}, state, { state: FormState.SHORTENING });
        case URL_SHORTENED:
            return Object.assign({}, state, { state: FormState.SHORTENED, shortenedUrl: action.shortenedUrl });
        case URL_SHORTENING_FAIL:
            return Object.assign({}, state, { state: FormState.SHORTENING_FAILED, error: action.error });
        default:
            return state;
    }
}

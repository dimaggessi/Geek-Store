import {ActionReducer, MetaReducer} from '@ngrx/store';

// meta reducer is applied in every state change
export function localStorageSyncReducer(reducer: ActionReducer<any>): ActionReducer<any> {
  return (state, action) => {
    // Reidrata o estado do localStorage antes de aplicar a ação
    const rehydratedState = rehydrateState();
    const initialState = {...state, ...rehydratedState};

    // Aplica a ação ao estado inicial
    const nextState = reducer(initialState, action);

    // console.log('--- Inside meta reducer ---');
    // console.log('Action Triggered -- Action:', action.type);
    // console.log('Next State:', nextState);

    // Atualiza o localStorage apenas se o estado auth estiver definido
    if (nextState.auth) {
      const stateWithoutAddress = {
        ...nextState,
        auth: {
          ...nextState.auth,
          user: nextState.auth.user ? {...nextState.auth.user, address: undefined} : null,
        },
      };

      localStorage.setItem('auth', JSON.stringify(stateWithoutAddress.auth));
    }

    return nextState;
  };
}

export function rehydrateState(): any {
  const auth = localStorage.getItem('auth');
  return auth ? {auth: JSON.parse(auth)} : {};
}

export const metaReducers: MetaReducer[] = [localStorageSyncReducer];

import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import { httpPost } from "../../utils/common"

const initialState = {
    isLoggedIn: false,
    status: '',
    error: ''
}

export const authorizeUser = createAsyncThunk('auth/authorizeUser',
    async (data) => {
        httpPost(data)
        .then(res => res.json())
        .then(res => res)
        .catch(err => err)
    }
)

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers:{

    },
    extraReducers: builder => {
        builder.addCase(authorizeUser.pending, (state) => {
            state.status = 'pending'
        })
        .addCase(authorizeUser.fulfilled, (state, action)=> {
            if(action.payload.status === 200)
            {
                state.status = 'completed'
                state.isLoggedIn = true
                localStorage.setItem("accessToken", action.payload.data)
            }
            else {
                state.status = 'failed'
                state.isLoggedIn = false
            }
        })
    }
})

export const authReducer = authSlice.reducer
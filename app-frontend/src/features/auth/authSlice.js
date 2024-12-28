import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import { httpPost, isInputMobileNumber } from "../../utils/common"

const initialState = {
    isLoggedIn: false,
    status: '',
    error: ''
}

export const authorizeUser = createAsyncThunk('auth/authorizeUser',
    async (data) => {
        if(isInputMobileNumber(data.email))
        {
            data = {
                ...data,
                email: '',
                phoneNumber: Number(data.email)
            }
        }

        const response =  httpPost(data)
        .then(res => res.json())
        .then(res => res)
        .catch(err => err)
        return response
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
        builder.addCase(authorizeUser.fulfilled, (state, action)=> {
            if(action.payload.statusCode === 200)
            {
                state.status = 'completed'
                state.isLoggedIn = true
                localStorage.setItem("accessToken", action.payload.data)
            }
            else {
                state.status = 'failed'
                state.error = action.payload.exception
                state.isLoggedIn = false
            }
        })
    }
})

export const authReducer = authSlice.reducer
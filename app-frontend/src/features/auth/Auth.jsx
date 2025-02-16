import ReactDOM from "react-dom";
import loginImage from '../../assets/images/login.png'
import { useEffect, useState } from "react";
import { isInputMobileNumber, validateLoginInput } from "../../utils/common";
import { useDispatch, useSelector } from "react-redux";
import { authorizeUser } from "./authSlice";

function Auth() {
  const [input, setInput] = useState({email:'', password: ''})
  const [isValid, setValid] = useState(false)
  const [isInputValid, setInputValidity] = useState(true)
  const [isEmail, setIsInputEmail] = useState(true)
  const dispatch = useDispatch()
  const authState = useSelector(state => state.auth)

  useEffect(() => {
    if(input.email === '' || input.password === ''){
      setValid(false)
      setInputValidity(true)
    }
    else{
      setInputValidity(true)
      setValid(true)
    }

    if(isInputMobileNumber(input.email))
      setIsInputEmail(false)
    else
      setIsInputEmail(true)
  }, [input])

  const clickHandler = () => {
    if(validateLoginInput(input, isEmail)){
      setInputValidity(true)
      dispatch(authorizeUser(input))
    }
    else{
      setInputValidity(false)
      setValid(false)
    }
  }

  return ReactDOM.createPortal(
    <div className="flex flex-col items-center h-[480px] w-[420px] rounded-xl mt-24">
      <img src={loginImage} alt='img' className='h-[250px] w-[500px]'/>
      <p className='font-bold text-xl text-slate-600 p-2'>Hala! Let's get started</p>
      <div className="flex flex-col h-[48px]">
        <input type="text" className="border border-black rounded-md outline-none w-[260px]
         h-9 p-1 text-xs pl-2" 
        value={input.email}
        placeholder='Please enter email or mobile number' 
        onChange={e => setInput({...input, email: e.target.value})}/>
        {
          !isInputValid && 
          <span className="text-red-600 text-[9px] text-left">
            Invalid Email ID/Mobile number</span>
        }
      </div>
     
      <input type="password" className="border border-black rounded-md outline-none w-[260px] h-9
       p-1 text-xs pl-2"
       placeholder='Password'
       value={input.password}
       onChange={e => setInput({...input, password: e.target.value})}/>

      <button type="submit" className="flex justify-center items-center mt-4 w-[260px] h-9 p-2
       text-white font-bold rounded-md" 
       disabled={!isValid}
       onClick={clickHandler}
       style={{backgroundColor: !isValid ? 'lightgray' : '#2563eb'}}
      >CONTINUE</button>

      <p className="text-[8px] text-slate-400 mt-4">This site is protected by reCAPTCHA and the 
      Google <span className="text-blue-600 font-semibold">Privacy Policy</span> and 
      <span className="text-blue-600 font-semibold"> Terms of Service</span> apply.
      </p>
      <p>{authState.status}</p>
    </div>,
    document.getElementById("modal-root")
  )
}

export default Auth
import ReactDOM from "react-dom";
import loginImage from '../../assets/images/login.png'
import { useEffect, useState } from "react";
import { isInputMobileNumber, validateLoginInput } from "../../utils/common";

function Auth() {
  const [input, setInput] = useState({email:'', password: ''})
  const [isValid, setValid] = useState(false)
  const [isInputValid, setInputValidity] = useState(true)
  const [isEmail, setIsInputEmail] = useState(true)

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
      
    }
    else{
      setInputValidity(false)
      setValid(false)
    }
  }

  return ReactDOM.createPortal(
    <div className="flex flex-col items-center h-[480px] w-[420px] bg-slate-100 rounded-xl mt-24">
      <img src={loginImage} alt='img' className='h-[260px] w-[500px]'/>
      <p className='font-bold text-xl text-slate-600 p-2'>Hala! Let's get started</p>
      <div className="flex flex-col">
        <input type="text" className="border border-black rounded-md outline-none w-[260px] h-9 p-1 text-xs pl-2" 
        placeholder='Please enter email or mobile number' 
        onChange={e => setInput({...input, email: e.target.value})}/>
        {
          !isInputValid && 
          <span className="text-red-600 text-[10px] text-left">
            Invalid Email ID/Mobile number</span>
        }
      </div>
     
      <input type="password" className="border border-black rounded-md outline-none w-[260px] h-9
       p-1 text-xs pl-2 mt-2"
       placeholder='Password'
       onChange={e => setInput({...input, password: e.target.value})}/>

      <button type="submit" className="flex justify-center items-center mt-4 w-[260px] h-9 p-2
       text-white font-bold rounded-md" 
       disabled={!isValid}
       onClick={clickHandler}
       style={{backgroundColor: !isValid ? 'lightgray' : '#2563eb'}}
      >CONTINUE</button>

      <p className="text-[8px] text-slate-400 mt-4">This site is protected by reCAPTCHA and the Google Privacy Policy and Terms of Service apply.
      </p>
    </div>,
    document.getElementById("modal-root")
  )
}

export default Auth
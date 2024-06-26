import { useState } from "react";
import useAuth from "../../hooks/useAuth.hook";
import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { IRegisterDto } from "../../types/auth.type";
import toast from "react-hot-toast";
import InputField from "../../components/general/InputField";
import { PATH_PUBLIC } from "../../routes/path";
import { Link } from "react-router-dom";
import Button from "../../components/general/Button";

const RegisterPage = () => {
  const [loading, setLoading] = useState<boolean>(false);
  const { register } = useAuth();

  const registerSchema = Yup.object().shape({
    firstName: Yup.string().required('First Name is required'),
    lastName: Yup.string().required('Last Name is required'),
    userName: Yup.string().required('User Name is required'),
    email: Yup.string().required('Email is required').email('Input text must be a valid email'),
    password: Yup.string().required('Password is required').min(8, 'Password must be at least 8 character'),
  });

  const {
    control,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm<IRegisterDto>({
    resolver: yupResolver(registerSchema),
    defaultValues: {
      firstName: '',
      lastName: '',
      userName: '',
      email: '',
      password: '',
    },
  });

  const onSubmitRegisterForm = async (data: IRegisterDto) => {
    try {
      setLoading(true);
      await register(data.firstName, data.lastName, data.userName, data.email, data.password);
      setLoading(false);
    } catch (error) {
      setLoading(false);
      const err = error as { data: string; status: number };
      const { status, data } = err;
      if (status === 400 || status === 409) {
        toast.error(data);
      } else {
        toast.error('An Error occurred. Please contact admins');
      }
    }
  };

  return (
    <div className='pageTemplate1 flex justify-center items-center min-h-screen'>
       <form
         onSubmit={handleSubmit(onSubmitRegisterForm)}
         className='flex-1 min-h-[600px] h-4/5 bg-[#f0ecf7] flex flex-col justify-center items-center rounded-r-2xl mx-auto'
         style={{ maxWidth: "600px" }} // Set max width for the form
       >
         <h1 className='text-4xl font-bold mb-2 text-[#754eb4]'>Register</h1>
   
         <InputField control={control} label='First Name' inputName='firstName' error={errors.firstName?.message} />
         <InputField control={control} label='Last Name' inputName='lastName' error={errors.lastName?.message} />
         <InputField control={control} label='User Name' inputName='userName' error={errors.userName?.message} />
         <InputField control={control} label='Email' inputName='email' error={errors.email?.message} />
         <InputField
           control={control}
           label='Password'
           inputName='password'
           inputType='password'
           error={errors.password?.message}
         />
   
         <div className='px-4 mt-2 mb-6 w-9/12 flex gap-2'>
           <h1>Already Have an account?</h1>
           <Link
             to={PATH_PUBLIC.login}
             className='text-[#754eb4] border border-[#754eb4] hover:shadow-[0_0_5px_2px_#754eb44c] px-3 rounded-2xl duration-200'
           >
             Log in
           </Link>
         </div>
   
         <div className='flex justify-center items-center gap-4 mt-6'>
           <Button variant='secondary' type='button' label='Reset' onClick={() => reset()} />
           <Button variant='primary' type='submit' label='Register' onClick={() => {}} loading={loading} />
         </div>
       </form>
    </div>
   );
};

export default RegisterPage;

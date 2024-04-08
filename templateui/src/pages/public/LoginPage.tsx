import { useState } from "react";
import useAuth from "../../hooks/useAuth.hook";
import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { ILoginDto } from "../../types/auth.type";
import toast from "react-hot-toast";
import { Link } from "react-router-dom";
import Button from "../../components/general/Button";
import InputField from "../../components/general/InputField";
import { PATH_PUBLIC } from "../../routes/path";

const LoginPage = () => {
  const [loading, setLoading] = useState<boolean>(false);
  const { login } = useAuth();

  const loginSchema = Yup.object().shape({
    userName: Yup.string().required('Username is required'),
    password: Yup.string().required('Password is required').min(8, 'Password must be at least 8 characters'),
  });

  const {
    control,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm<ILoginDto>({
    resolver: yupResolver(loginSchema),
    defaultValues: {
      userName: '',
      password: '',
    }
  });

  const onSubmitLoginForm = async (data: ILoginDto) => {
    try {
      setLoading(true);
      await login(data.userName, data.password);
      setLoading(false);
    } catch (error) {
      setLoading(false);
      const err = error as { data: string; status: number };
      const { status } = err;

      if (status === 401) {
        toast.error('Invalid Username or Password');
      } else {
        toast.error('An error occurred');
      }
    }
  };

  return (
    <div className='pageTemplate1 flex justify-center items-center min-h-screen'>
      <form
        onSubmit={handleSubmit(onSubmitLoginForm)}
        className='flex-1 min-h-[600px] h-4/5 bg-[#f0ecf7] flex flex-col justify-center items-center rounded-r-2xl'
        style={{ maxWidth: "600px" }} // Set max width for the form
      >
        <h1 className='text-4xl font-bold mb-2 text-[#754eb4]'>Login</h1>

        <InputField control={control} label='Username' inputName='userName' error={errors.userName?.message} />
        <InputField
          control={control}
          label='Password'
          inputName='password'
          inputType='password'
          error={errors.password?.message}
        />

        <div className='px-4 mt-2 mb-6 w-9/12 flex gap-2'>
          <h1>Don't have an account?</h1>
          <Link
            to={PATH_PUBLIC.register}
            className='text-[#754eb4] border border-[#754eb4] hover:shadow-[0_0_5px_2px_#754eb44c] px-3 rounded-2xl duration-200'
          >
            Register
          </Link>
        </div>

        <div className='flex justify-center items-center gap-4 mt-6'>
          <Button variant='secondary' type='button' label='Reset' onClick={() => reset()} />
          <Button variant='primary' type='submit' label='Login' onClick={() => {}} loading={loading} />
        </div>
      </form>
    </div>
  );
};

export default LoginPage;

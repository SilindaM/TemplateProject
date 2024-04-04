import React from 'react'
import useAuth from '../../hooks/useAuth.hook'
import { useNavigate } from 'react-router-dom';
import { AiOutlineHome } from 'react-icons/ai';
import { FiLock, FiUnlock } from 'react-icons/fi';
import Button from '../general/Button';
import { PATH_DASHBOARD, PATH_PUBLIC } from '../../routes/path';

const header = () => {
    const {isAuthLoading,isAuthenticated,user,logout} = useAuth();
    const navigate = useNavigate();

    const userRolesLabelCreator = () => {
        if (user) {
          let result = '';
          user.roles.forEach((role, index) => {
            result += role;
            if (index < user.roles.length - 1) {
              result += ', ';
            }
          });
          return result;
        }
        return '--';
      };
    
  return (
    <div className='flex justify-between items-center bg-black h-12 px-4'>
        <div className='flex items-items-center gap-4'>
            <AiOutlineHome className='w-8 h-8 text-purple-40 hover:text-purple-400 cursor-pointer'
            onClick={()=>navigate('/')}/>
        </div>
        <div className='flex gap-1'>
            <h1 className='px-1 border border-dashed border-purple-300 rounded-lg'>
                AuthLoading:{isAuthLoading?'True':'--'}
            </h1>
            <h1 className='px-1 border border-dashed border-purple-400 rounded-lg flex items-center gap-1'>
                Auth:
                {isAuthenticated? <FiUnlock className='text-green-200'/> : <FiLock className='text-red-400'/>}
            </h1>
            <h1 className='px-1 border border-dashed border-purple-200 rounder-lg'>
                UserName : {user? user.userName : '--'}
            </h1>
            <h1 className='px-1 border border-dashed border-purple-400 rounded-lg'>
                UserRoles : {userRolesLabelCreator()}
            </h1>
        </div>
        <div>
            {isAuthenticated ?(
                <div className=' flex items-center gap-2'>
                <Button label='Dashboard'
                  onClick={()=>navigate(PATH_DASHBOARD.dashboard)}
                  type='button'
                  variant='light'
                  />
                  <Button label='Logout'
                    onClick={logout}
                    type='button'
                    variant='light'
                    />
                </div>
            ):(
                <div className='flex items-center gap-2'>
                <Button
                label='register'
                onClick={()=>navigate(PATH_PUBLIC.register)}
                type='button'
                variant='light'/>
                <Button
                label='login'
                onClick={()=>navigate(PATH_PUBLIC.login)}
                type='button'
                variant='light'/>
                
                </div>
            )}
        </div>
    </div>
  )
}

export default header
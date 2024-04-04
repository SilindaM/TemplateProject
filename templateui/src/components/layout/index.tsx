import useAuth from '../../hooks/useAuth.hook';
import { Outlet, useLocation } from 'react-router-dom';
import Sidebar from './Sidebar';
import Header from './header';

const Layout = () => {
    const {isAuthenticated}=useAuth();
    const {pathname} = useLocation();

    console.log(pathname);
    
    const sideBarRender=()=>{
        if(isAuthenticated && pathname.toLocaleLowerCase().startsWith('/dashboard')){
            return <Sidebar/>
        }
        return null;
    }
  return (
    <div>
        <div>
            <Header/>
            <div className='flex'>
                {sideBarRender()}
                <Outlet/>
            </div>
        </div>
    </div>
  )
}

export default Layout
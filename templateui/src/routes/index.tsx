import { Navigate, Route, Routes } from 'react-router-dom'
import Layout from '../components/layout'
import { PATH_DASHBOARD, PATH_PUBLIC } from './path'
import HomePage from '../pages/public/HomePage'
import LoginPage from '../pages/public/LoginPage'
import RegisterPage from '../pages/public/RegisterPage'
import UnauthorizedPage from '../pages/public/UnauthorizedPage'
import AuthGuard from '../auth/AuthGuard'
import { adminAccessRoles, allAccessRoles, managerAccessRoles, ownerAccessRoles } from '../auth/auth.utils'
import DashboardPage from '../pages/dashboard/DashboardPage'
import UserPage from '../pages/dashboard/UserPage'
import InboxPage from '../pages/dashboard/InboxPage'
import MyLogsPage from '../pages/dashboard/MyLogsPage'
import SendMessagePage from '../pages/dashboard/SendMessagePage'
import ManagerPage from '../pages/dashboard/ManagerPage'
import AdminPage from '../pages/dashboard/AdminPage'
import SystemLogsPage from '../pages/dashboard/SystemLogsPage'
import AllMessagesPage from '../pages/dashboard/AllMessagesPage'
import UpdateRolePage from '../pages/dashboard/UpdateRolePage'
import UserManagementPage from '../pages/dashboard/UserManagementPage'
import OwnerPage from '../pages/dashboard/OwnerPage'
import NotFoundPage from '../pages/public/NotFoundPage'

const GlobalRouter = () => {
  return (
    <Routes>
        <Route element={<Layout/>}>
        <Route index element={<HomePage/>}/>
        <Route path={PATH_PUBLIC.register} element={<RegisterPage/>}/>
        <Route path={PATH_PUBLIC.login} element={<LoginPage/>}/>
        <Route path={PATH_PUBLIC.unauthorized} element={<UnauthorizedPage/>}/>
        </Route>
        
        {/*Protected Roles all users */}
        <Route element={<AuthGuard roles={allAccessRoles}/>}>
            <Route path={PATH_DASHBOARD.dashboard} element={<DashboardPage/>}/>
            <Route path={PATH_DASHBOARD.sendMessage} element={<SendMessagePage/>}/>
            <Route path={PATH_DASHBOARD.inbox} element={<InboxPage/>}/>
            <Route path={PATH_DASHBOARD.myLogs} element={<MyLogsPage/>}/>
            <Route path={PATH_DASHBOARD.user} element={<UserPage/>}/>
        </Route>

        {/*Protected Roles manager*/}
        <Route element={<AuthGuard roles={managerAccessRoles}/>}>
            <Route path={PATH_DASHBOARD.dashboard} element={<ManagerPage/>}/>
        </Route>
        
        {/*Protected Roles admin*/}
        <Route element={<AuthGuard roles={adminAccessRoles}/>}>
            <Route path={PATH_DASHBOARD.usersManagement} element={<UserManagementPage/>}/>
            <Route path={PATH_DASHBOARD.updateRole} element={<UpdateRolePage/>}/>
            <Route path={PATH_DASHBOARD.allMessages} element={<AllMessagesPage/>}/>
            <Route path={PATH_DASHBOARD.systemLogs} element={<SystemLogsPage/>}/>
            <Route path={PATH_DASHBOARD.admin} element={<AdminPage/>}/>
        </Route>

        {/*Protected Roles owner*/}
        <Route element={<AuthGuard roles={ownerAccessRoles}/>}>
            <Route path={PATH_DASHBOARD.owner} element={<OwnerPage/>}/>
        </Route>
        {/*Not Found */}
        <Route path={PATH_PUBLIC.notFound} element={<NotFoundPage/>}/>
        <Route path='*' element={<Navigate to={PATH_PUBLIC.notFound} replace/>}/>
    </Routes>
  )
}
export default GlobalRouter
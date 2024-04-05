import { FaUser } from "react-icons/fa"
import PageAccessTemplate from "../../components/pageAccess/PageAccessTemplate"


const UserManagementPage = () => {
  return (  <div className="pageTemplate2">
  <PageAccessTemplate color="#FEC223" icon={FaUser} role="User"/> 
</div>
  )
}

export default UserManagementPage
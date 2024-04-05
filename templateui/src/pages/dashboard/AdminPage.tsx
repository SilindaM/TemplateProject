import { FaUserShield } from "react-icons/fa"
import PageAccessTemplate from "../../components/pageAccess/PageAccessTemplate"


const AdminPage = () => {
  return (  <div className="pageTemplate2">
  <PageAccessTemplate color="#FE2534" icon={FaUserShield} role="Admin"/> 
</div>
  )
}

export default AdminPage
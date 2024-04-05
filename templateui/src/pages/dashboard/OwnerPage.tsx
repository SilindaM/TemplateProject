import { FaUserCog } from "react-icons/fa"
import PageAccessTemplate from "../../components/pageAccess/PageAccessTemplate"

const OwnerPage = () => {
  return (  <div className="pageTemplate2">
  <PageAccessTemplate color="#FEC223" icon={FaUserCog} role="User"/> 
</div>
  )
}

export default OwnerPage
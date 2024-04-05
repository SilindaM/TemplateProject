import { FaUserTie } from "react-icons/fa";
import PageAccessTemplate from "../../components/pageAccess/PageAccessTemplate";

const ManagerPage = () => {
  return (
    <div className="pageTemplate2">
      <PageAccessTemplate color="#FEC223" icon={FaUserTie} role="Manager" />
    </div>
  );
};

export default ManagerPage;

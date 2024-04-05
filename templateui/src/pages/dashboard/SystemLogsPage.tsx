import { FaUser } from "react-icons/fa"
import PageAccessTemplate from "../../components/pageAccess/PageAccessTemplate"
import { ILogDto } from "../../types/log.type";
import axios from "axios";
import axiosInstance from "../../utils/axiosInstance";
import toast from "react-hot-toast";
import { LOGS_URL } from "../../utils/globalConfig";
import { useEffect, useState } from "react";
import Spinner from "../../components/general/Spinner";

const SystemLogsPage = () => {

  const [Logs,setLogs]= useState<ILogDto[]>([]);
  const [loading,setLoading] = useState<boolean>(false);

  const getLogs = async ()=>{
    try {
      setLoading(true);
      const response = await axiosInstance.get<ILogDto[]>(LOGS_URL);
      const {data} = response;
      setLogs(data);
      setLoading(false);
    } catch (error) {
      toast.error("An Error Occured")
    }
  }
  useEffect(()=>{
    getLogs();
  },[]);
  if(loading){
    return(
      <div className="">
        <Spinner/>
      </div>
    )
  }
  return (  <div className="pageTemplate2">
  <PageAccessTemplate color="#FEC223" icon={FaUser} role="User"/> 
</div>
  )
}

export default SystemLogsPage


import com.is.ws.ServerInfo;
import com.is.ws.ServerInfoService;
import java.net.ProxySelector;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */


/**
 *
 * @author izabe
 */
public class WSClientHandler {

    /**
     * @param args the command line arguments
     */
     public static void main(String[] args) throws Exception {
       ProxySelector.setDefault(new CustomProxySelector());//monitor SOAP
        ServerInfoService sis = new ServerInfoService();
        ServerInfo si = sis.getServerInfoPort();
        System.out.println(si.getServerName());
       
    }
    
}

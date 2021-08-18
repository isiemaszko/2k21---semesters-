/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package wsclient;

import java.net.ProxySelector;
import java.util.Map;
import javax.xml.ws.BindingProvider;
import ssl.WSServerSSL;
import ssl.WSServerSSLService;

/**
 *
 * @author izabe
 */
public class WSClient2_SSL {
      public static void main(String[] args){
          
          
          System.setProperty("javax.net.debug","all");
          System.setProperty("javax.net.ssl.trustStore","/client/client_cacerts.jks");
          System.setProperty("javax.net.ssl.trustStorePassword","changeit");
          
       //   ProxySelector.setDefault(new CustomProxySelector());
           WSServerSSLService service=new WSServerSSLService();
          WSServerSSL port=service.getWSServerSSLPort();
          
          //z ssl
          // Authentication
    BindingProvider bindProv = (BindingProvider) port;
    Map<String, Object> context = bindProv.getRequestContext();
    context.put("javax.xml.ws.security.auth.username", "isiemaszko");
    context.put("javax.xml.ws.security.auth.password", "haslo123");
          
          //bez ssl
          String hello=port.hello("Izabela");
          System.out.println("Client: "+hello);
      }
      
}

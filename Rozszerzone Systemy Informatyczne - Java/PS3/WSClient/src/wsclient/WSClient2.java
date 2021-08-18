/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package wsclient;

import java.net.URL;
import javax.xml.namespace.QName;
import javax.xml.ws.Service;
import org.jg.rsi.HelloWorld;

/**
 *
 * @author izabe
 */
public class WSClient2{
    
     public static void main(String[] args) throws Exception{
      //    URL url=new URL("http://localhost:9805/ws/hello?wsdl");
       URL url=new URL("http://localhost:8080/ws/hello?wsdl");
          QName name= new QName("http://rsi.jg.org/","HelloWorldImplService");
          
          Service service =Service.create(url, name);
          HelloWorld hello=service.getPort(HelloWorld.class);
          
           String request="to ja klient  -2";
        String response=hello.getHelloWorldAsString(request);
        System.out.println("Klient wysłał "+request);
        System.out.println("Klient dostał "+response);
     }
   
    
}

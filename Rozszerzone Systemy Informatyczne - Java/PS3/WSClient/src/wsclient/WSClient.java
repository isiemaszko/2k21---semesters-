/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package wsclient;

import org.jg.rsi.HelloWorld;
import org.jg.rsi.HelloWorldImplService;

/**
 *
 * @author izabe
 */
public class WSClient {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
        
        HelloWorldImplService helloService=new HelloWorldImplService();
        
        HelloWorld hello=helloService.getHelloWorldImplPort();
        
        String request="to ja klient";
        String response=hello.getHelloWorldAsString(request);
        System.out.println("Klient wysłał "+request);
        System.out.println("Klient dostał "+response);
    }
    
}

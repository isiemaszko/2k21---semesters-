/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package webservice;

import javax.xml.ws.Endpoint;
import org.jg.rsi.HelloWorldImpl;

/**
 *
 * @author izabe
 */
public class HelloWorldPublisher {
    
    public static void main(String[] args){
        Endpoint.publish("http://localhost:9805/ws/hello", new HelloWorldImpl());
        System.out.println("Web serwis czeka na klienta..");
    }
       
}

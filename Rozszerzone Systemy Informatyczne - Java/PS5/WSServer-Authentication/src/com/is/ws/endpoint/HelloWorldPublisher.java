/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.is.ws.endpoint;

/**
 *
 * @author izabe
 */
import com.is.ws.HelloWorldImpl;
import javax.xml.ws.Endpoint;
 
//Endpoint publisher
public class HelloWorldPublisher{
 
    public static void main(String[] args) {
       Endpoint.publish("http://localhost:9999/ws/hello", new HelloWorldImpl());
    }
 
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.is.ws.endpoint;

import com.is.ws.ServerInfo;
import javax.xml.ws.Endpoint;

/**
 *
 * @author izabe
 */
public class WsPublisher{
 
    public static void main(String[] args) {
       Endpoint.publish("http://localhost:8888/ws/server", new ServerInfo());
       
       System.out.println("Service is published!");
    }
 
}
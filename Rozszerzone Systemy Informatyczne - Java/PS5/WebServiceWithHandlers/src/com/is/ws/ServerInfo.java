/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.is.ws;

import javax.jws.HandlerChain;
import javax.jws.WebMethod;
import javax.jws.WebService;

/**
 *
 * @author izabe
 */
@WebService
@HandlerChain(file="handler-chain.xml")
public class ServerInfo {
     @WebMethod
    public String getServerName() {
        
        return "IZS server";
        
    }
}

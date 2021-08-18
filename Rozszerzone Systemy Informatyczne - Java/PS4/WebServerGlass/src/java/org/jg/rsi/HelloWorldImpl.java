/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package org.jg.rsi;

import javax.jws.WebService;

/**
 *
 * @author izabe
 */
@WebService(endpointInterface = "org.jg.rsi.HelloWorld")
public class HelloWorldImpl implements HelloWorld {
    @Override

    public String getHelloWorldAsString(String name) {

    return "Witaj świecie JAX-WS: " + name;

    }
}

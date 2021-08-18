/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package org.jg.rsi;

import javax.jws.WebMethod;

import javax.jws.WebService;

import javax.jws.soap.SOAPBinding;

import javax.jws.soap.SOAPBinding.Style;

import javax.jws.soap.SOAPBinding.Use;
/**
 *
 * @author izabe
 */

@WebService
@SOAPBinding(style = Style.DOCUMENT, use = Use.LITERAL) //optional
public interface HelloWorld {
    @WebMethod

String getHelloWorldAsString(String name);
    
}

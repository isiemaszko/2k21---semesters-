/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.is.ws.client;

/**
 *
 * @author izabe
 */
import com.is.ws.HelloWorld;
import java.net.ProxySelector;
import java.net.URL;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.xml.namespace.QName;
import javax.xml.ws.BindingProvider;
import javax.xml.ws.Service;
import javax.xml.ws.handler.MessageContext;



public class HelloWorldClient{
    
    private static final String WS_URL = "http://localhost:9999/ws/hello?wsdl";
        
    public static void main(String[] args) throws Exception {
        
       ProxySelector.setDefault(new CustomProxySelector());//monitor SOAP
       
       
    URL url = new URL(WS_URL);
        QName qname = new QName("http://ws.is.com/", "HelloWorldImplService");

        Service service = Service.create(url, qname);
        HelloWorld hello = service.getPort(HelloWorld.class);
        
        /*******************UserName & Password ******************************/
        Map<String, Object> req_ctx = ((BindingProvider)hello).getRequestContext();
        req_ctx.put(BindingProvider.ENDPOINT_ADDRESS_PROPERTY, WS_URL);

        Map<String, List<String>> headers = new HashMap<String, List<String>>();
        headers.put("Username", Collections.singletonList("isiemaszko"));
        headers.put("Password", Collections.singletonList("password"));
        req_ctx.put(MessageContext.HTTP_REQUEST_HEADERS, headers);
        /**********************************************************************/
        
        System.out.println(hello.getHelloWorldAsString());
       
    }
}
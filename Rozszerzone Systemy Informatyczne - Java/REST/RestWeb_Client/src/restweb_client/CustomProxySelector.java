/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package restweb_client;

import java.io.IOException;
import java.net.InetSocketAddress;
import java.net.Proxy;
import java.net.ProxySelector;
import java.net.SocketAddress;
import java.net.URI;
import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author izabe
 */
public class CustomProxySelector extends ProxySelector{

    @Override
    public List<Proxy> select(URI uri) {
       Proxy proxy=new Proxy(Proxy.Type.HTTP, new InetSocketAddress("localhost",4040));
       ArrayList list=new ArrayList();
       list.add(proxy);
       return list;
    
    }

    @Override
    public void connectFailed(URI uri, SocketAddress sa, IOException ioe) {
        System.out.println("Connection to: "+uri+" failed");
    }
    
    
}

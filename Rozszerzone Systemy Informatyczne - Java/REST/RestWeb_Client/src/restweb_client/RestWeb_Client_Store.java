/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package restweb_client;

import classes.Product;
import classes.ResponseList;
import client.Store;
import java.net.ProxySelector;
import javax.ws.rs.client.Client;
import javax.ws.rs.client.ClientBuilder;
import javax.ws.rs.client.WebTarget;
import javax.ws.rs.core.MediaType;

/**
 *
 * @author izabe
 */
public class RestWeb_Client_Store {
     public static void main(String[] args) {
        // TODO code application logic here
        ProxySelector.setDefault(new CustomProxySelector());
        System.out.println("Start");
        System.out.println("---------------");
        System.out.println("Products"); 
        System.out.println("---------------");
        
        Store client=new Store();
        ResponseList allProducts=client.getAllProducts(ResponseList.class);
        for(Product p:allProducts.getProducts()){
         System.out.println(p.getName());}
        
        System.out.println("---------------");
        System.out.println("Find product"); 
        
        ResponseList findProducts=client.findProducts("produkt", "Mlekowita", (float)3.5);
         for(Product p:findProducts.getProducts()){
         System.out.println(p.getName()+" : "+p.getPrice()+" : "+p.getCompany());}
         
         System.out.println("---------------");
        System.out.println("End");
        
        
        
        
    }
}

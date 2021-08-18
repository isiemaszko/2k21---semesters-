/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package client;

import classes.ResponseList;
import classes.SearchParam;
import javax.ws.rs.ClientErrorException;
import javax.ws.rs.client.Client;
import javax.ws.rs.client.Entity;
import javax.ws.rs.client.WebTarget;
import javax.ws.rs.core.MediaType;

/**
 * Jersey REST client generated for REST resource:SklepResource [/sklep]<br>
 * USAGE:
 * <pre>
 *        Store client = new Store();
 *        Object response = client.XXX(...);
 *        // do whatever with response
 *        client.close();
 * </pre>
 *
 * @author izabe
 */
public class Store {

    private WebTarget webTarget;
    private Client client;
    private static final String BASE_URI = "http://localhost:8080/RestWebApp/webresources";

    public Store() {
        client = javax.ws.rs.client.ClientBuilder.newClient();
        webTarget = client.target(BASE_URI).path("sklep");
    }

    public <T> T getAllProducts(Class<T> responseType) throws ClientErrorException {
        WebTarget resource = webTarget;
        resource = resource.path("allproducts");
        return resource.request(javax.ws.rs.core.MediaType.APPLICATION_JSON).get(responseType);
    }

    public ResponseList findProducts(String name, String company, float price) throws ClientErrorException {
        WebTarget resource = webTarget;
        resource = resource.path("findProducts");
        
        SearchParam sp=new SearchParam();
        sp.setName(name);
        sp.setCompany(company);
        sp.setPriceLessThan(price);
        
        ResponseList rs=resource.request(MediaType.APPLICATION_JSON)
                .post(Entity.entity(sp,MediaType.APPLICATION_JSON),ResponseList.class);
                
        return rs;
    }

    public void close() {
        client.close();
    }
    
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package restweb_client;

import javax.ws.rs.client.Client;
import javax.ws.rs.client.ClientBuilder;
import javax.ws.rs.client.WebTarget;
import javax.ws.rs.core.MediaType;

/**
 *
 * @author izabe
 */
public class RestWeb_Client {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
        Client client=ClientBuilder.newClient();
        WebTarget target=client.target("http://localhost:8080/RestWebApp/webresources/messages/2");
        
        String mes=target.request(MediaType.APPLICATION_JSON).get(String.class);
        System.out.println("response "+mes);
    }
    
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package filter;

import java.io.IOException;
import java.util.Base64;
import java.util.List;
import java.util.StringTokenizer;
import javax.ws.rs.container.ContainerRequestContext;
import javax.ws.rs.container.ContainerRequestFilter;
import javax.ws.rs.core.Response;
import javax.ws.rs.ext.Provider;

/**
 *
 * @author izabe
 */
@Provider
public class SecurityFilter implements ContainerRequestFilter{

    @Override
    public void filter(ContainerRequestContext requestContext) throws IOException {
       System.out.print("---------------:");
       System.out.print("Filtr");
       
       List<String> authHeader=requestContext.getHeaders().get("authorization");
       if(authHeader!=null && authHeader.size()>0){
           String authToken=authHeader.get(0);
           System.out.print("authToken: "+authToken);
           String authToken2=authToken.replaceFirst("Basic ", "");
           
           System.out.print("authTokenBezBasic: "+authToken2);
           byte[] bytes=authToken2.getBytes();
           System.out.print("bytes: "+new String(bytes));
           byte[] decodeBytes2=Base64.getDecoder().decode(bytes);
           String decodeStr2=new String(decodeBytes2);
           System.out.print("decodeString: "+decodeStr2);
           
           StringTokenizer tokenizer=new StringTokenizer(decodeStr2,":");
           String username=tokenizer.nextToken();
           String password=tokenizer.nextToken();
           if("user".equals(username) && "password".equals(password)){
           return ;}
       }
     
       Response unauthorizeStatus=Response
               .status(Response.Status.UNAUTHORIZED)
               .entity("Brak dostępu")
               .build();
       requestContext.abortWith(unauthorizeStatus);
    }
    
}

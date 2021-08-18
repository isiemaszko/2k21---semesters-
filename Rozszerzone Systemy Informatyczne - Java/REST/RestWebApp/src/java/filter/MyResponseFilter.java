/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package filter;

import java.io.IOException;
import javax.ws.rs.container.ContainerRequestContext;
import javax.ws.rs.container.ContainerRequestFilter;
import javax.ws.rs.container.ContainerResponseContext;
import javax.ws.rs.container.ContainerResponseFilter;
import javax.ws.rs.ext.Provider;

/**
 *
 * @author izabe
 */
@Provider
public class MyResponseFilter implements ContainerResponseFilter,ContainerRequestFilter{

    @Override
    public void filter(ContainerRequestContext requestContext, ContainerResponseContext responseContext) throws IOException {
        responseContext.getHeaders().add("mojNaglowek", "rsi test");
        System.out.print("Response filter:");
        System.out.print("Headers: "+responseContext.getHeaders());
    }

    @Override
    public void filter(ContainerRequestContext requestContext) throws IOException {
         System.out.print("Request filter:");
        System.out.print("Headers: "+requestContext.getHeaders());
    }
    
}

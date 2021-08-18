/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package myservice;

import java.rmi.RemoteException;
import java.util.ArrayList;
import java.util.List;
import javax.jws.WebMethod;
import javax.jws.WebService;

/**
 *
 * @author izabe
 */
@WebService
public class Store {
    
    @WebMethod
     public List<Product> listaProduktow() throws RemoteException{
        ArrayList<Product> list=new ArrayList<Product>();
        list.add(new Product("chleb","Okruszek", (float) 1.25));
         list.add(new Product("mleko","Mlekowita", (float)3.75));
        return list;
    }
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package rmi;

/**
 *
 * @author izabe
 */
import java.rmi.RemoteException;

import java.rmi.server.UnicastRemoteObject;
import java.util.ArrayList;
import java.util.List;

public class MyServerImpl extends UnicastRemoteObject implements MyServerInt {

    int i = 0;

    protected MyServerImpl() throws RemoteException {

        super();

    }

    @Override

    public String getDescription(String text) throws RemoteException {

        i++;

        System.out.println("MyServerImpl.getDescription: " + text + " " + i);

        return "getDescription: " + text + " " + i;

    }
    
    @Override
    public List<Product> listaProduktow() throws RemoteException{
        ArrayList<Product> list=new ArrayList<Product>();
        list.add(new Product("chleb", (float) 1.25));
         list.add(new Product("mleko", (float)3.75));
        return list;
    }
    
     @Override

    public String findProduct(String name) throws RemoteException {

        

        return "getDescription: " ;

    }
    
    
}

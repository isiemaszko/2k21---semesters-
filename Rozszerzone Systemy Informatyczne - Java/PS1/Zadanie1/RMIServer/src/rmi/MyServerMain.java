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
import java.net.MalformedURLException;

import java.rmi.Naming;

import java.rmi.RemoteException;

import java.rmi.registry.LocateRegistry;

public class MyServerMain {

    public static void main(String[] args) {

        try {

//System.setProperty("java.security.policy", "file:/C:/Users/izabe/OneDrive/Pulpit/RSI/PS1/Zadanie1/RMIServer/security.policy");
            System.setProperty("java.security.policy", "security.policy");

            if (System.getSecurityManager() == null) {

                System.setSecurityManager(new SecurityManager());

            }

            System.setProperty("java.rmi.server.codebase", "file:/C:/Users/izabe/OneDrive/Pulpit/RSI/PS1/Zadanie1/RMIServer/build/classes/");
            System.setProperty("java.rmi.server.hostname", "192.168.0.114");

            System.out.println("Codebase: " + System.getProperty("java.rmi.server.codebase"));
            LocateRegistry.createRegistry(1099);

            MyServerImpl obj1 = new MyServerImpl();

            Naming.rebind("//192.168.0.114/ABC", obj1);

            System.out.println("Serwer oczekuje ...");

        } catch (RemoteException | MalformedURLException e) {

            e.printStackTrace();

        }
    }
}

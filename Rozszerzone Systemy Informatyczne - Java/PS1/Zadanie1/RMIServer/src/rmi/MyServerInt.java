/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package rmi;
import java.rmi.Remote;
import java.rmi.RemoteException;
import java.util.List;
/**
 *
 * @author izabe
 */
public interface MyServerInt extends Remote{

String getDescription(String text) throws RemoteException;
List<Product> listaProduktow()throws RemoteException;
String findProduct(String name) throws RemoteException;
}

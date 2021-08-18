/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package calculator;

import java.rmi.Remote;
import java.rmi.RemoteException;

/**
 *
 * @author izabe
 */
public interface CalcServerInt extends Remote{

String addition(int a, int b) throws RemoteException;
String subtraction(int a, int b)throws RemoteException;
String multiplication(int a, int b)throws RemoteException;
String division(int a, int b)throws RemoteException;
}

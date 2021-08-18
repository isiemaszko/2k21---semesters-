/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package calculator;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

/**
 *
 * @author izabe
 */
public class CalcServerImpl extends UnicastRemoteObject implements CalcServerInt
{
    protected CalcServerImpl() throws RemoteException {

    super();}

    @Override
    public String addition(int a, int b) throws RemoteException{
        int result=a+b;
        System.out.println("CalcServerImpl.addition: " + result);

        return "result: " + result;
    }

    @Override
    public String subtraction(int a, int b) throws RemoteException {
        int result=a-b;
        System.out.println("CalcServerImpl.subtraction: " + result);

        return "result: " + result; //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public String multiplication(int a, int b) throws RemoteException {
        int result=a*b;
       

        return "result: " + result; //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public String division(int a, int b) throws RemoteException {
        
        if(b==0){
            System.out.println("CalcServerImpl.division: null" );
            return "nie dziel przez zero xd";
        }
        int result=a/b;
        System.out.println("CalcServerImpl.division: " + result);

        return "result: " + result; //To change body of generated methods, choose Tools | Templates.
    }
}

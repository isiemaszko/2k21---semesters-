/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package chat;

import java.rmi.Remote;
import java.rmi.RemoteException;

/**
 *
 * @author izabe
 */
public interface ChatServerInt extends Remote{
    public String getName()throws RemoteException;
    public void send(String msg) throws RemoteException;
    public void setClient(ChatServerInt e) throws RemoteException;
    public ChatServerInt getClient()throws RemoteException;
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package chat;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

/**
 *
 * @author izabe
 */
public class Chat  extends UnicastRemoteObject implements ChatServerInt{

    public String name;
    public ChatServerInt client=null;
    
    public Chat(String name) throws RemoteException{
        
        this.name=name;
    }
    
   
    
    @Override
    public String getName() throws RemoteException {
        return this.name;
    }

    @Override
    public void send(String msg) throws RemoteException {
        System.out.println(name);
           }

    @Override
    public void setClient(ChatServerInt e) throws RemoteException {
      this.client=e;
    }

    @Override
    public ChatServerInt getClient() throws RemoteException {
        return client;
    }
    
}

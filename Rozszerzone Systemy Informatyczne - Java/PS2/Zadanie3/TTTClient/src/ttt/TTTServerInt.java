/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ttt;

import java.rmi.Remote;
import java.rmi.RemoteException;

/**
 *
 * @author izabe
 */
public interface TTTServerInt extends Remote {
    public String getName()throws RemoteException;
    public char[][] getBoard() throws RemoteException;
     public void setBoard(char[][] board) throws RemoteException;
    public void send(String msg) throws RemoteException;
    public void setClient(TTTServerInt e) throws RemoteException;
    public TTTServerInt getClient()throws RemoteException;
    public  void printBoard(char[][] board)throws RemoteException;
     public  boolean performMove(char[][] board)throws RemoteException;
      public void setActivePl(Boolean e) throws RemoteException;
    public boolean getActivePl()throws RemoteException;
     public boolean checkWinner(char[][] board) throws RemoteException;
     public boolean checkFirstDiagonal(char[][] board) throws RemoteException;
     public boolean checkSecondDiagonal(char[][] board) throws RemoteException;
     public boolean checkWinInRows(char[][] board) throws RemoteException;
      public boolean  checkWinInColumns(char[][] board) throws RemoteException;
    
    
}

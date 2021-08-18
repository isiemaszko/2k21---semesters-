/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ttt;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.Scanner;

/**
 *
 * @author izabe
 */
public class Board extends UnicastRemoteObject implements TTTServerInt {
    //client
   public char [][] board;
   public String name;
   public char activePlayer;
   int dim=3;
   public TTTServerInt client=null;
    public boolean activePl;
   
    public Board(String name,char activePlayer)throws RemoteException{
        board=new char[3][3];
        this.name=name;
        this.activePlayer=activePlayer;
         activePl=true;
    }
    
     @Override
    public String getName() throws RemoteException {
        return this.name;
    }
      @Override
    public void send(String msg) throws RemoteException {
        System.out.println(msg);
           }
    @Override
   public  boolean performMove(char[][] board)throws RemoteException {
        System.out.println(this.name + ", podaj nr wiersza");
        int row = new Scanner(System.in).nextInt();
        System.out.println(",podaj nr kolumny");
        int col = new Scanner(System.in).nextInt();

        if (board[row][col] == 0) { // jeżeli pole jest wolne
            board[row][col] = activePlayer; 

            return true; // zwracam true jezeli ruch sie udał
            // jeżeli kod dojdzie do tej linii to znaczy ze się udał
        } else { // ten else jest opcjonalny, wystarczyloby return false (dlaczego?)
            return false; // zwroce false, jeżeli miejsce było zajęte
        }

    }
   
     @Override
    public char[][] getBoard() throws RemoteException {
         return this.board;
    }
    @Override
    public void setBoard(char[][] board) throws RemoteException {
         this.board=board;
    }
    
    @Override
    public  void printBoard(char[][] board)throws RemoteException {
        
        // nagłówki kolumn
        System.out.print("\t");
        for (int i = 0; i < dim; i++) {
            System.out.print(i + "\t");
        }
        System.out.println();
        for (int row = 0; row < dim; row++) {
            System.out.print(row + ":\t");
            for (int column = 0; column < dim; column++) {
                System.out.print(board[row][column] + "\t");
            }
            System.out.println();
        }
    }
    
    @Override
     public boolean checkWinner(char[][] board) throws RemoteException {
        return checkFirstDiagonal(board) ||
                checkSecondDiagonal(board) ||
                checkWinInColumns(board) ||
                checkWinInRows(board);
    }
     
     @Override
     public boolean checkFirstDiagonal(char[][] board)  throws RemoteException{
        int dim = board.length;
        for (int i = 0; i < dim; i++) {
            if (board[i][i] != activePlayer) {
                return false;
            }
        }
        return true;
    }

     @Override
    public  boolean checkSecondDiagonal(char[][] board) throws RemoteException {
        int dim = board.length;
        for (int i = 0; i < dim; i++) {
            if (board[i][dim - i - 1] != activePlayer) {
                return false;
            }
        }
        return true;
    }

    @Override
    public boolean checkWinInColumns(char[][] board) throws RemoteException {
        int dim = board.length;
        for (int col = 0; col < dim; col++) {
            // zakładam optymistycznie, że activePlayer wygrał
            boolean win = true;
            for (int row = 0; row < dim; row++) {
                if (board[row][col] != activePlayer) {
                    win = false;
                    break;
                }
            }
            if (win) {
                return true;
            }
        }
        return false;
    }

    @Override
    public boolean checkWinInRows(char[][] board) throws RemoteException {
        int dim = board.length;
        for (int col = 0; col < dim; col++) {
            boolean win = true;
            for (int row = 0; row < dim; row++) {
                if (board[col][row] != activePlayer) {
                    win = false;
                    break;
                }
            }
            if (win) {
                return true;
            }
        }
        return false;
    }

    @Override
    public TTTServerInt getClient() throws RemoteException {
        return client;
    }

 

    @Override
    public void setClient(TTTServerInt e) throws RemoteException {
        this.client=e;
    }
    
      @Override
    public boolean getActivePl() throws RemoteException {
        return activePl;
    }

 

    @Override
    public void setActivePl(Boolean change) throws RemoteException {
        this.activePl=change;
    }
}

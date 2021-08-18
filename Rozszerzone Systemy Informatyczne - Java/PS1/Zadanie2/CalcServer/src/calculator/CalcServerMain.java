/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package calculator;
import java.net.MalformedURLException;

import java.rmi.Naming;

import java.rmi.RemoteException;

import java.rmi.registry.LocateRegistry;
import java.util.Scanner;
/**
 *
 * @author izabe
 */
public class CalcServerMain {

    /**
     * @param args the command line arguments
     */
     private static void printBoard(char[][] board) {
        int dim = board.length;
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
     
      private static boolean performMove(char[][] board, char activePlayer) {
        System.out.println(activePlayer + ", podaj nr wiersza");
        int row = new Scanner(System.in).nextInt();
        System.out.println(activePlayer + ", podaj nr kolumny");
        int col = new Scanner(System.in).nextInt();

        if (board[row][col] == 0) { // jeżeli pole jest wolne
            board[row][col] = activePlayer; // wstaw tam symbol obecnego gracza

            // jako że przeniosłem tę metodę to nie będę już tutaj
            // modyfikował licznika ruchów ani zmieniał gracza
            // zrobię to metodzie main

            return true; // zwracam true jezeli ruch sie udał
            // jeżeli kod dojdzie do tej linii to znaczy ze się udał
        } else { // ten else jest opcjonalny, wystarczyloby return false (dlaczego?)
            return false; // zwroce false, jeżeli miejsce było zajęte
        }

    }
      
    public static void main(String[] args) {
        // TODO code application logic here
        try {

//System.setProperty("java.security.policy", "file:/C:/Users/izabe/OneDrive/Pulpit/RSI/PS1/Zadanie1/RMIServer/security.policy");
System.setProperty("java.security.policy", "security.policy");

if (System.getSecurityManager() == null) {

System.setSecurityManager(new SecurityManager());

}


System.setProperty("java.rmi.server.codebase","file:/C:/Users/izabe/OneDrive/Pulpit/RSI/PS1/Zadanie2/CalcServer/build/classes/");


System.out.println("Codebase: " + System.getProperty("java.rmi.server.codebase"));
 LocateRegistry.createRegistry(1099);

CalcServerImpl obj1 = new CalcServerImpl();

Naming.rebind("//localhost/ABC", obj1);

System.out.println("Serwer oczekuje ...");
char [][] board=new char[3][3];
//board[1][1] = 'X';
//        board[2][2] = 'O';
//printBoard(board);

 int movesCounter = 0;
        char activePlayer = 'X';

        while (movesCounter < 9) {
            printBoard(board); // drukuje plansze zeby był widoczny rezultat
            System.out.println(activePlayer + ", podaj nr wiersza");
            int row = new Scanner(System.in).nextInt();
            System.out.println(activePlayer + ", podaj nr kolumny");
            int col = new Scanner(System.in).nextInt();

            if (board[row][col] == 0) { // jeżeli pole jest wolne
                board[row][col] = activePlayer; // wstaw tam symbol obecnego gracza
                movesCounter++; // zwiększam licznik ruchów o 1
                // zmieniam aktywnego gracza na przeciwnego
                activePlayer = activePlayer == 'X' ? 'O' : 'X';
            }
        }

 while(movesCounter<9){
            printBoard(board); // drukuje plansze zeby był widoczny rezultat
            boolean moveWasCorrect = performMove(board,activePlayer);
            if(moveWasCorrect){
                // zwiększam licznik ruchów o 1
                movesCounter++;
                // zmieniam aktywnego gracza na przeciwnego
                activePlayer = activePlayer == 'X' ? 'O' : 'X';
            } else {
                System.out.println("Ruch niepoprawny, spróbuj ponownie");
            }
        }
} catch (RemoteException | MalformedURLException e) {

e.printStackTrace();

}
    }
    
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ttt;

import java.rmi.Naming;
import java.rmi.registry.LocateRegistry;
import java.util.Scanner;

/**
 *
 * @author izabe
 */
public class TTTServer {
     public static void main(String[] args) {
     
          System.setProperty("java.security.policy", "security.policy");

        try {
        
             if (System.getSecurityManager() == null) {

                System.setSecurityManager(new SecurityManager());

            }
            
              System.setProperty("java.rmi.server.codebase", "file:/C:/Users/izabe/OneDrive/Pulpit/RSI/PS2/Zadanie3/TTTServer/build/classes/");
            System.setProperty("java.rmi.server.hostname", "192.168.0.114");
           // System.setProperty("java.rmi.server.hostname", "192.168.1.10");
            
            LocateRegistry.createRegistry(1099);
            
             Scanner scan = new Scanner(System.in);
            System.out.println("Podaj imie ");
            String name = scan.nextLine().trim();
            Board server=new Board(name,'X');
            
            Naming.rebind("//192.168.0.114/ABC", server);
            //  Naming.rebind("//192.168.1.10/ABC", server);

            System.out.println("[System] Poczekaj  na połączenie drugiego gracza");
            
            while(true){
                 boolean check=server.getActivePl();
                
                if (check==true){
                    
                      char[][]board=server.getBoard();
                    
	    			TTTServerInt client=server.getClient();//klient się zalogował
	    			server.printBoard(board); //wyświetli tablice
                                server.performMove(board);//prosi o ruch + zapisze ruch
                                //tu sprawdzenie czy wygrana
                                
	    			board=server.getBoard();
                              //  client.printBoard(board);
                              if(server.checkWinner(board)==true){
                                  server.printBoard(board);
                                  client.printBoard(board);
                                  server.send("Gracz "+server.getName()+" wygrał");
                                   client.send("Gracz "+server.getName()+" wygrał");
                                     
                              }
                              else{
                                  
                                 

                                   client.setBoard(board);
                                server.setActivePl(false);
                                client.setActivePl(true);
                            server.printBoard(board); 
                                 
                                 System.out.println("Ruch gracza "+client.getName());
                              }
                               
                    
                    
                   
	    		}
                 String msg=scan.nextLine().trim();
            }
        }
        catch (Exception e) {
            e.printStackTrace();
        }
     
     }
}

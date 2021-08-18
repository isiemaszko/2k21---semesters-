/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package ttt;

import java.rmi.Naming;
import java.util.Scanner;

/**
 *
 * @author izabe
 */
public class TTTClient {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
        
          System.setProperty("java.security.policy", "security.policy");

        try {
         if (System.getSecurityManager() == null) {

                System.setSecurityManager(new SecurityManager());

            }
            Scanner scan = new Scanner(System.in);
            System.out.println("Podaj imie ");
            String name = scan.nextLine().trim();
            
            TTTServerInt client = new Board(name,'O');

            TTTServerInt server = (TTTServerInt) Naming.lookup("//localhost/ABC");
            
            String msg = "Pierwszy ruch należy do gracza " + client.getName();
             server.send(msg);
             
              server.setClient(client);
              boolean win=false;
               while (true) {
                   boolean check=client.getActivePl();
                if (check==true){
                 
                      char[][]board=client.getBoard();
                    //  client.setBoard(board);
                      client.printBoard(board);
                      client.performMove(board);
                      
                      board=client.getBoard();
                      
                       if(client.checkWinner(board)==true){
                                  server.printBoard(board);
                                  client.printBoard(board);
                                  server.send("Gracz "+client.getName()+" wygrał");
                                   client.send("Gracz "+client.getName()+" wygrał");
                                   
                              }
                       else{


                            server.setBoard(board);
                      client.printBoard(board);
                      
                      client.setActivePl(false);
                      server.setActivePl(true);
                    
                         System.out.println("Ruch gracza "+server.getName());
                         
                     }
                     
                }
                 String msgg=scan.nextLine().trim();
                 
                          

            }
        
        }catch (Exception e) {

            e.printStackTrace();

        }
    }
    
}

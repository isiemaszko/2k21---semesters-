/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package chat;

import java.rmi.Naming;
import java.rmi.registry.LocateRegistry;
import java.util.List;
import java.util.Scanner;

/**
 *
 * @author izabe
 */
public class ChatServer {

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
            
              System.setProperty("java.rmi.server.codebase", "file:/C:/Users/izabe/OneDrive/Pulpit/RSI/PS2/Zadanie2/ChatServer/build/classes/");
            System.setProperty("java.rmi.server.hostname", "192.168.0.114");
            LocateRegistry.createRegistry(1099);
            Scanner scan = new Scanner(System.in);
            System.out.println("Podaj imie ");
            String name = scan.nextLine().trim();

            Chat server = new Chat(name);

           Naming.rebind("//192.168.0.114/ABC", server);

           System.out.println("[System] Chat Remote Object is ready:");
           
            
            while(true){
	    		String msg=scan.nextLine().trim();
	    		if (server.getClient()!=null){
	    			ChatServerInt client=server.getClient();
	    			msg="["+server.getName()+"] "+msg;
	    			client.send(msg);
	    		}	
	    	}

        } catch (Exception e) {

            e.printStackTrace();

        }
    }

}

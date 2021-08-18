/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package chat;

import java.rmi.Naming;
import java.util.List;
import java.util.Scanner;

/**
 *
 * @author izabe
 */
public class ChatClient {

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

            ChatServerInt client = new Chat(name);

            ChatServerInt server = (ChatServerInt) Naming.lookup("//localhost/ABC");

            String msg = "{" + client.getName() + "} polaczony";

            server.send(msg);

            System.out.println("Klient jest ready..");
            server.setClient(client);
            while (true) {
                msg = scan.nextLine().trim();

                msg = "{" + client.getName() + "} " + msg;
                server.send(msg);

            }

        } catch (Exception e) {

            e.printStackTrace();

        }
    }

}

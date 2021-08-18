/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package rmi;

/**
 *
 * @author izabe
 */
import java.rmi.Naming;
import java.util.List;
import java.util.Scanner;

public class RMIClient {

public static void main(String[] args) {

System.setProperty("java.security.policy", "security.policy");

System.setSecurityManager(new SecurityManager());

try {

MyServerInt myRemoteObject = (MyServerInt) Naming.lookup("//localhost/ABC");

String text = "Hallo :-)";

String result = myRemoteObject.getDescription(text);

System.out.println("Wysłano do servera: " + text);

System.out.println("Otrzymana z serwera odpowiedź: " + result);

List<Product> products=myRemoteObject.listaProduktow();
System.out.println("Lista produktów");
for(Product prod : products){
    System.out.println("Nazwa: " + prod.getName());
    System.out.println("Cena: " + prod.getPrice());
    System.out.println("///");
}
Scanner scan = new Scanner(System.in);
System.out.println("Wyszukaj produktu ");
String productName=scan.nextLine();

for(Product prod : products){
    if(prod.getName().equals(productName)){
        System.out.println("Szukany produkt to " + prod.getName()+ " o cenie "+ prod.getPrice()+"zł");
    }
    
   
}

} catch (Exception e) {

e.printStackTrace();

}}}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package calculator;
import java.rmi.Naming;
import java.util.Scanner;
/**
 *
 * @author izabe
 */
public class CalcClient {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
        System.setProperty("java.security.policy", "security.policy");

System.setSecurityManager(new SecurityManager());

try {

CalcServerInt myRemoteObject = (CalcServerInt) Naming.lookup("//localhost/ABC");
Scanner scan = new Scanner(System.in);
 System.out.println("Operacja ");
        String op=scan.nextLine();
         System.out.println("Podaj pierwszą liczbę ");
        int a=scan.nextInt();
       System.out.println("Podaj drugą liczbę ");
        int b=scan.nextInt();
        String result="";
        
            if(op.equals("+")){ result= myRemoteObject.addition(a,b); System.out.println("ad" + result);}
            else if(op.equals("-")) {result= myRemoteObject.subtraction(a,b);System.out.println("sub" + result);}
            else if(op.equals("*")) { result= myRemoteObject.multiplication(a,b);System.out.println("" + result);}
            else   {   result= myRemoteObject.division(a,b);System.out.println("" + result);}
        
      

        

} catch (Exception e) {

e.printStackTrace();

}
    }
    
}

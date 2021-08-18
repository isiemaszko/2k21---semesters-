/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package wsclient;

import java.net.ProxySelector;
import service.throwss.InvalidInputException_Exception;
import service.throwss.ShopInfo;
import service.throwss.ShopInfoService;

/**
 *
 * @author izabe
 */
public class WSClientThrows {
    
      public static void main(String[] args){
          try{
          ProxySelector.setDefault(new CustomProxySelector());
          
          ShopInfoService sIS=new ShopInfoService();
          ShopInfo si=sIS.getShopInfoPort();
            System.out.println("Odpowiedź "+si.getShopInfo("zabela"));
          }
          catch(InvalidInputException_Exception e){
               System.out.println("Error: Message: "+e.getFaultInfo().getMessage());
        
          }
      }
}

package service.throwss;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */


import javax.jws.WebMethod;
import javax.jws.WebService;

/**
 *
 * @author izabe
 */
@WebService
public class ShopInfo {
    @WebMethod
    public String getShopInfo(String name) throws InvalidInputException{
        String outText="";
        if(name.equals("Izabela")){
            outText="Akceptuje "+name;
        }
        else{
        throw new InvalidInputException("Nie właściwe dane wejściowe ",name+" nazwa");
        }
        return outText;
    }
}

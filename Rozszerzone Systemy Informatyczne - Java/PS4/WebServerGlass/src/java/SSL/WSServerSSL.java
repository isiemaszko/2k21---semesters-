/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package SSL;

import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.jws.WebService;

/**
 *
 * @author izabe
 */
@WebService
public class WSServerSSL {
    @WebMethod(operationName="hello")
    public String SayHello(@WebParam(name="name")String name){
        return "Server: Hello "+name+" !";
    }
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package mtom.wsserver;

import java.awt.Image;
import java.io.File;
import java.io.IOException;
import javax.imageio.ImageIO;
import javax.jws.WebMethod;
import javax.jws.WebService;
import javax.jws.soap.SOAPBinding;
import javax.xml.ws.BindingType;
import javax.xml.ws.soap.MTOM;

/**
 *
 * @author izabe
 */
@MTOM
@WebService
//@BindingType(value=SOAPBinding.SOAP11HTTP_MTOM_BINDING)
public class MTOMWSServer {

    /**
     * @param args the command line arguments
     */
    @WebMethod
    public String echo(String text){
        return "Hello "+text;
    }
    
    public Image downloadImage(String name){
        try{
            File image=new File("C:\\Users\\izabe\\OneDrive\\Pulpit\\RSI\\PS6\\images\\"+name);
            return ImageIO.read(image);
            
        }
        catch(IOException e){
            e.printStackTrace();
            return null;
        }
    }
    
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package MTOM;

import com.itextpdf.text.Paragraph;
import com.itextpdf.text.Document;
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.pdf.PdfWriter;
import java.awt.Image;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
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
                  System.out.println("image -server");
           // return document;
            return ImageIO.read(image);
            
        }
        catch(IOException e){
            e.printStackTrace();
            return null;
        }
    }
    
    public void getPDFFile(){
        try{
            Document document=new Document();
            PdfWriter.getInstance(document, new FileOutputStream("C:\\Users\\izabe\\OneDrive\\Pulpit\\RSI\\PS6\\images\\iTextHelloWorld.pdf"));

             document.add(new Paragraph("Hello "));
            document.close();
      
        }
        catch(IOException e){
            e.printStackTrace();
          //  return null;
        } catch (DocumentException ex) {
            Logger.getLogger(MTOMWSServer.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
}

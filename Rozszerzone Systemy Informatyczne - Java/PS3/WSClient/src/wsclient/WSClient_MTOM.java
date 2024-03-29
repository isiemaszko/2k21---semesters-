/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package wsclient;

import com.itextpdf.text.BadElementException;
import com.itextpdf.text.BaseColor;
import com.itextpdf.text.Document;
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.Element;
import com.itextpdf.text.Font;
import com.itextpdf.text.Paragraph;
import com.itextpdf.text.Phrase;
import com.itextpdf.text.Section;
import com.itextpdf.text.pdf.PdfPCell;
import com.itextpdf.text.pdf.PdfPTable;
import com.itextpdf.text.pdf.PdfWriter;
import java.awt.Desktop;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.ProxySelector;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.xml.ws.soap.MTOMFeature;
import mtom.MTOMWSServer;
import mtom.MTOMWSServerService;

/**
 *
 * @author izabe
 */
public class WSClient_MTOM {
     public static void main(String[] args) throws DocumentException, FileNotFoundException{
          
          ProxySelector.setDefault(new CustomProxySelector());
          
          MTOMWSServerService service=new MTOMWSServerService();
          MTOMWSServer port=service.getMTOMWSServerPort(new MTOMFeature());
//                    byte[] image=port.downloadImage("kot.jpg");
//          
//          JFrame frame =new JFrame();
//          frame.setSize(300,300);
//          JLabel label=new JLabel(new ImageIcon(image));
//          frame.add(label);
//          frame.setVisible(true);

          //  port.getPDFFile();
       byte[] b=getPDF();
      }
      private static Font catFont = new Font(Font.FontFamily.TIMES_ROMAN, 18,
            Font.BOLD);
    private static Font subFont = new Font(Font.FontFamily.TIMES_ROMAN, 16,
            Font.BOLD);
    private static Font smallBold = new Font(Font.FontFamily.TIMES_ROMAN, 12,
            Font.BOLD);

     public static byte[] getPDF() throws DocumentException {
       try{
         Document document=new Document();
            PdfWriter.getInstance(document, new FileOutputStream("C:\\Users\\izabe\\OneDrive\\Pulpit\\RSI\\PS6\\images\\iTextHelloWorld.pdf"));
            document.open();
            
        Paragraph title = new Paragraph();
        addEmptyLine(title, 1);
        title.add(new Paragraph("Potwierdzenie rezerwacji numer X", catFont));
         addEmptyLine(title, 3);
        // Will create: Report generated by: _name, _date
        title.add(new Paragraph("Szanowny username!", subFont));
          addEmptyLine(title, 1);
          title.add(new Paragraph("Serdecznie dziekujemy za okazanie zainteresowania naszym hotelem i z przyjemnoscia potwierdzamy rezerwacje w terminie +data_od - +data_do", subFont)); 
         addEmptyLine(title, 1);
          title.add(new Paragraph("Szczególy rezerwacji:", subFont)); 
            addEmptyLine(title, 2);
          
          document.add(title);
             
          //tabela 
          Paragraph details =new Paragraph();
         
             createTable(details);
               addEmptyLine(details, 2);
             document.add(details);
            
               Paragraph notes=new Paragraph();
            notes.add(new Paragraph("Dodatkowe uwagi:", subFont));
            notes.add(new Paragraph("--uwagi--", smallBold));
            document.add(notes);
            
            
            
            document.close();
            
        
            
     
            File file=new File("C:\\Users\\izabe\\OneDrive\\Pulpit\\RSI\\PS6\\images\\iTextHelloWorld.pdf");
            byte[] b=Files.readAllBytes(Paths.get("C:\\Users\\izabe\\OneDrive\\Pulpit\\RSI\\PS6\\images\\iTextHelloWorld.pdf"));
            return b;
       }
       catch(IOException e){
       e.printStackTrace();
       return null;}
     }
     private static void createTable(Paragraph par)
            throws BadElementException {
        PdfPTable table = new PdfPTable(5);

        // t.setBorderColor(BaseColor.GRAY);
        // t.setPadding(4);
        // t.setSpacing(4);
        // t.setBorderWidth(1);

        PdfPCell c1 = new PdfPCell(new Phrase("Numer pokoju"));
        c1.setHorizontalAlignment(Element.ALIGN_LEFT);
        table.addCell(c1);

        c1 = new PdfPCell(new Phrase("Numer pietra"));
        c1.setHorizontalAlignment(Element.ALIGN_LEFT);
        table.addCell(c1);

        c1 = new PdfPCell(new Phrase("Lazienka"));
        c1.setHorizontalAlignment(Element.ALIGN_LEFT);
        table.addCell(c1);
        
         c1 = new PdfPCell(new Phrase("Max. liczba osób"));
        c1.setHorizontalAlignment(Element.ALIGN_LEFT);
        table.addCell(c1); 
        
         c1 = new PdfPCell(new Phrase("Powierzchnia"));
        c1.setHorizontalAlignment(Element.ALIGN_LEFT);
        table.addCell(c1);
        table.setHeaderRows(1);

        table.addCell("1.0");
        table.addCell("1.1");
        table.addCell("1.2");
        table.addCell("1.0");
        table.addCell("1.1");
        
        table.addCell("2.1");
        table.addCell("2.2");
        table.addCell("2.3");
        table.addCell("2.2");
        table.addCell("2.3");
        
        par.add(table);

    }
     private static void addEmptyLine(Paragraph paragraph, int number) {
        for (int i = 0; i < number; i++) {
            paragraph.add(new Paragraph(" "));
        }
    }
     
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package resources;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import model.Message;

/**
 *
 * @author izabe
 */
public class MessageService {
    private List<Message> list=new ArrayList<>();
    private static  HashMap<Long, Message> messages=new HashMap<Long, Message>();
    
//    public MessageService(){
//        Message m1=new Message(1L, "Pierwsza wiadomość","Iza");
//         Message m2=new Message(1L, "Druga wiadomość","Łukasz");
//          Message m3=new Message(1L, "Trzecia wiadomość","Julia");
//         
//        list.add(m1);
//        list.add(m2);
//        list.add(m3);
//    }
//    public List<Message> getAllMessages(){ return list;}
    
    public MessageService(){
        messages.put(1L, new Message(1L,"Pierwsza wiadomość","Iza"));
        messages.put(2L,  new Message(2L,"Druga wiadomość","Łukasz"));
        messages.put(3L, new Message(3L, "Trzecia wiadomość","Julia"));
    }
    
     public List<Message> getAllMessages(){ return new ArrayList<Message>(messages.values());}

     public Message getMessage(long id){
         return messages.get(id);
     }
     
     public Message createMessage(Message message){
         message.setId(messages.size()+1L);
         message.setCreated();
         messages.put(messages.size()+1L, message);
         return message;
     }
     
     public Message updateMessage(Message message,long id){
            Message mes=getMessage(message.getId());
            mes.setAuthor(message.getAuthor());
            mes.setMessage(message.getMessage());
            mes.setCreated();
            messages.replace(1L, mes);
            return mes;
            
        }
     
     public void deleteMessage(long  id){
         messages.remove(id,getMessage(id));
     }
     
//   public List<Message> getAllMessagesStartingWith(String perl){
//       ArrayList<Message> list= new ArrayList<Message>();
//       for(int i=0;i<messages.size();i++){
//           
//       }
//   }
}

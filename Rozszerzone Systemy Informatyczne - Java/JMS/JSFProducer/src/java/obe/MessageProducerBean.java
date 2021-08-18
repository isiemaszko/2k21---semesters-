/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package obe;

import com.sun.istack.logging.Logger;
import javax.annotation.Resource;
import javax.inject.Named;
import javax.enterprise.context.RequestScoped;
import javax.faces.application.FacesMessage;
import javax.faces.context.FacesContext;
import javax.inject.Inject;
import javax.jms.Connection;
import javax.jms.ConnectionFactory;
import javax.jms.JMSConnectionFactory;
import javax.jms.JMSContext;
import javax.jms.JMSException;
import javax.jms.Message;
import javax.jms.MessageProducer;
import javax.jms.Queue;
import javax.jms.Session;
import javax.jms.TextMessage;

/**
 *
 * @author izabe
 */
//http://localhost:8080/JSFProducer/
@Named(value = "messageProducerBean")
@RequestScoped
public class MessageProducerBean {

    @Resource(mappedName = "jms/myqueue")
    private Queue myqueue;

//    @Inject
//    @JMSConnectionFactory("jms/myqueueFactory")
//    private JMSContext context;



    @Resource(mappedName="jms/myqueueFactory")
    private ConnectionFactory myqueueFactory;
    private String message;
    /**
     * Creates a new instance of MessageProducerBean
     */
    public MessageProducerBean() {
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
   public void send(){
     System.out.println("Test IS "+message);
      System.out.println("Sending message: "+message);
    FacesContext facesContext = FacesContext.getCurrentInstance();
try {
     sendJMSMessageToMyqueue(message);
     FacesMessage facesMessage = new FacesMessage("Message sent: " + message);
     facesMessage.setSeverity(FacesMessage.SEVERITY_INFO);
     facesContext.addMessage(null, facesMessage);
} catch (JMSException jmse) {
     FacesMessage facesMessage = new FacesMessage("Message NOT sent: " + message);
     facesMessage.setSeverity(FacesMessage.SEVERITY_ERROR);
    facesContext.addMessage(null, facesMessage);
}
    }
    
//     public void send(){
//         System.out.println("Test IS "+message);
//         JMSContext cont=myqueueFactory.createContext();
//         System.out.println("Sending message: "+message);
//         cont.createProducer().send(myqueue, message);
//         
//         FacesContext fc=FacesContext.getCurrentInstance();
//         FacesMessage fm=new FacesMessage("Mes sent: "+message);
//         fm.setSeverity(FacesMessage.SEVERITY_INFO);
//         fc.addMessage(null, fm);
//    }
     
public Message createJMSMessageForjmsMyqueue(Session session, Object messageData) throws JMSException {
    TextMessage tm=session.createTextMessage();
    tm.setText(messageData.toString());
    return tm;
}
    private void sendJMSMessageToMyqueue(Object messageData) throws JMSException{
       Connection connection=null;
       Session session=null;
       try{
       connection=myqueueFactory.createConnection();
       session=connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
       MessageProducer mP=session.createProducer(myqueue);
       mP.send(createJMSMessageForjmsMyqueue(session,messageData));
               
               }
       finally{
       if(session!=null){
           try{session.close();}
           catch(JMSException e){}
       }
       if(connection!=null){
       connection.close();}
       }
    }

   
}

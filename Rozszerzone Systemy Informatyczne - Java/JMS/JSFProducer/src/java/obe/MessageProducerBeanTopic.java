/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package obe;

import javax.annotation.Resource;
import javax.inject.Named;
import javax.enterprise.context.RequestScoped;
import javax.inject.Inject;
import javax.jms.JMSConnectionFactory;
import javax.jms.JMSContext;
import javax.jms.Topic;

/**
 *
 * @author izabe
 */
@Named(value = "messageProducerBeanTopic")
@RequestScoped
public class MessageProducerBeanTopic {

     private String name;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
    @Resource(mappedName = "jms/myTopic")
    private Topic myTopic;

    @Inject
    @JMSConnectionFactory("java:comp/DefaultJMSConnectionFactory")
    private JMSContext context;

    /**
     * Creates a new instance of MessageProducerBeanTopic
     */
    public MessageProducerBeanTopic() {
    }

    public void sendJMSMessageToMyTopic(String messageData) {
        context.createProducer().send(myTopic, messageData);
    }
    
    public void sendName(){
        
         System.out.println("Test topic IS "+name);
      System.out.println("Sending topic message: "+name);
        this.sendJMSMessageToMyTopic(this.name);
    }
    
}

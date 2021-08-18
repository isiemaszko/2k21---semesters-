/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.mdb;

import javax.ejb.ActivationConfigProperty;
import javax.ejb.MessageDriven;
import javax.jms.Message;
import javax.jms.MessageListener;
import javax.jms.TextMessage;
import javax.jms.JMSException;
/**
 *
 * @author izabe
 */
@MessageDriven(activationConfig = {
@ActivationConfigProperty(propertyName = "destinationLookup", propertyValue = "jms/myqueue"),
@ActivationConfigProperty(propertyName = "destinationType", propertyValue = "javax.jms.Queue")
})
public class NewMessageBean implements MessageListener {
    
    public NewMessageBean() {
    }
    
    @Override
    public void onMessage(Message message) {
        try{
        TextMessage tm=(TextMessage) message;
        System.out.println("Consumed mes: "+tm.getText());
        }
        catch(JMSException ex){
        System.out.println("JMSException: "+ex);
        }
    }
    
}

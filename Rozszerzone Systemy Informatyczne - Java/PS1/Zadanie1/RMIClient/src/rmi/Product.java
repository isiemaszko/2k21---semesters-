/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package rmi;

import java.io.Serializable;

/**
 *
 * @author izabe
 */
public class Product implements Serializable {
    String name;
    double price;
    
    public Product(String name, float price){
        this.name=name;
        this.price=price;
    }
    
    public String getName(){
        return name;}
    
     public double getPrice(){
        return price;}
     
     public void setName(String name){
        this.name=name;}
    
     public void setPrice(float price){
        this.price=price;}
    
}

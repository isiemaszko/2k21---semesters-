/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package model;

import javax.xml.bind.annotation.XmlRootElement;


/**
 *
 * @author izabe
 */
//@XmlRootElement
public class Product{
    private String name;
    private String company;
    private float price;
    
    public Product(){}
    public Product(String name, String company,float price){
        this.name=name;
         this.company=company;
        this.price=price;
    }
    
    public String getName(){
        return name;}
    public String getCompany(){
        return company;}
    
     public float getPrice(){
        return price;}
     
     public void setName(String name){
        this.name=name;}
    
     public void setPrice(float price){
        this.price=price;}
     
     public void setCompany(String name){
        this.company=name;}
    
    
    
}

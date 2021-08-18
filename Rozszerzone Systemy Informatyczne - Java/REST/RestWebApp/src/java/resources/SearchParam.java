/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package resources;

import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author izabe
 */
@XmlRootElement
public class SearchParam {
    String name;
    String company;
    float priceLessThan;
        
    public String getName(){
        return name;}
    public String getCompany(){
        return company;}
    
     public float getPriceLessThan(){
        return priceLessThan;}
     
     public void setName(String name){
        this.name=name;}
    
     public void setPriceLessThan(float price){
        this.priceLessThan=price;}
     
     public void setCompany(String name){
        this.company=name;}
}

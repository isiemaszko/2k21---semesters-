/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package model;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 *
 * @author izabe
 */   
public class Products {
     private static List<Product> products=new ArrayList<>();
    //  private static  HashMap<Long, Product> products=new HashMap<Long, Product>();
     public Products(){
         products.clear();
         products.add(new Product("chleb","Okruszek", (float) 1.25));
         products.add(new Product("mleko","Mlekowita", (float)3.75));
         products.add(new Product("ser","Mlekowita", (float) 2.45));
         products.add(new Product("woda","Cisowianka", (float)1.55));
     }
    public List<Product> getAllProducts() {
        return new ArrayList<>(products);
    }
    
    public List<Product> findProduktyByNazwaMniejszaNiz(String company, float price){
        List<Product> list=new ArrayList<>();
        for(Product p : products){
            if(p.getCompany().equals(company) && p.getPrice()<price){
                list.add(p);
            }
        }
        return list;
    }
}

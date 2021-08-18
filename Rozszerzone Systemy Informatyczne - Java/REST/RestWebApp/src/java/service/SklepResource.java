/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package service;

import java.util.List;
import javax.ws.rs.Consumes;
import javax.ws.rs.Produces;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PUT;
import javax.ws.rs.core.MediaType;
import model.Product;
import model.Products;
import resources.ResponseList;
import resources.SearchParam;

/**
 * REST Web Service
 *
 * @author izabe
 */
@Path("/sklep")
public class SklepResource {

    Products products=new Products();
  
    @GET
    @Path("/allproducts")
    @Produces(MediaType.APPLICATION_JSON)
    public  ResponseList getAllProducts() {
        //TODO return proper representation object
        List<Product> allProducts=products.getAllProducts();
               
        ResponseList responseList =new ResponseList();
        responseList.setList(allProducts);
        return responseList;

    }

    @POST
    @Path("/findProducts")
    @Produces(MediaType.APPLICATION_JSON)
    @Consumes(MediaType.APPLICATION_JSON)
    public ResponseList findProducts(SearchParam searchElem){
        ResponseList responseList =new ResponseList();
        List<Product> resList=products.findProduktyByNazwaMniejszaNiz(searchElem.getCompany(), searchElem.getPriceLessThan());
        responseList.setList(resList);
        return responseList;
    }
   
    
}

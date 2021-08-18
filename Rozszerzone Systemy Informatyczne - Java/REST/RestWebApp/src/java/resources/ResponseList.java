/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package resources;

import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import model.Product;

/**
 *
 * @author izabe
 */

@XmlRootElement
@XmlAccessorType(XmlAccessType.NONE)
public class ResponseList {
    @XmlElement(name="products")
    private List<Product> list;
    
    public List<Product> getList(){return list;}
    public void setList(List<Product> list){this.list=list;}
}

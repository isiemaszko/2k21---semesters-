/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package model;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author izabe
 */
@XmlRootElement
public class Message {

    private long id;
    private String message;
    private Date created;
    private String author;
    private List<Link> links = new ArrayList<Link>();

    public Message() {
    }

    public Message(long id, String mes, String auth) {
        this.id = id;
        this.message = mes;
        this.author = auth;
        this.created = new Date();
    }

    public long getId() {
        return id;
    }

    public String getMessage() {
        return message;
    }

    public Date getCreated() {
        return created;
    }

    public String getAuthor() {
        return author;
    }

    public void setId(long id) {
        this.id = id;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public void setCreated() {
        this.created = new Date();
    }

    public void setAuthor(String author) {
        this.author = author;
    }

    public List<Link> getLinks() {
        return links;
    }

    public void setLinks(List<Link> links) {
        this.links = links;
    }

    public void addLink(String uri, String rel) {
        Link link=new Link();
        link.setLink(uri);
        link.setRel(rel);
        boolean isThere=false;
        for(Link l:links){
            if((l.getRel()).equals(link.getRel())){
                isThere=true;
                System.out.print("znalezniono link");
            }
        }
        if(!isThere) links.add(link);
         //To change body of generated methods, choose Tools | Templates.
    }

}

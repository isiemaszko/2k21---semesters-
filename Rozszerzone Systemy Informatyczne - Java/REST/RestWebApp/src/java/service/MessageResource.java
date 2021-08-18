/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package service;

import java.net.URI;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.HeaderParam;
import javax.ws.rs.MatrixParam;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.UriInfo;
import model.Message;
import resources.MessageService;

/**
 *
 * @author izabe
 */
@Path("/messages")
public class MessageResource {
    MessageService messageService=new MessageService();
    
    @Context
    private UriInfo uriinfo;
     
    @GET
    @Produces(MediaType.APPLICATION_XML)
    public List<Message> getText(){ return messageService.getAllMessages();}
    
 
    
    @GET
    @Path("/json")
    @Produces(MediaType.APPLICATION_JSON) 
    public List<Message> getTextJson() {
        return messageService.getAllMessages();
    }
    
    @GET
    @Path("/{messageId}")
    @Produces(MediaType.APPLICATION_JSON)
    public Message getMessage(@PathParam("messageId") long id) {
    return messageService.getMessage(id);
    }
    
    @POST
    @Consumes(MediaType.APPLICATION_JSON)
      @Produces(MediaType.APPLICATION_JSON)
    public Message createMessage( Message message) {
    //return "post test";
    return messageService.createMessage(message);
}
    
    
        @PUT
    @Path("/{messageId}")
    @Consumes(MediaType.APPLICATION_JSON)
    @Produces(MediaType.APPLICATION_JSON)
    public Message updateMessage(Message message,@PathParam("messageId") long id) {
    //return "post test";
    return messageService.updateMessage(message,id);
    }
    
    @DELETE
    @Path("/{messageId}")
     public void deleteMessage(@PathParam("messageId") long id) {
    //return "post test";
         messageService.deleteMessage(id);
    }
     
     @GET
      @Path("/query")
    @Produces(MediaType.APPLICATION_JSON)
    public Message getMessages(@QueryParam("id") long id ) {
    return messageService.getMessage(id);
   }
    
    @GET
     @Path("/headers")
    @Produces(MediaType.TEXT_HTML)
    public String getHeaders(@HeaderParam("Host") String host){
        return "Host: "+host;
    }
    
    @GET
   @Path("/matrix/{model}")
    @Produces(MediaType.TEXT_HTML)
   public String getPicture(@PathParam("model") String model,
                          @MatrixParam("color") String color,
                           @MatrixParam("year") String year) {
      return "model "+model+" color "+color+" year "+year;
   }

   @GET
   @Produces(MediaType.APPLICATION_JSON)
   public List<Message> getMessages(@QueryParam("zaczynasie") String parl ) {
       if(parl!=null){
           return messageService.getAllMessagesStartingWith(parl);
       }
       return messageService.getAllMessages();
   }
   
   //sub-resources
   @GET
   @Path("{id}/{comments}")
   @Produces("text/xml")
   public String getComments(@PathParam("comments")String com,@PathParam("id")long id){
       Message mes=messageService.getMessage(id);
       return mes.getMessage()+", comment: "+com;
   }
   
   //header->location
    @POST
    @Path("/headers")
    @Consumes(MediaType.APPLICATION_JSON)
    @Produces(MediaType.APPLICATION_JSON)
    public Response createMessagePathLocation( Message message) {
    //return "post test";
    Message newMes=messageService.createMessage(message);
    String newId=String.valueOf(newMes.getId());
    URI uri=uriinfo.getAbsolutePathBuilder().path(newId).build();
    Response res=Response.created(uri)
            .entity(newMes)
            .build();
    return res;
}
    
    //hateoas
     @GET
    @Path("/hateoas/{messageId}")
    @Produces(MediaType.APPLICATION_JSON)
    public Message getMessageHeteoas(@PathParam("messageId") long id) {
        Message newMes=messageService.getMessage(id);
        String uri=uriinfo.getBaseUriBuilder()
                .path(MessageResource.class)
                .path(String.valueOf(newMes.getId()))
                .build()
                .toString();
        newMes.addLink(uri,"self");
        
        return newMes;
    }
}

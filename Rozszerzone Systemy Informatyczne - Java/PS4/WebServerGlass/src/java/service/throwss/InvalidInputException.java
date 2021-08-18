package service.throwss;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */


/**
 *
 * @author izabe
 */
class InvalidInputException extends Exception {

    String errorDetails;
    public InvalidInputException(String reason, String errorDetails) {
        super(reason);
        this.errorDetails=reason;
         }
    
    public String getFaultinfo(){
        return errorDetails;
    }
    
}

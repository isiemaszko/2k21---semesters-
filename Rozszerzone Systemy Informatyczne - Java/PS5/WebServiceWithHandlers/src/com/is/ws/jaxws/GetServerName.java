/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.is.ws.jaxws;

/**
 *
 * @author izabe
 */
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "getServerName", namespace = "http://ws.is.com/")
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "getServerName", namespace = "http://ws.is.com/")
public class GetServerName {
}


package com.is.ws;

import java.net.MalformedURLException;
import java.net.URL;
import javax.jws.HandlerChain;
import javax.xml.namespace.QName;
import javax.xml.ws.Service;
import javax.xml.ws.WebEndpoint;
import javax.xml.ws.WebServiceClient;
import javax.xml.ws.WebServiceException;
import javax.xml.ws.WebServiceFeature;


/**
 * This class was generated by the JAX-WS RI.
 * JAX-WS RI 2.2.6-1b01 
 * Generated source version: 2.2
 * 
 */
@WebServiceClient(name = "ServerInfoService", 
        targetNamespace = "http://ws.is.com/", 
        wsdlLocation = "http://localhost:8888/ws/server?wsdl")
@HandlerChain(file="handler-chain.xml")
public class ServerInfoService
    extends Service
{

    private final static URL SERVERINFOSERVICE_WSDL_LOCATION;
    private final static WebServiceException SERVERINFOSERVICE_EXCEPTION;
    private final static QName SERVERINFOSERVICE_QNAME = new QName("http://ws.is.com/", "ServerInfoService");

    static {
        URL url = null;
        WebServiceException e = null;
        try {
            url = new URL("http://localhost:8888/ws/server?wsdl");
        } catch (MalformedURLException ex) {
            e = new WebServiceException(ex);
        }
        SERVERINFOSERVICE_WSDL_LOCATION = url;
        SERVERINFOSERVICE_EXCEPTION = e;
    }

    public ServerInfoService() {
        super(__getWsdlLocation(), SERVERINFOSERVICE_QNAME);
    }

    public ServerInfoService(WebServiceFeature... features) {
        super(__getWsdlLocation(), SERVERINFOSERVICE_QNAME, features);
    }

    public ServerInfoService(URL wsdlLocation) {
        super(wsdlLocation, SERVERINFOSERVICE_QNAME);
    }

    public ServerInfoService(URL wsdlLocation, WebServiceFeature... features) {
        super(wsdlLocation, SERVERINFOSERVICE_QNAME, features);
    }

    public ServerInfoService(URL wsdlLocation, QName serviceName) {
        super(wsdlLocation, serviceName);
    }

    public ServerInfoService(URL wsdlLocation, QName serviceName, WebServiceFeature... features) {
        super(wsdlLocation, serviceName, features);
    }

    /**
     * 
     * @return
     *     returns ServerInfo
     */
    @WebEndpoint(name = "ServerInfoPort")
    public ServerInfo getServerInfoPort() {
        return super.getPort(new QName("http://ws.is.com/", "ServerInfoPort"), ServerInfo.class);
    }

    /**
     * 
     * @param features
     *     A list of {@link javax.xml.ws.WebServiceFeature} to configure on the proxy.  Supported features not in the <code>features</code> parameter will have their default values.
     * @return
     *     returns ServerInfo
     */
    @WebEndpoint(name = "ServerInfoPort")
    public ServerInfo getServerInfoPort(WebServiceFeature... features) {
        return super.getPort(new QName("http://ws.is.com/", "ServerInfoPort"), ServerInfo.class, features);
    }

    private static URL __getWsdlLocation() {
        if (SERVERINFOSERVICE_EXCEPTION!= null) {
            throw SERVERINFOSERVICE_EXCEPTION;
        }
        return SERVERINFOSERVICE_WSDL_LOCATION;
    }

}

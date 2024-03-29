
package mtom;

import java.net.MalformedURLException;
import java.net.URL;
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
@WebServiceClient(name = "MTOMWSServerService", targetNamespace = "http://MTOM/", wsdlLocation = "http://localhost:8080/WebServerGlass/MTOMWSServerService?WSDL")
public class MTOMWSServerService
    extends Service
{

    private final static URL MTOMWSSERVERSERVICE_WSDL_LOCATION;
    private final static WebServiceException MTOMWSSERVERSERVICE_EXCEPTION;
    private final static QName MTOMWSSERVERSERVICE_QNAME = new QName("http://MTOM/", "MTOMWSServerService");

    static {
        URL url = null;
        WebServiceException e = null;
        try {
            url = new URL("http://localhost:8080/WebServerGlass/MTOMWSServerService?WSDL");
        } catch (MalformedURLException ex) {
            e = new WebServiceException(ex);
        }
        MTOMWSSERVERSERVICE_WSDL_LOCATION = url;
        MTOMWSSERVERSERVICE_EXCEPTION = e;
    }

    public MTOMWSServerService() {
        super(__getWsdlLocation(), MTOMWSSERVERSERVICE_QNAME);
    }

    public MTOMWSServerService(WebServiceFeature... features) {
        super(__getWsdlLocation(), MTOMWSSERVERSERVICE_QNAME, features);
    }

    public MTOMWSServerService(URL wsdlLocation) {
        super(wsdlLocation, MTOMWSSERVERSERVICE_QNAME);
    }

    public MTOMWSServerService(URL wsdlLocation, WebServiceFeature... features) {
        super(wsdlLocation, MTOMWSSERVERSERVICE_QNAME, features);
    }

    public MTOMWSServerService(URL wsdlLocation, QName serviceName) {
        super(wsdlLocation, serviceName);
    }

    public MTOMWSServerService(URL wsdlLocation, QName serviceName, WebServiceFeature... features) {
        super(wsdlLocation, serviceName, features);
    }

    /**
     * 
     * @return
     *     returns MTOMWSServer
     */
    @WebEndpoint(name = "MTOMWSServerPort")
    public MTOMWSServer getMTOMWSServerPort() {
        return super.getPort(new QName("http://MTOM/", "MTOMWSServerPort"), MTOMWSServer.class);
    }

    /**
     * 
     * @param features
     *     A list of {@link javax.xml.ws.WebServiceFeature} to configure on the proxy.  Supported features not in the <code>features</code> parameter will have their default values.
     * @return
     *     returns MTOMWSServer
     */
    @WebEndpoint(name = "MTOMWSServerPort")
    public MTOMWSServer getMTOMWSServerPort(WebServiceFeature... features) {
        return super.getPort(new QName("http://MTOM/", "MTOMWSServerPort"), MTOMWSServer.class, features);
    }

    private static URL __getWsdlLocation() {
        if (MTOMWSSERVERSERVICE_EXCEPTION!= null) {
            throw MTOMWSSERVERSERVICE_EXCEPTION;
        }
        return MTOMWSSERVERSERVICE_WSDL_LOCATION;
    }

}

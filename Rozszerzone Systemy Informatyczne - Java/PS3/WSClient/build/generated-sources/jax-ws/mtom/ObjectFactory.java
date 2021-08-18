
package mtom;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlElementDecl;
import javax.xml.bind.annotation.XmlRegistry;
import javax.xml.namespace.QName;


/**
 * This object contains factory methods for each 
 * Java content interface and Java element interface 
 * generated in the mtom package. 
 * <p>An ObjectFactory allows you to programatically 
 * construct new instances of the Java representation 
 * for XML content. The Java representation of XML 
 * content can consist of schema derived interfaces 
 * and classes representing the binding of schema 
 * type definitions, element declarations and model 
 * groups.  Factory methods for each of these are 
 * provided in this class.
 * 
 */
@XmlRegistry
public class ObjectFactory {

    private final static QName _EchoResponse_QNAME = new QName("http://MTOM/", "echoResponse");
    private final static QName _DownloadImage_QNAME = new QName("http://MTOM/", "downloadImage");
    private final static QName _Echo_QNAME = new QName("http://MTOM/", "echo");
    private final static QName _GetPDFFile_QNAME = new QName("http://MTOM/", "getPDFFile");
    private final static QName _DownloadImageResponse_QNAME = new QName("http://MTOM/", "downloadImageResponse");
    private final static QName _GetPDFFileResponse_QNAME = new QName("http://MTOM/", "getPDFFileResponse");

    /**
     * Create a new ObjectFactory that can be used to create new instances of schema derived classes for package: mtom
     * 
     */
    public ObjectFactory() {
    }

    /**
     * Create an instance of {@link GetPDFFileResponse }
     * 
     */
    public GetPDFFileResponse createGetPDFFileResponse() {
        return new GetPDFFileResponse();
    }

    /**
     * Create an instance of {@link GetPDFFile }
     * 
     */
    public GetPDFFile createGetPDFFile() {
        return new GetPDFFile();
    }

    /**
     * Create an instance of {@link DownloadImageResponse }
     * 
     */
    public DownloadImageResponse createDownloadImageResponse() {
        return new DownloadImageResponse();
    }

    /**
     * Create an instance of {@link Echo }
     * 
     */
    public Echo createEcho() {
        return new Echo();
    }

    /**
     * Create an instance of {@link EchoResponse }
     * 
     */
    public EchoResponse createEchoResponse() {
        return new EchoResponse();
    }

    /**
     * Create an instance of {@link DownloadImage }
     * 
     */
    public DownloadImage createDownloadImage() {
        return new DownloadImage();
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link EchoResponse }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://MTOM/", name = "echoResponse")
    public JAXBElement<EchoResponse> createEchoResponse(EchoResponse value) {
        return new JAXBElement<EchoResponse>(_EchoResponse_QNAME, EchoResponse.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link DownloadImage }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://MTOM/", name = "downloadImage")
    public JAXBElement<DownloadImage> createDownloadImage(DownloadImage value) {
        return new JAXBElement<DownloadImage>(_DownloadImage_QNAME, DownloadImage.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link Echo }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://MTOM/", name = "echo")
    public JAXBElement<Echo> createEcho(Echo value) {
        return new JAXBElement<Echo>(_Echo_QNAME, Echo.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link GetPDFFile }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://MTOM/", name = "getPDFFile")
    public JAXBElement<GetPDFFile> createGetPDFFile(GetPDFFile value) {
        return new JAXBElement<GetPDFFile>(_GetPDFFile_QNAME, GetPDFFile.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link DownloadImageResponse }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://MTOM/", name = "downloadImageResponse")
    public JAXBElement<DownloadImageResponse> createDownloadImageResponse(DownloadImageResponse value) {
        return new JAXBElement<DownloadImageResponse>(_DownloadImageResponse_QNAME, DownloadImageResponse.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link GetPDFFileResponse }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://MTOM/", name = "getPDFFileResponse")
    public JAXBElement<GetPDFFileResponse> createGetPDFFileResponse(GetPDFFileResponse value) {
        return new JAXBElement<GetPDFFileResponse>(_GetPDFFileResponse_QNAME, GetPDFFileResponse.class, null, value);
    }

}

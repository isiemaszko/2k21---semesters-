using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebServiceWCF
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę interfejsu „IHelloWorld” w kodzie i pliku konfiguracji.
    [ServiceContract]
    public interface IHelloWorld
    {
        [OperationContract]
        string getMessage(string text);
    }
}

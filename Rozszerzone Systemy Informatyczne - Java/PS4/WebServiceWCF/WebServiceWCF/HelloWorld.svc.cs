using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebServiceWCF
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę klasy „HelloWorld” w kodzie, usłudze i pliku konfiguracji.
    // UWAGA: aby uruchomić klienta testowego WCF w celu przetestowania tej usługi, wybierz plik HelloWorld.svc lub HelloWorld.svc.cs w eksploratorze rozwiązań i rozpocznij debugowanie.
    public class HelloWorld : IHelloWorld
    {
        public string getMessage(string text)
        {
            return "Hello " + text;
        }
    }
}

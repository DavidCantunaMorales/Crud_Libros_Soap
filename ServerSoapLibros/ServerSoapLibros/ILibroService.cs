using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServerSoapLibros
{
    [ServiceContract]
    public interface ILibroService
    {

        [OperationContract]
        Libro GetLibroById(int libroID);

        [OperationContract]
        List<Libro> GetAllLibros();

        [OperationContract]
        bool AddLibro(Libro libro);

        [OperationContract]
        bool UpdateLibro(Libro libro);

        [OperationContract]
        bool DeleteLibro(int libroID);
    }

    [DataContract]
    public class Libro
    {
        [DataMember]
        public int LibroID { get; set; }

        [DataMember]
        public string Titulo { get; set; }

        [DataMember]
        public string Autor { get; set; }

        [DataMember]
        public string Genero { get; set; }

        [DataMember]
        public decimal? Precio { get; set; }
    }
}

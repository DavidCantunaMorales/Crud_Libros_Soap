from zeep import Client
from zeep.exceptions import Fault

# URL del WSDL del servicio SOAP
wsdl = 'http://localhost:57185/LibroService.svc?wsdl'

# Crear un cliente SOAP
client = Client(wsdl)

# Obtener un libro por ID
libroID = 4
response = client.service.GetLibroById(libroID)
print(response)

# Obtener todos los libros
response = client.service.GetAllLibros()
print(response)

# Crear un libro
nuevo_libro = {
    'Titulo': 'El principito',
    'Autor': 'Marquez',
    'Genero': 'Fantasia',
    'Precio': 19.99
}

response = client.service.AddLibro(nuevo_libro)
if response:
    print("El libro fue agregado exitosamente.")
else:
    print("Error al agregar el libro.")

    
# Actualizar un libro existente
libro_actualizado = {
    'LibroID': 4,  # ID del libro a actualizar
    'Titulo': 'Cien Años de Soledad - Edición Revisada',
    'Autor': 'Gabriel García Márquez',
    'Genero': 'Realismo Mágico',
    'Precio': 21.99
}

response = client.service.UpdateLibro(libro_actualizado)
if response:
    print("El libro fue actualizado exitosamente.")
else:
    print("Error al actualizar el libro.")


# Eliminar un libro
response = client.service.DeleteLibro(libroID=4)
if response:
    print("El libro fue eliminado exitosamente.")
else:
    print("Error al eliminar el libro.")

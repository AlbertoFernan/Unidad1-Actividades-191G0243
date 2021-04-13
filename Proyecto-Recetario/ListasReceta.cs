using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

namespace Proyecto_Recetario
{
  
     
        public enum Vistas { Lista, Agregar, Editar, Eliminar, Ver }

    public class ListaRecetas : INotifyPropertyChanged
    {
        private Vistas vistas = Vistas.Lista;

            public Vistas Vista
            {
                get { return vistas; }
                set
                {
                    vistas = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Vista"));
                }
            }

            public ICommand MostrarEditarCommand { get; set; }
            public ICommand MostrarEliminarCommand { get; set; }

        public ICommand MostrarRecetaCommand { get; set; }

        public ICommand AgregarCommand { get; set; }
            public ICommand EditarCommand { get; set; }
            public ICommand EliminarCommand { get; set; }
        public ICommand MostrarAgregarCommand { get; set; }
        public ICommand FiltrarCommand { get; set; }
        public ICommand CambiarVistaCommand { get; set; }

            public ObservableCollection<Receta>Recetas { get; set; }

            public ObservableCollection<Receta>RecetasFiltradas{ get; set; } = new ObservableCollection<Receta>();

            public ListaRecetas()
            {
                Cargar();
                
                AgregarCommand = new RelayCommand(Agregar);
                EditarCommand = new RelayCommand(Editar);
                EliminarCommand = new RelayCommand(Eliminar);
            MostrarAgregarCommand = new RelayCommand(MostrarAgregar);
            CambiarVistaCommand = new RelayCommand<Vistas>(CambiarVista);
                MostrarEditarCommand = new RelayCommand<Receta>(MostrarEditar);
            MostrarRecetaCommand = new RelayCommand<Receta>(MostrarReceta);

            FiltrarCommand = new RelayCommand(Filtrar);
            MostrarEliminarCommand = new RelayCommand<Receta>(MostrarEliminar);

            }

            private void MostrarEliminar(Receta obj)
            {
                Receta = obj;
                CambiarVista(Vistas.Eliminar);
            }
        private void MostrarAgregar()
        {
            
            CambiarVista(Vistas.Agregar);
        }
        private void MostrarEditar(Receta obj)
            {
            Receta = new Receta { Nombre = obj.Nombre, Ingredientes = obj.Ingredientes, Procedimiento = obj.Procedimiento, Imagen = obj.Imagen };

                posicionOriginal = Recetas.IndexOf(obj);

                CambiarVista(Vistas.Editar);
            }


        private void MostrarReceta(Receta obj)
        {
            Receta = new Receta { Nombre = obj.Nombre, Ingredientes = obj.Ingredientes, Procedimiento = obj.Procedimiento, Imagen = obj.Imagen };

         

            CambiarVista(Vistas.Ver);
        }

        private void CambiarVista(Vistas obj)
            {
            Error = "";
            Vista = obj;

                if (Vista == Vistas.Agregar)
                {
                    Receta = new Receta();
                }

            }

        public string rece { get; set; }
        public void Filtrar()
        {

            RecetasFiltradas.Clear();
           
            var datos = Recetas.Where(x => rece==null || x.Nombre.ToUpper().StartsWith(rece.ToUpper()));

            foreach (var receta in datos)
            {
                RecetasFiltradas.Add(receta);
            }
        }

        private Receta receta;

            public Receta Receta
            {
                get { return receta; }
                set
                {
                    receta = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Receta)));
                }
            }

            private string error;

       
        public string Error
            {
                get { return error; }
                set
                {
                    error = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Error)));

                }
            }


            public void Agregar()
            {
                if (Receta != null)
                {
                    Error = "";
                    
                    if (string.IsNullOrWhiteSpace(Receta.Nombre))
                    {
                        Error = "Escriba el nombre de la receta";
                    return;
                }
                    if (string.IsNullOrWhiteSpace(Receta.Ingredientes))
                    {
                        Error = "Ingrese los ingredientes de la receta";
                    return;
                }

                if (string.IsNullOrWhiteSpace(Receta.Procedimiento))
                {
                    Error = "Ingrese el procedimiento de la receta";
                    return;
                }

                if (string.IsNullOrWhiteSpace(Receta.Imagen))
                    {
                        Error = "Escriba la URL de la imagen";
                    return;
                }

                

                if (Recetas.Any(x => x.Nombre.ToUpper() == Receta.Nombre.ToUpper()))
                    {
                        Error = "Ya existe una receta con el mismo nombre";
                    return;
                    }

              
                    Recetas.Add(Receta);
                
                   
                    Guardar();
                Filtrar();

                Vista = Vistas.Lista;
                }
            }

          


            int posicionOriginal;

            public event PropertyChangedEventHandler PropertyChanged;

            public void Editar()
            {
              
                if (Receta != null)
                {
                    Error = "";
                    //Validación
                    if (string.IsNullOrWhiteSpace(Receta.Nombre))
                    {
                        Error = "Escriba el nombre de la receta";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(Receta.Ingredientes))
                    {
                        Error = "Ingrese los ingredientes de la receta";
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(Receta.Procedimiento))
                    {
                        Error = "Ingrese el procedimiento de la receta";
                        return;
                    }

                if (string.IsNullOrWhiteSpace(Receta.Imagen))
                {
                    Error = "Ingrese el url de la imagen";
                    return;
                }

                if (Recetas[posicionOriginal].Nombre.ToUpper() != Receta.Nombre.ToUpper())
                 
                    {
                        if (Recetas.Any(x => x.Nombre.ToUpper() == Receta.Nombre.ToUpper()))
                        {
                            Error = "Ya existe una receta con el mismo nombre";
                            return;
                        }
                    }

                   
                    Recetas[posicionOriginal] = Receta;

           
                
                Guardar();
                Filtrar();

                CambiarVista(Vistas.Lista);
                }
            }

            public void Eliminar()
            {
           
            if (Recetas.Remove(Receta))
                {
                    Guardar();
                }
                CambiarVista(Vistas.Lista);
            Filtrar();
        }

            private void Guardar()
            {
                //Serialización XML
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Receta>));
                XmlWriter archivo = XmlWriter.Create("recetas.xml");

                serializer.Serialize(archivo, Recetas);
                archivo.Close();
            }


            private void Cargar()
            {
                try
                {

                    XmlReader archivo = XmlReader.Create("recetas.xml");
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Receta>));
                Recetas = (ObservableCollection<Receta>)serializer.Deserialize(archivo);
                    archivo.Close();

                }
                catch (Exception)
                {
                Recetas = new ObservableCollection<Receta>();
                }
            }
        }
    }

using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Input;
using System.Linq;


namespace Unidad_2_actividad_1
{
    public class ListaEpisodios : INotifyPropertyChanged
    {

        private bool agregando;
        private bool editando;

    

        public ICommand VerAgregarCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand VerEditarCommand { get; set; }



        public ListaEpisodios()
        {
            Load();
            VerAgregarCommand = new RelayCommand<string>(VerAgregar);
            AgregarCommand = new RelayCommand(Agregar);
            EliminarCommand = new RelayCommand(Eliminar);
            VerEditarCommand = new RelayCommand<string>(VerEditar);
            EditarCommand = new RelayCommand(Editar);
        }

        public bool Editando
        {
            get { return editando; }
            set
            {
                editando = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Editando"));
            }
        }
        public bool Agregando
        {
            get { return agregando; }
            set { agregando = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Agregando"));
            }
        }

        private void VerAgregar(string ver)
        {
            Error = "";
            Agregando = bool.Parse(ver);
            if(Agregando==true)
            {
                
            }
        }

        int posElementoEditar;

        private void VerEditar(string ver)
        {
            if (Capitulo != null)
            {
                Error = "";
                Editando = bool.Parse(ver);
                if (Editando == true)
                {
                    posElementoEditar = Capitulos.IndexOf(Capitulo);

                    Capitulo clon = new Capitulo
                    {
                        Numero = Capitulo.Numero,
                        Temporada = Capitulo.Temporada,
                        Titulo = Capitulo.Titulo,
                        TituloEspañol=Capitulo.TituloEspañol,
                        Descripcion=Capitulo.Descripcion

                    };

                    Capitulo = clon;
                }
                else
                {
                   Capitulo = Capitulos[posElementoEditar];
                }
            }
        }

        public void Editar()
        {
            Error = "";

            if (string.IsNullOrWhiteSpace(Capitulo.Titulo))
            {
                Error = "El nombre del Capitulo está en blanco.";
                return;
            }
            if (string.IsNullOrWhiteSpace(Capitulo.Temporada))
            {
                Error = "El número de temporada está en blanco.";
                return;
            }
            if (string.IsNullOrWhiteSpace(Capitulo.Numero))
            {
                Error = "El numero del capitulo no esta";
                return;
            }

            if (string.IsNullOrWhiteSpace(Capitulo.TituloEspañol))
            {
                Error = "El titulo en español no fue escrito";
                return;
            }
            if (string.IsNullOrWhiteSpace(Capitulo.Descripcion))
            {
                Error = "Hace falta la descripcion";
                return;
            }
            if (Capitulos.Any(x => x.Titulo == Capitulo.Titulo))
            {
                Error = "El capitulo ya esta registrado";
                return;
            }

            Capitulos[posElementoEditar] = Capitulo; //Alumno es el clon que editaron
            Save();

            Editando = false;
        }


        public ObservableCollection<Capitulo> Capitulos { get; set; }

        private Capitulo a;

        public Capitulo Capitulo
        {
            get { return a; }
            set
            {
                a = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Capitulo"));
            }
        }

        private string error;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Error"));
            }


        }

        public void Agregar()
        {

            Capitulo = new Capitulo();
            Capitulos.Add(Capitulo);
         

            Agregando = false;
        }

        public void Eliminar()
        {
            if (Capitulo != null)
            {
                Capitulos.Remove(Capitulo);
                Save();
            }
        }





        private void Save()
        {
            FileStream fs = File.Create("Capitulos.dat");
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, Capitulos);
            fs.Close();
        }

        private void Load()
        {
            try
            {
                FileStream fs = File.OpenRead("Capitulos.dat");
                BinaryFormatter bf = new BinaryFormatter();
                Capitulos = (ObservableCollection<Capitulo>)bf.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
              Capitulos = new ObservableCollection<Capitulo>();
            }

        }

    }

}




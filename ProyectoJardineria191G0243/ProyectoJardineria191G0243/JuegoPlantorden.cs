using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using Plugin.SimpleAudioPlayer;

namespace ProyectoJardineria191G0243
{

    
    public class JuegoPlantorden : INotifyPropertyChanged
    {

        public ObservableCollection<Fertilizante> diversosfer { get; set; } = new ObservableCollection<Fertilizante>();


        public ICommand ComenzarCommand { get; set; }

        public ICommand ComprarFerCommand { get; set; }

        public ICommand CompetirCommand { get; set; }

        public ICommand SiguienteRondaCommand { get; set; }
        public ICommand AgregarFertilizanteCommand { get; set; }



        ISimpleAudioPlayer tienda;
        ISimpleAudioPlayer comprando;
        ISimpleAudioPlayer pasaste;

        ISimpleAudioPlayer nopasaste;
        ISimpleAudioPlayer verpeticion;
        ISimpleAudioPlayer sindinero;



        public JuegoPlantorden()

        {
            sindinero = CrossSimpleAudioPlayer.Current;
            tienda = CrossSimpleAudioPlayer.Current;
            verpeticion = CrossSimpleAudioPlayer.Current;
            pasaste = CrossSimpleAudioPlayer.Current;
            comprando = CrossSimpleAudioPlayer.Current;
            nopasaste = CrossSimpleAudioPlayer.Current;
            



            diversosfer.Add(new Fertilizante() { Nombre = "Barato", Descripcion = "de 1 - 2 metros", imagen = "FBARATO.png", Precio=150 });

            diversosfer.Add(new Fertilizante() { Nombre = "Costo medio", Descripcion = "de 3 - 4 metros", imagen = "FMEDIO.png", Precio=250 });

            diversosfer.Add(new Fertilizante() { Nombre = "Costoso", Descripcion = "de 5 - 6 metros", imagen = "FCOSTOSO.png", Precio=350 });

            diversosfer.Add(new Fertilizante() { Nombre = "Comodin 1", Descripcion = " 1 metro", imagen = "Comodin1.png", Precio=200 });
            ComprarFerCommand = new Command(VerFertilizantes);

            CompetirCommand = new Command(verificar);
            ComenzarCommand = new Command(Iniciar);
            SiguienteRondaCommand = new Command(Siguiente);
            AgregarFertilizanteCommand = new Command<Fertilizante>(AgregarFertilizante);
        }


        SelecFertiView vista3;

        PeticionView vista2;

        ResultadoView vista4;
        public PlantaJugador planta { get; set; } = new PlantaJugador();




        private int tamañoposible;

        public int Tamañoposible
        {
            get { return tamañoposible; }
            set { tamañoposible = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tamañoposible))); }
        }

        private int peticion;

        public int Peticion
        {
            get { return peticion; }
            set { peticion = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Peticion))); }
        }

        private string imagentamaño;

        public string TmagenTamaño
        {
            get { 
                if(planta.Tamaño>=0 && planta.Tamaño<3)
                {
                    return "Uno";
                }
                else if (planta.Tamaño >= 3 && planta.Tamaño < 6)
                {
                    return "Dos";
                }
                else if (planta.Tamaño >= 6 && planta.Tamaño < 9)
                {
                    return "Tres";
                }
                else if (planta.Tamaño >= 9 && planta.Tamaño < 12)
                {
                    return "Cuatro";
                }
                else {
                    return "Cinco";
                }
              
                  
                

 }
            set { imagentamaño = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TmagenTamaño))); }
        }


        Random Metros = new Random();
        private void ObtenerPeticion()
        {
            Peticion = Metros.Next(2, 16);
            verpeticion.Load("Peticion.mp3");


            verpeticion.Play();
        }
        private void Siguiente()
        {

            vista2 = new PeticionView() { BindingContext = this };





            Application.Current.MainPage.Navigation.PushAsync(vista2);

            Resultado = false;
            planta.Tamaño = 0;
            Tamañoposible = 0;
            ObtenerPeticion();


          

        }


        private bool resultado;

        public bool Resultado
        {
            get { return resultado; }
            set { resultado = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Resultado))); }
        }


       

        private string mensaje;

        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mensaje))); }
        }
      
        private void verificar()
        {
           

             vista4 = new ResultadoView() { BindingContext = this }; 
                
            
           

            Application.Current.MainPage.Navigation.PushAsync(vista4);

            if (planta.Tamaño>peticion || planta.Tamaño==0 )
            {
                Resultado = false;
                Mensaje = "Has perdido, la planta sobrepaso lo pedido o no creció en absoluto";
                nopasaste.Load("Nopasaste.mp3");

                nopasaste.Play();
            }

            else if (planta.Tamaño < (peticion-2))
            {
                Resultado = false;
                Mensaje = "Has perdido, la diferencia de altura entre tu planta y la requerida es de más de 2 metros";
                nopasaste.Load("Nopasaste.mp3");

                nopasaste.Play();
            }
            else
            {
                Mensaje = "Has pasado, ganaste: "+ (planta.Tamaño * 100)+" pesos"
                    ;
                Resultado = true;
                planta.Dinero += (planta.Tamaño * 100);

                pasaste.Load("Pasaste.mp3");

              pasaste.Play();
            }
        }



        private void Iniciar()
        {

            Resultado = false;
            planta.Dinero= 700;
            planta.Tamaño = 0;
            
           Tamañoposible = 0;
            ObtenerPeticion();

           
            vista2 = new PeticionView() { BindingContext = this };

               
            


          

            Application.Current.MainPage.Navigation.PushAsync(vista2);

        }

        private void VerFertilizantes()
        {
            tienda.Load("TIENDA.wav");

            tienda.Play();
           
         

            vista3 = new SelecFertiView() { BindingContext = this };
            
               
            
            

            Application.Current.MainPage.Navigation.PushAsync(vista3);
        }
      
      
        private void AgregarFertilizante(Fertilizante fer)
        {
           

           if(fer.Nombre=="Barato" && (planta.Dinero- fer.Precio) >=0)
            {
                planta.Dinero -= fer.Precio;
                Tamañoposible += 2;
              planta.Tamaño += Metros.Next(1, 3);
                comprando.Load("Compra.mp3");
                comprando.Play();
            }
           else if(fer.Nombre == "Costo medio" && (planta.Dinero - fer.Precio) >= 0)
            {
                planta.Dinero -= fer.Precio;
                Tamañoposible += 4;
                planta.Tamaño += Metros.Next(3, 5);
                comprando.Load("Compra.mp3");
                comprando.Play();
            }
            else if (fer.Nombre == "Costoso" && (planta.Dinero - fer.Precio) >= 0)
            {
                planta.Dinero -= fer.Precio;
                Tamañoposible += 6;
                planta.Tamaño += Metros.Next(5, 7);
                comprando.Load("Compra.mp3");
                comprando.Play();
            }
            else if (fer.Nombre == "Comodin 1" && (planta.Dinero - fer.Precio) >= 0)
            {
                planta.Dinero -= fer.Precio;
                Tamañoposible += 1;
                planta.Tamaño += 1;
                comprando.Load("Compra.mp3");
                comprando.Play();
            }
            else
            {
                sindinero.Load("Sindinero.mp3");
                sindinero.Play();

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}

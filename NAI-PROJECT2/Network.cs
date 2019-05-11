using System;
using System.Collections.Generic;
using System.Linq;


namespace NAI_PROJECT2
{
    class Network
    {
        private List<List<Neuron>> Layers;
        private double Alpha;
        private List<Training> TrainingSet; 
        public Network(int firstLayerSize,int secondLayerSize,List<Training> TrainingSet,double Alpha)
        {
            Layers = new List<List<Neuron>>();
            this.Alpha = Alpha;
            this.TrainingSet = TrainingSet;

            var firstLayer = new List<Neuron>();
            Layers.Add(new List<Neuron>());
            //Dodajemy neurony do sieci
            for (int i = 0; i < firstLayerSize; i++)
            {
                firstLayer.Add(new Neuron());
            }
            Layers.Add(firstLayer);
            var secondLayer = new List<Neuron>();
            for (int i = 0; i < secondLayerSize; i++)
            {
                secondLayer.Add(new Neuron());
            }
            Layers.Add(secondLayer);
            //Dla kazdego neuronu ustawiamy losowe wartosci wektora wag
            foreach (List<Neuron> layer in Layers)
            {
                foreach(Neuron neuron in layer)
                {
                    neuron.RadomizeWeights();
                }
               
            }
        }
       
        public void StartLearning()
        {
            
           // Uczymy sie dopóki bład nie będzie równy 0 lub nie minie 100 epok
            for (int i = 1; i < 101; i++)
            {
                double networkError = 0;
                Console.WriteLine("Ucze się już "+i+" epoke");
                //Przechodzimy przez wszystkie zestawy treningowe dla kazdego neuronu przy kazdym przejsciu sprawdzamy blad sieci 
                foreach(Training training in TrainingSet)
                {
                    var output = new List<double>();
                    foreach(Neuron neuron in Neurons)
                    {
                        output.Add(CalculateOutput(training.Input,neuron));

                    }
                    CalculateNewWeights(output, training.Expected, training.Input);
                    for (int j = 0; j < training.Expected.Count(); j++)
                    {
                        networkError += Math.Pow((training.Expected.ElementAt(j)-output.ElementAt(j)), 2);
                    }
                }
               
                networkError /= 2;
                Console.WriteLine(networkError);
                //Jesli blad sieci wynosi 0 konczymy nauke przerywajac petle
                if (networkError == 0.0)
                {
                    Console.WriteLine("Nauczylem sie");
                    break;
                }
            }

        }
        //Metoda sprawdzajaca wejscie dla nauczonej sieci 
        public string Test(IEnumerable<double> input)
        {
            string response="";
            Console.WriteLine("TESTOWE");

            foreach(Neuron neuron in Neurons)
            {
                var y = CalculateOutput(input, neuron);
                response += y;
                Console.WriteLine(y);
            }
            switch (response)
            {
                case "1000000000": response = "Liczba to : 0"; break;
                case "0100000000": response = "Liczba to : 1"; break;
                case "0010000000": response = "Liczba to : 2"; break;
                case "0001000000": response = "Liczba to : 3"; break;
                case "0000100000": response = "Liczba to : 4"; break;
                case "0000010000": response = "Liczba to : 5"; break;
                case "0000001000": response = "Liczba to : 6"; break;
                case "0000000100": response = "Liczba to : 7"; break;
                case "0000000010": response = "Liczba to : 8"; break;
                case "0000000001": response = "Liczba to : 9"; break;
                default:response = "Nieznana liczba";break;
            }
            return response;
        }
        private double CalculateOutput(IEnumerable<double> input, Neuron neuron)
        {
            var net = 0.0;
            for (int i = 0; i < input.Count(); i++)
            {
                net += input.ElementAt(i) * neuron.Weigths.ElementAt(i);
            }
            net += neuron.Bias;
            return Activate(net);
        }
        //Funkcja aktywacji w tym przypadku jest to liniowa unipolarna 
        private double Activate(double net)
        {
            return net >= 0 ? 1 : 0;
        }
        private void CalculateNewWeights(List<double> received, List<double>  expected, List<double> input)
        {

            for (int i = 0; i < Neurons.Count(); i++)
      
            {
                var nWeights = new List<double>();
                for (int j = 0; j < input.Count(); j++)
                {
                    var oldW = Neurons.ElementAt(i).Weigths.ElementAt(j);
                    nWeights.Add(oldW +Alpha * (expected.ElementAt(i) - received.ElementAt(i)) * input.ElementAt(j));
                }
                Neurons.ElementAt(i).Weigths = nWeights;
                Neurons.ElementAt(i).Bias += Alpha * (expected.ElementAt(i) - received.ElementAt(i));
            }

            
        }
       


    }
}

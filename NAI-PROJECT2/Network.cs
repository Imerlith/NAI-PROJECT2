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
        private static readonly double LAMBDA = 1;
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
        //Funkcja aktywacji w tym przypadku jest to sigmoidalna  unipolarna 
        private double Activate(double net)
        {
            return 1 / (1 + Math.Pow(Math.E, -1*net));
        }
        private void CalculateNewWeights(List<double> received, List<double>  expected, 
            List<double> input, List<List<double>> allOutputs)
        {
            var errors = new List<List<double>>();
            var lastLayerErrors = new List<double>();
            for (int i = 0; i < Layers.Last().Count; i++)
            {
                lastLayerErrors.Add((expected.ElementAt(i) - received.ElementAt(i)) * Derive(received.ElementAt(i)));
            }
            errors.Add(lastLayerErrors);

            //pozostałe warstwy

            for (int i = Layers.Count-2; i >=0 ; i--)
            {
                var layerError = new List<double>();
                for (int j = 0; j < Layers.ElementAt(i).Count; j++)
                {
                    double error = 0;
                    int lastErrorIndex = errors.Count - 1;
                    for (int k = 0; k < Layers.ElementAt(i+1).Count; k++)
                    {
                        error += Layers.ElementAt(i + 1).ElementAt(k).Weigths.ElementAt(j)
                            * errors.ElementAt(lastErrorIndex - 1).ElementAt(k);
                    }
                    error *= Derive(allOutputs.ElementAt(i).ElementAt(j));
                    layerError.Add(error);
                }
                errors.Add(layerError);
            }

            errors.Reverse();
            for (int i = 0; i < Layers.First().Count; i++)
            {
                for (int j = 0; j < Layers.First().ElementAt(i).Weigths.Count(); j++)
                {
                    var weight = Layers.First().ElementAt(i).Weigths.ElementAt(j);
                    var nWeight = weight + Alpha * errors.First().ElementAt(i) * input.ElementAt(j);
                    Layers.First().ElementAt(i).Weigths[j] = nWeight;
                }
                var bias = Layers.First().ElementAt(i).Bias;
                var nBias = bias + Alpha * errors.First().ElementAt(i);
                Layers.First().ElementAt(i).Bias = nBias;
            }


            //pozostałe warstwy

            for (int i = 1; i < Layers.Count(); i++)
            {
                for (int j = 0; j < Layers.ElementAt(i).Count(); j++)
                {
                    for (int k = 0; k < Layers.ElementAt(i).ElementAt(j).Weigths.Count(); k++)
                    {
                        var weight = Layers.ElementAt(i).ElementAt(j).Weigths.ElementAt(k);
                        var nWeight = weight + Alpha * errors.ElementAt(i).ElementAt(j) 
                            * allOutputs.ElementAt(i - 1).ElementAt(k);
                        Layers.ElementAt(i).ElementAt(j).Weigths[k] = nWeight;
                    }
                    var bias = Layers.ElementAt(i).ElementAt(j).Bias;
                    var nBias = bias + Alpha * errors.ElementAt(i).ElementAt(j);
                    Layers.ElementAt(i).ElementAt(j).Bias = nBias;
                }
            }
        }
        private double Derive(double num)
        {
            var norm = Activate(num);
            return LAMBDA * norm * (1.0 - norm);
        }
        private ICollection<double> CalculateLayerOutput(ICollection<Neuron> layer,ICollection<double> input)
        {
            var layerOutput = new List<double>();
            foreach(Neuron neuron in layer)
            {
                layerOutput.Add(CalculateOutput(input, neuron));
            }
            return layerOutput;
        }
        private ICollection<double> CalculateNetworkOutput(ICollection<double> input)
        {
            var lastLayerOutput = CalculateLayerOutput(Layers.Last(), input);
            for (int i = 0; i < Layers.Count; i++)
            {
                lastLayerOutput = CalculateLayerOutput(Layers.ElementAt(i),lastLayerOutput);
            }
            return lastLayerOutput;
        }
       
       


    }
}

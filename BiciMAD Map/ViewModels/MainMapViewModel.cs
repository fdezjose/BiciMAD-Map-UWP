using BiciMAD_Map.Models;
using BiciMAD_Map.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls.Maps;

namespace BiciMAD_Map.ViewModels
{
    class MainMapViewModel : ViewModelBase
    {
        private ObservableCollection<Station> stations;
        private bool isLoading;

        private DelegateCommand fetchStations;

        public MainMapViewModel()
        {
            fetchStations = new DelegateCommand(FetchStationsExecute, FetchStationsCanExecute);

            stations = new ObservableCollection<Station>();
        }

        public ObservableCollection<Station> Stations
        {
            get { return stations; }
            set
            {
                stations = value;
                RaisePropertyChanged("Stations");
            }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        public async void FetchStationsExecute(object parameter)
        {
            IsLoading = true;

            try
            {
                Uri requestUri = new Uri("http://helena.bonopark.es:16080/app/app/functions/get_all_estaciones_new.php?format=json");
                dynamic postDataJson = new ExpandoObject();
                postDataJson.id_auth = "INICIO_APP";
                postDataJson.id_security = "62474822af20a313acd2f66e73f0b38d69bb65c336433449e621b24f95ff5c8b";
                postDataJson.dni = "INICIO_APP";

                JsonObject jsonObject = new JsonObject();
                jsonObject["id_auth"] = JsonValue.CreateStringValue("INICIO_APP");
                jsonObject["id_security"] = JsonValue.CreateStringValue("62474822af20a313acd2f66e73f0b38d69bb65c336433449e621b24f95ff5c8b");
                jsonObject["dni"] = JsonValue.CreateStringValue("INICIO_APP");

                string json = jsonObject.Stringify();
                var httpClient = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage respon = await httpClient.PostAsync(requestUri, new System.Net.Http.StringContent(json, System.Text.Encoding.UTF8, "application/json"));
                string responeJsonText = await respon.Content.ReadAsStringAsync();

                JsonObject responseJson = JsonObject.Parse(responeJsonText);

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Station>));
                object serializedResponse = jsonSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(responseJson["estaciones"].Stringify())));

                if (serializedResponse != null)
                {
                    Stations = serializedResponse as ObservableCollection<Station>;
                }
            }
            catch(Exception ex)
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

                var messageDialog = new MessageDialog(loader.GetString("InternetError"));

                await messageDialog.ShowAsync();
            }

            IsLoading = false;
        }

        public bool FetchStationsCanExecute()
        {
            return true;
        }
    }
}
